using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace MyEshop.Web.Controllers
{

    using MyEshop.Services;
    using MyEshop.DomainClass;
    using MoreLinq;
    using MyEshop.ViewModel.Products;
    using MyEshop.Business;

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

        #region Show Product

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _db.ProductRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductFeatures = product.ProductSelectedFeatures.DistinctBy(f => f.FeatureId).Select(f =>
            new ShowProductFeatureViewModel()
            {
                FeatureTitle = f.ProductFeatures.FeatureTitle,
                Values = product.ProductSelectedFeatures.Where(fe => fe.FeatureId == f.FeatureId).Select(fe => fe.Value).ToList()
            }).ToList();

            return View(product);
        }

        #endregion

        #region Show Comments

        // id = productId
        public ActionResult ShowComments(int id)
        {
            var productComments = _db.ProductRepository.GetProductCommentsByProductId(id).ToList();

            return PartialView(productComments);
        }

        #endregion

        #region Create Comment
        
        // id = productId
        public ActionResult CreateComment(int id)
        {
            ProductComment productComment = new ProductComment()
            {
                ProductId = id
            };

            return PartialView(productComment);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(ProductComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                comment.UserId = UserManager.GetCurrentUserId();
                _db.ProductRepository.InsertComment(comment);
                _db.Save();

                var ProductComments = _db.ProductRepository.GetProductCommentsByProductId(comment.ProductId).ToList();
                return PartialView("ShowComments", ProductComments);
            }

            return PartialView(comment);
        }

        #endregion

    }
}