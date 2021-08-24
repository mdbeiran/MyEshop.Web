using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MyEshop.Web.Areas.UserPanel.Controllers
{

    using MyEshop.ViewModel;
    using MyEshop.Services;
    using MyEshop.Utility;
    using MyEshop.Business;
    using MyEshop.DomainClass;


    [RouteArea("UserPanel", AreaPrefix = "User")]
    [RoutePrefix("")]
    public class HomeController : Controller
    {

        #region Ctor

        private MyEshopUOW _db;
        private IPasswordHelper _passwordHelper;
        public HomeController(MyEshopUOW db, IPasswordHelper passwordHelper)
        {
            this._db = db;
            this._passwordHelper = passwordHelper;
        }

        #endregion

        #region Index User Panel

        [Route("", Name = "UserIndex")]
        public ActionResult Index()
        {
            UserPanelIndexViewModel data = _db.UserRepository.GetUserPanelIndexData(UserManager.GetCurrentUserId());
            return View(data);
        }

        #endregion

        #region Edit User Information

        [HttpGet]
        [Route("EditInformation", Name = "EditInformationGet")]
        public ActionResult EditInformation()
        {
            User user = _db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
            return View(user);
        }


        [HttpPost]
        [Route("EditInformation", Name = "EditInformationPost")]
        [ValidateAntiForgeryToken]
        public ActionResult EditInformation([Bind(Include = "UserId,RoleId,UserName,Email,Password,ActiveCode,RegisterDate,IsActive,IsDelete,ViewByAdmin")] User editUser)
        {
            if (ModelState.IsValid)
            {
                _db.UserRepository.UpdateUser(editUser);
                _db.Save();

                return RedirectToRoute("UserIndex");
            }

            return View(editUser);
        }

        #endregion

        #region User Sidebar

        public ActionResult UserSidebar()
        {
            User user = _db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
            return PartialView("UserSidebar", user);
        }

        #endregion

        #region Change User Password

        [HttpGet]
        [Route("ChangePassword", Name = "ChangePasswordGet")]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword", Name = "ChangePasswordPost")]
        public ActionResult ChangePassword([Bind(Include = "OldPassword, Password, RePassword")]ChangePasswordViewModel userPassword)
        {
            if (ModelState.IsValid)
            {
                string oldPassword = _passwordHelper.EncodePasswordMd5(userPassword.OldPassword);
                User thisUser = _db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());

                if (oldPassword == thisUser.Password)
                {
                    thisUser.Password = _passwordHelper.EncodePasswordMd5(userPassword.Password);
                    _db.Save();

                    ViewBag.userName = thisUser.UserName;
                    ViewBag.changePasswordSuccessful = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی اشتباه می باشد !");
                }
            }

            return View();
        }

        #endregion

    }
}