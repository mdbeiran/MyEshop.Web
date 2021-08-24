using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Web.Areas.Admin.Controllers
{

    using MyEshop.DomainClass;
    using MyEshop.Services;
    using MyEshop.Utility;

    public class ManageUsersController : Controller
    {

        #region Ctor

        private MyEshopUOW _db;
        private IPasswordHelper _passwordHelper;

        public ManageUsersController(MyEshopUOW db, IPasswordHelper passwordHelper)
        {
            _db = db;
            _passwordHelper = passwordHelper;
        }

        #endregion

        #region Users

        #region Users List

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.UserRepository.GetUsers().ToList());
        }

        #endregion

        #region Create User

        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.RoleID = new SelectList(_db.UserRepository.GetActiveUserRoles(), "RoleID", "RoleTitle");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "UserId,RoleId,UserName,Email,Password,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                // ثبت کاربر
                if (!_db.UserRepository.IsExistUserWithUserName(user.UserName))
                {
                    if (!_db.UserRepository.IsExistUserWithEmail(user.Email))
                    {
                        User newUser = new User()
                        {

                            RoleId = user.RoleId,
                            UserName = user.UserName,
                            Email = FixedText.FixedEmail(user.Email),
                            ActiveCode = Guid.NewGuid().ToString(),
                            Password = _passwordHelper.EncodePasswordMd5(user.Password),
                            IsActive = user.IsActive,
                            RegisterDate = DateTime.Now,
                            ViewByAdmin = true
                        };
                        _db.UserRepository.InsertUser(newUser);
                        _db.Save();
                        // پایان ثبت کاربر

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است !");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "این نام کاربری قبلا استفاده شده است !");
                }
            }

            ViewBag.RoleId = new SelectList(_db.UserRepository.GetActiveUserRoles(), "RoleId", "RoleTitle", user.RoleId);
            return View(user);
        }

        #endregion

        #region Edit User

        //id = UserId
        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.UserRepository.GetUserForEditByUserId(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(_db.UserRepository.GetActiveUserRoles(), "RoleId", "RoleTitle", user.RoleId);
            return View("EditUser", user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "UserId,RoleId,UserName,Email,Password,ActiveCode,RegisterDate,IsActive,IsDelete,ViewByAdmin")] User editUser)
        {
            User user = _db.UserRepository.GetUserByUserId(editUser.UserId);
            if (ModelState.IsValid)
            {
                // ویرایش کاربر
                if (!_db.UserRepository.IsExistUserWithUserName(editUser.UserName) || user.UserName == editUser.UserName)
                {
                    if (!_db.UserRepository.IsExistUserWithEmail(editUser.Email) || user.Email == editUser.Email)
                    {
                        _db.UserRepository.UpdateUser(editUser);
                        _db.Save();
                        // پایان ویرایش کاربر

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است !");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "این نام کاربری قبلا استفاده شده است !");
                }
            }

            //ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "Sex", user.UserId);
            ViewBag.RoleId = new SelectList(_db.UserRepository.GetActiveUserRoles(), "RoleId", "RoleTitle", editUser.RoleId);
            return View(editUser);
        }

        #endregion

        #region Delete User

        // id = UserId
        public ActionResult DeleteUser(int id)
        {
            _db.UserRepository.DeleteUser(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }   
        
        #endregion

        #region Return User

        // id = GroupId
        public ActionResult ReturnUser(int id)
        {
            _db.UserRepository.ReturnUser(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
        
        #endregion

        #endregion


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }

}
