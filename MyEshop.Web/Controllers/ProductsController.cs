using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Web.Controllers
{

    using MyEshop.Services;
    using MyEshop.DomainClass;

    public class ProductsController : Controller
    {

        #region Ctor

        private MyEshopUOW _db;
        public ProductsController(MyEshopUOW db)
        {
            _db = db;
        }

        #endregion

        #region Products Category
        
        // id = null
        public ActionResult ProductsCategory(int? id)
        {
            IEnumerable<ProductGroup> mainGroup = _db.ProductRepository.GetGroups(id).ToList();
            return PartialView(mainGroup);
        }

        #endregion

        #region Last Products

        public ActionResult LastProducts()
        {
            IEnumerable<Product> lastProduct = _db.ProductRepository.LastProducts().ToList();
            return PartialView(lastProduct);
        }

        #endregion

    }
}