using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;

using CodeLib.Models;

namespace CodeLib.DAL
{
    public class UserDAL
    {
        public static AppUser GetUserById(string userIdOrEmail)
        {
            AppUser user = null;

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_GetUserById, userIdOrEmail);

                if (rdr != null)
                {
                    if (rdr.Read())
                        user = AppUser.ReadDataToAppUserObject(rdr);

                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, userIdOrEmail);
            }

            return user;
        }

        public static Task<AppUser> GetUserByIdAsync(int userId)
        {
            AppUser user = null;

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_GetUserById, userId.ToString());

                if (rdr != null)
                {
                    if (rdr.Read())
                        user = AppUser.ReadDataToAppUserObject(rdr);

                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, userId.ToString());
            }

            return Task.FromResult(user);
        }

    }
}
