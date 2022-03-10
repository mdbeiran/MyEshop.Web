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
    using MyEshop.DataLayer;

    public class ProductsController : Controller
    {

        #region Ctor

        private MyEshopUOW _db;
        MyEshopDbContext context = new MyEshopDbContext();
        public ProductsController(MyEshopUOW db)
        {
            _db = db;
        }

        #endregion
        
        #region Products Category

        // ParentId = null
        public ActionResult ProductsCategory(int? parentId)
        {
            IEnumerable<ProductGroup> mainGroup = _db.ProductRepository.GetGroups(parentId).ToList();

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

        #region Show Details Product

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
        public ActionResult CreateComment(ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                productComment.CreateDate = DateTime.Now;
                productComment.UserId = UserManager.GetCurrentUserId();
                _db.ProductRepository.InsertComment(productComment);
                _db.Save();

                IEnumerable<ProductComment> ProductComments = _db.ProductRepository.GetProductCommentsByProductId(productComment.ProductId).ToList();
                return PartialView("ShowComments", ProductComments);
            }

            return PartialView(productComment);
        }

        #endregion

        #region Show Products

        [Route("Shop")]
        public ActionResult ShowProducts(string searchQuery = "")
        {
            if (searchQuery != "")
            {
                var filteredProducts = _db.ProductRepository.GetFilteredProducts(searchQuery).ToList();
                var filteredProductsByTag = _db.ProductRepository.GetProductsByTagTitle(searchQuery).ToList();
                filteredProducts.AddRange(filteredProductsByTag);

                ViewBag.SearchQuery = searchQuery;
                return View(filteredProducts.Distinct());
            }

            IEnumerable<Product> products = _db.ProductRepository.GetProducts();
            return View(products);

        }

        #endregion

        #region Products Archive

        [Route("Archive")]
        public ActionResult ArchiveProducts(FilterProductsByArchiveProductViewModel filter)
        {
            FilterProductsByArchiveProductViewModel filterProducts = _db.ProductRepository.GetArchiveProductsByFilter(filter);

            return View(filterProducts);
        }

        #endregion

        #region Products Grid

        //public ActionResult ProductsGrid()
        //{
        //    return PartialView();
        //}

        #endregion

    }
}