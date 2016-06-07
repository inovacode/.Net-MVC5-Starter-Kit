using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace CodeLib.DAL
{
    public class CommonDAL
    {
        public static SqlDataReader GetQueryData(DatabaseIdEnum queryDataType, string filter)
        {
            DataTable table = new DataTable();
            var sqlParms = new List<SqlParameter>
            {
                SqlDataHelper.GetParam("@QueryDataTypeId", SqlDbType.Int, (int)queryDataType),
                SqlDataHelper.GetParam("@Filter", SqlDbType.VarChar, filter)
            };

            return SqlHelper.ExecuteReader(SqlDataHelper.GetConnectionString(), "GetQueryData", sqlParms.ToArray());
        }

        public static Task<List<Models.Lookup>> GetLookupList(LookupTypeIdEnum lookupType)
        {
            List<Models.Lookup> results = new List<Models.Lookup>();

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_LookupTable, ((int)lookupType).ToString());

                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        results.Add(new Models.Lookup
                        {
                            LookupId = SqlDataHelper.GetDataReaderValue<int>(rdr, "LookupId"),
                            Descr = SqlDataHelper.GetDataReaderValue<string>(rdr, "Descr"),
                            Value = SqlDataHelper.GetDataReaderValue<string>(rdr, "Value")
                        });
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, ((int)lookupType).ToString());
            }

            return Task.FromResult(results);
        }

        #region Insert Log Methods

        public static long InsertLog(DatabaseIdEnum logTypeId, string page, string url, string descr, string linkToId)
        {
            long logId = 0;

            try
            {
                // Exclude the exceptions that are thrown due to the Response.Redirect() method without the "end response" set to false.
                if (!descr.Contains("Thread was being aborted") && !descr.Contains("System.Web.HttpResponse.Redirect"))
                {
                    using (var dbContext = new CodeLib.Models.Entities())
                    {
                        decimal result = dbContext.Log_Insert((int)logTypeId, descr, url, page, linkToId).FirstOrDefault() ?? -1;

                        if (result > 0)
                            long.TryParse(result.ToString(), out logId);
                    }
                }
            }
            catch (Exception ex)
            {
                // Don't let this error bubble up to the UI
                string error = ex.Message;
            }

            return logId;
        }

        public static long InsertExceptionLog(DatabaseIdEnum logTypeId, string exception, string stackTrace, string linkToId)
        {
            return InsertExceptionLog(logTypeId, null, null, exception, stackTrace, linkToId);
        }

        public static long InsertExceptionLog(DatabaseIdEnum logTypeId, string page, string url, string exception, string stackTrace, string linkToId)
        {
            return InsertLog(logTypeId, page, url, exception + "<br /><br /><b>Stack Trace:</b><br />" + stackTrace, linkToId);
        }

        #endregion
    }
}
