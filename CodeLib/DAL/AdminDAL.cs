using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeLib.Models;

namespace CodeLib.DAL
{
    public class AdminDAL
    {
        #region Roles

        public static async Task<ApplicationRole> GetRoleById(int? roleId)
        {
            ApplicationRole role = null;
            List<ApplicationRole> roles = await GetRoles(roleId);
            if (roles != null && roles.Count == 1)
                role = new ApplicationRole(roles[0].Id, roles[0].Name, roles[0].Description);

            return role;
        }

        public static Task<List<ApplicationRole>> GetRoles(int? roleId)
        {
            List<ApplicationRole> roles = new List<ApplicationRole>();

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_AspNetRoles,
                    (roleId.HasValue && roleId.Value > 0) ? roleId.Value.ToString() : null);

                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        roles.Add(new ApplicationRole
                        {
                            Id = SqlDataHelper.GetDataReaderValue<int>(rdr, "Id"),
                            Name = SqlDataHelper.GetDataReaderValue<string>(rdr, "Name"),
                            Description = SqlDataHelper.GetDataReaderValue<string>(rdr, "Description")
                        });
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, (roleId.HasValue) ? roleId.Value.ToString() : null);
            }

            return Task.FromResult(roles);
        }

        public static Task<List<ApplicationRole>> GetUserRoles(int userId)
        {
            List<ApplicationRole> roles = new List<ApplicationRole>();

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_GetUserRoles, userId.ToString());

                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        roles.Add(new ApplicationRole
                        {
                            Id = SqlDataHelper.GetDataReaderValue<int>(rdr, "Id"),
                            Name = SqlDataHelper.GetDataReaderValue<string>(rdr, "Name"),
                            Description = SqlDataHelper.GetDataReaderValue<string>(rdr, "Description")
                        });
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, userId.ToString());
            }

            return Task.FromResult(roles);
        }

        public static Task<int> Role_InsertUpdateDelete(int? roleId, string name, string description, bool delete)
        {
            try
            {
                using (var dbContext = new Models.Entities())
                {
                    roleId = dbContext.AspNetRoles_InsertUpdateDelete(roleId, name, description, delete).FirstOrDefault() ?? -1;
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, roleId.ToString());
                roleId = -1;
            }

            return Task.FromResult(roleId ?? -1);
        }

        public static Task<List<AppUser>> GetUsersByRole(int? roleId)
        {
            List<AppUser> users = new List<AppUser>();

            try
            {
                SqlDataReader rdr = CommonDAL.GetQueryData(DatabaseIdEnum.QueryDataType_GetUsersByRoleId,
                    (roleId.HasValue && roleId.Value > 0) ? roleId.Value.ToString() : null);

                if (rdr != null)
                {
                    while (rdr.Read())
                        users.Add(AppUser.ReadDataToAppUserObject(rdr));

                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, 
                    ex.Message, ex.StackTrace, (roleId.HasValue && roleId.Value > 0) ? roleId.Value.ToString() : null);
            }

            return Task.FromResult(users);
        }

        #endregion
    }
}
