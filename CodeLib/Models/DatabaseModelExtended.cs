using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeLib.Models
{
    public partial class AppUser
    {
        public ApplicationUser IdentityUser { get; set; }
        public string Email { get; set; }

        [Display(Name = "Status")]
        public string StatusDisplay
        {
            get
            {
                return CodeLib.SiteUtils.GetDisplayAttributeFrom((CodeLib.DatabaseIdEnum)Enum.Parse(
                    typeof(CodeLib.DatabaseIdEnum), this.StatusId.ToString()), typeof(CodeLib.DatabaseIdEnum)); 
            }
        }

        [Display(Name = "User Name")]
        public string FullName
        {
            get
            {
                return (!string.IsNullOrWhiteSpace(this.FirstName) ? this.FirstName : string.Empty) + " " +
                       (!string.IsNullOrWhiteSpace(this.LastName) ? this.LastName : string.Empty);
            }
        }

        #region Methods

        public static AppUser ReadDataToAppUserObject(System.Data.SqlClient.SqlDataReader rdr)
        {
            AppUser user = new AppUser
            {
                UserId = DAL.SqlDataHelper.GetDataReaderValue<int>(rdr, "UserId"),
                FirstName = DAL.SqlDataHelper.GetDataReaderValue<string>(rdr, "FirstName"),
                LastName = DAL.SqlDataHelper.GetDataReaderValue<string>(rdr, "LastName"),
                Email = DAL.SqlDataHelper.GetDataReaderValue<string>(rdr, "Email"),
                StatusId = DAL.SqlDataHelper.GetDataReaderValue<int>(rdr, "StatusId")
            };

            return user;
        }

        #endregion
    }
}
