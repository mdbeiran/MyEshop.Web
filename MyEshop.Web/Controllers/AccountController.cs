using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyEshop.Web.Controllers
{

    using MyEshop.ViewModel;
    using MyEshop.Services;
    using MyEshop.DomainClass;
    using MyEshop.Utility;
    using MyEshop.Business;

    public class AccountController : Controller
    {
        #region Ctor

        private MyEshopUOW _db;
        private IPasswordHelper _passwordHelper;
        public AccountController(MyEshopUOW db, IPasswordHelper passwordHelper)
        {
            this._db = db;
            this._passwordHelper = passwordHelper;
        }

        #endregion

        #region Register

        [HttpGet]
        [Route("Register", Name = "RegisterGet")]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("Register", Name = "RegisterPost")]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName, Email, Password, RePassword")] RegisterViewModel user)
        {
            // ثبت نام کاربر در سایت
            if (ModelState.IsValid)
            {
                if (!_db.UserRepository.IsExistUserWithUserName(user.UserName))
                {
                    if (!_db.UserRepository.IsExistUserWithEmail(user.Email))
                    {
                        User newUser = new User()
                        {
                            RoleId = 1,
                            UserName = user.UserName,
                            Email = FixedText.FixedEmail(user.Email),
                            Password = _passwordHelper.EncodePasswordMd5(user.Password),
                            ActiveCode = Guid.NewGuid().ToString(),
                            RegisterDate = DateTime.Now,
                            IsActive = false
                        };

                        _db.UserRepository.InsertUser(newUser);
                        _db.Save();
                        // پایان ثبت نام کاربر در سایت

                        // ارسال ایمیل فعالسازی

                        string bodyEmail = RazorConvertor.RenderPartialViewToString("Account", "_ActivationEmail", newUser);
                        SendEmail.SendActivationEmail(user.Email, "ایمیل فعالسازی", bodyEmail);

                        // پایان ارسال ایمیل فعالسازی

                        return View("SuccessRegister", newUser);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده تکراری می باشد !");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "نام کاربری تکراری می باشد !");
                }
            }

            return View(user);
        }


        // id = ActiveCode(کد فعالسازی کاربر)
        public ActionResult ActivateUser(string id)
        {
            User user = _db.UserRepository.GetUserByActiveCode(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            _db.Save();
            ViewBag.userName = user.UserName;

            return View();
        }

        #endregion

        #region Login

        [HttpGet]
        [Route("Login", Name = "LoginGet")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login", Name = "LoginPost")]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email ,Password ,RememberMe")] LoginViewModel user, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashPassword = _passwordHelper.EncodePasswordMd5(user.Password);
                User thisUser = _db.UserRepository.GetUserByEmailPass(FixedText.FixedEmail(user.Email), hashPassword);
                if (thisUser != null)
                {
                    if (thisUser.IsActive && !thisUser.IsDelete)
                    {
                        FormsAuthentication.SetAuthCookie(thisUser.UserId.ToString(), user.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما مسدود شده است !");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد !");
                }
            }

            return View(user);
        }

        #endregion

        #region Sign Out

        [Route("SignOut")]
        public ActionResult SignOut()
        {
            UserManager.SignOut();
            return Redirect("/");
        }

        #endregion

        #region Reset Password

        [HttpGet]
        [Route("ForgotPassword", Name = "ForgotPasswordGet")]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [Route("ForgotPassword", Name = "ForgotPasswordPost")]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword([Bind(Include = "Email")] ForgotPasswordViewModel userEmail)
        {
            if (ModelState.IsValid)
            {
                User thisUser = _db.UserRepository.GetUserByEmail(FixedText.FixedEmail(userEmail.Email));
                if (thisUser != null)
                {
                    if (thisUser.IsActive && !thisUser.IsDelete)
                    {
                        // ارسال ایمیل بازیابی کلمه عبور
                        string bodyEmail = RazorConvertor.RenderPartialViewToString("Account", "_RecoveryPasswordEmail", thisUser);
                        SendEmail.SendForgotPasswordEmail(thisUser.Email, "بازیابی کلمه عبور", bodyEmail);
                        // پایان ارسال ایمیل بازیابی کلمه عبور

                        return View("SuccessForgotPassword", thisUser);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست/مسدود است !");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد !");
                }
            }

            return View();
        }


        // id = ActiveCode(کد فعالسازی کاربر)
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword([Bind(Include = "Password, RePassword")] string id, ResetPasswordViewModel userPassword)
        {
            if (ModelState.IsValid)
            {
                User thisUser = _db.UserRepository.GetUserByActiveCode(id);
                if (thisUser == null)
                {
                    return HttpNotFound();
                }

                thisUser.Password = _passwordHelper.EncodePasswordMd5(userPassword.Password);
                thisUser.ActiveCode = Guid.NewGuid().ToString();
                _db.Save();

                return Redirect("/Login?resetPassword=true");
            }

            return View();
        }

        #endregion

        #region Manage Email

        //[ChildActionOnly]
        //public ActionResult ActivationEmail()
        //{
        //    return PartialView("_ActivationEmail");
        //}


        //[ChildActionOnly]
        //public ActionResult RecoveryPasswordEmail()
        //{
        //    return PartialView("_RecoveryPasswordEmail");
        //}

        #endregion

    }
}