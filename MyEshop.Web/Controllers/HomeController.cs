using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor



        #endregion

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Slider

        #region ShowSlider

        public ActionResult ShowSlider()
        {
            return PartialView();
        }

        #endregion

        #endregion

        #region About Us

        public ActionResult AboutUs()
        {
            return View();
        }

        #endregion

        #region Contact Us

        [Route("contactus/{id?}")]
        public ActionResult ContactUs(int? id)
        {
            ViewBag.age = id;

            return View();
        }

        #endregion

        #region Footer

        public ActionResult Footer()
        {
            return PartialView();
        }

        #endregion

    }
}