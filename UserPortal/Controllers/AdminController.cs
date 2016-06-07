using System;
using UserPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;

using CodeLib;
using CodeLib.Models;
using CodeLib.DAL;

namespace UserPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Roles Mgmt

        public async Task<ActionResult> RolesMgmt()
        {
            return View(await AdminDAL.GetRoles(null));
        }

        public async Task<ActionResult> RoleDetails(int id)
        {
            if (id > 0)
            {
                ApplicationRole role = await AdminDAL.GetRoleById(id);  // Get role info
                var users = await AdminDAL.GetUsersByRole(id);  // Get the list of Users in this Role

                ViewBag.Users = users;
                ViewBag.UserCount = users.Count();
                return View(role);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RoleCreate(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int roleId = await AdminDAL.Role_InsertUpdateDelete(roleViewModel.Id, roleViewModel.Name, roleViewModel.Description, false);

                    if (roleId <= 0)
                    {
                        ModelState.AddModelError("", "The process failed to create the user role.");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Oops! An error has occurred. " + CommonObjects.ERROR_MSG_SUPPORT);
                    CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, null);
                }

                return RedirectToAction("RolesMgmt");
            }
            return View();
        }

        public async Task<ActionResult> RoleEdit(int id)
        {
            if (id > 0)
            {
                var role = await AdminDAL.GetRoleById(id);  // Get role info
                if (role == null)
                    return HttpNotFound();

                RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name, Description = role.Description };
                return View(roleModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleEdit([Bind(Include = "Name,Id,Description")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (roleModel.Id <= 2) // Admin, Customer Support, App User
                    {
                        ModelState.AddModelError("", "This user role cannot be changed because the system needs it to function.");
                        return View();
                    }

                    int roleId = await AdminDAL.Role_InsertUpdateDelete(roleModel.Id, roleModel.Name, roleModel.Description, false);

                    if (roleId <= 0 || roleId != roleModel.Id)
                    {
                        ModelState.AddModelError("", "The process failed to update the user role.");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Oops! An error has occurred. " + CommonObjects.ERROR_MSG_SUPPORT);
                    CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, null);
                }

                return RedirectToAction("RolesMgmt");
            }
            return View();
        }

        public async Task<ActionResult> RoleDelete(int id)
        {
            if (id > 0)
            {
                ApplicationRole role = await AdminDAL.GetRoleById(id);

                if (role == null)
                    return HttpNotFound();

                return View(role);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost, ActionName("RoleDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id > 0)
                    {
                        if (id <= 2) // Admin, Customer Support, App User
                        {
                            ModelState.AddModelError("", "This user role cannot be deleted because the system needs it to function.");
                            return View();
                        }

                        id = await AdminDAL.Role_InsertUpdateDelete(id, null, null, true);

                        if (id <= 0)
                        {
                            ModelState.AddModelError("", "The process failed to delete the user role.");
                            return View();
                        }
                        return RedirectToAction("RolesMgmt");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Oops! An error has occurred. " + CommonObjects.ERROR_MSG_SUPPORT);
                    CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, null);
                }
            }
            return View();
        }

        #endregion

        #region Users Mgmt

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public async Task<ActionResult> UsersMgmt()
        {
            return View(await AdminDAL.GetUsersByRole(null));
        }

        public async Task<ActionResult> UserMgmt(int id)
        {
            ViewBag.Title = "Create New User";
            ViewBag.ButtonText = "Create";
            var statusList = await CommonDAL.GetLookupList(LookupTypeIdEnum.AppUserStatus);
            var statusOptions = statusList.Select(status => new SelectListItem { Text = status.Descr, Value = status.LookupId.ToString() }).ToList();

            if (id > 0)
            {
                var user = await UserDAL.GetUserByIdAsync(id);
                if (user != null)
                {
                    ViewBag.Title = "Update User";
                    ViewBag.ButtonText = "Update";

                    return View(new UserViewModel()
                    {
                        UserId = user.UserId,
                        StatusId = user.StatusId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        StatusList = statusOptions
                    });
                }
                else
                    ModelState.AddModelError("", "Invalid UserId.");
            }

            return View(new UserViewModel
            {
                UserId = id,
                StatusList = statusOptions
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserMgmt(UserViewModel userModel)
        {
            if (ModelState.IsValid || (userModel.UserId > 0 && string.IsNullOrWhiteSpace(userModel.Password)))
            {
                ApplicationUser identityUser = null;
                IdentityResult result = null;
                int? userRoleId = null;
                bool sendWelcomeEmail = (userModel.UserId <= 0);

                if (userModel.UserId <= 0)
                {
                    // Create the AspNet Identity user
                    identityUser = new CodeLib.ApplicationUser { UserName = userModel.Email, Email = userModel.Email };
                    result = await UserManager.CreateAsync(identityUser, userModel.Password);
                    if (result.Succeeded)
                    {
                        userModel.UserId = identityUser.Id;
                        userRoleId = (int)RoleIdEnum.AppUser;
                    }
                    else
                        AddErrors(result);
                }

                if (result == null || result.Succeeded)
                {
                    #region Create AppUser Record
                    try
                    {
                        AppUser appUser = null;
                        int? loggedInUserId = (Request.IsAuthenticated) ? User.Identity.GetUserId<int>() : new int();

                        using (var dbContext = new CodeLib.Models.Entities())
                        {
                            int userId = dbContext.AppUser_InsertUpdate(userModel.UserId, userModel.StatusId,
                                userRoleId, userModel.FirstName, userModel.LastName, userModel.Email, loggedInUserId).FirstOrDefault() ?? -1;

                            if (userId == userModel.UserId)
                            {
                                appUser = new AppUser
                                {
                                    UserId = userId,
                                    StatusId = userModel.StatusId,
                                    FirstName = userModel.FirstName,
                                    LastName = userModel.LastName,
                                    Email = userModel.Email,
                                    IdentityUser = identityUser
                                };
                            }
                            else
                                CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, "The DB failed to save AppUser record for " + userModel.FirstName + " " + userModel.LastName, "AdminController.cs >> UserMgmt() >> AppUser_InsertUpdate()", identityUser.Id.ToString());
                        }

                        if (appUser != null)
                        {
                            if (sendWelcomeEmail)
                            {
                                // Send an email with this link
                                string code = await UserManager.GenerateEmailConfirmationTokenAsync(appUser.UserId);
                                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = appUser.UserId, code = code }, protocol: Request.Url.Scheme);

                                if (!CodeLib.Email.EmailTemplate.SendRegistrationEmail(appUser, callbackUrl))
                                    CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, "The registration confirmation email failed to send to " + appUser.Email, "AdminController.cs >> UserMgmt()", appUser.UserId.ToString());
                            }

                            return RedirectToAction("UsersMgmt");
                        }
                        else
                            AddErrors(new IdentityResult(new string[] { "Oops! An error has occurred. " + CommonObjects.ERROR_MSG_SUPPORT }));
                    }
                    catch (Exception ex)
                    {
                        AddErrors(new IdentityResult(new string[] { "Oops! An error has occurred. " + CommonObjects.ERROR_MSG_SUPPORT }));
                        CommonDAL.InsertExceptionLog(DatabaseIdEnum.LogType_SiteException, SiteUtils.GetPageName(), null, ex.Message, ex.StackTrace, userModel.Email);
                    }
                    #endregion
                }
            }

            var statusList = await CommonDAL.GetLookupList(LookupTypeIdEnum.AppUserStatus);
            userModel.StatusList = statusList.Select(status => new SelectListItem { Text = status.Descr, Value = status.LookupId.ToString() }).ToList();            

            return View(userModel);
        }

        #endregion

        #region Helper Methods

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}
