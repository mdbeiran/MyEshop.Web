using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MyEshop.Web.Areas.Admin.Controllers
{

    using MyEshop.DataLayer;
    using MyEshop.DomainClass;
    using MyEshop.Services;
    using MyEshop.ViewModel;
    using MyEshop.Utility;
    using MyEshop.Business;
    using MyEshop.ViewModel.Products;

    public class ManageProductsController : Controller
    {

        #region Ctor

        private MyEshopUOW _db;
        public ManageProductsController(MyEshopUOW db)
        {
            _db = db;
        }

        #endregion

        #region Specific Methods

        private void AddImageToServer(HttpPostedFileBase image, string imageName, string originalPath, int? width, int? height, string thumbPath = null, string deleteImageName = null)
        {
            if (image != null)
            {
                // Delete Image
                if (!string.IsNullOrEmpty(deleteImageName))
                {
                    if (System.IO.File.Exists(originalPath + deleteImageName))
                    {
                        System.IO.File.Delete(originalPath + deleteImageName);
                    }

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (System.IO.File.Exists(thumbPath + deleteImageName))
                        {
                            System.IO.File.Delete(thumbPath + deleteImageName);
                        }
                    }
                }


                // Add Image
                image.SaveAs(originalPath + imageName);
                if (!string.IsNullOrEmpty(thumbPath))
                {
                    image.SaveAs(thumbPath + imageName);
                    ImageResizer img = new ImageResizer();
                    if (width != null && height != null)
                    {
                        img.ResizeImageFromFile(thumbPath + imageName, height.Value, width.Value, false, false);
                    }
                    else
                    {
                        img.ResizeImageFromFile(thumbPath + imageName, 200, 200, false, false);
                    }
                }

            }
        }

        #endregion

        #region Product Groups

        #region Index

        // id = ParentId
        public ActionResult ProductGroups(int? id)
        {
            if (id != null)
                ViewBag.parentId = id.Value;

            IEnumerable<ProductGroup> groups = _db.ProductRepository.GetGroups(id).ToList();
            return View(groups);
        }

        #endregion

        #region Create Group

        // id = GroupId
        [HttpGet]
        public ActionResult CreateGroup(int? id)
        {
            ViewBag.UrlError = false;
            if (id != null)
                ViewBag.parentId = id.Value;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup([Bind(Include = "GroupID,GroupTitle,ParentId,NameInUrl")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                if (!_db.ProductRepository.IsExistUrlTitleInGroups(productGroup.NameInUrl))
                {
                    _db.ProductRepository.InsertGroup(productGroup);
                    _db.Save();
                    return RedirectToAction("ProductGroups", "ManageProducts", new { id = productGroup.ParentID });
                }
                else
                {
                    ViewBag.UrlError = true;
                }
            }

            return View(productGroup);
        }

        #endregion

        #region Edit Group

        public ActionResult EditGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = _db.ProductRepository.GetGroupByGroupId(id.Value);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrlError = false;
            return View(productGroup);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup([Bind(Include = "GroupID,GroupTitle,ParentID,NameInUrl,IsDelete")] ProductGroup productGroup, string oldUrlName)
        {
            if (ModelState.IsValid)
            {
                if (!_db.ProductRepository.IsExistUrlTitleInGroups(productGroup.NameInUrl) || productGroup.NameInUrl == oldUrlName)
                {
                    _db.ProductRepository.UpdateGroup(productGroup);
                    _db.Save();
                    return RedirectToAction("ProductGroups", "ManageProducts", new { id = productGroup.ParentID });
                }
                else
                {
                    ViewBag.UrlError = true;
                }
            }
            return View(productGroup);
        }

        #endregion

        #region Delete Group

        // id = GroupId
        public ActionResult DeleteGroup(int id)
        {
            _db.ProductRepository.DeleteGroup(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Return Group

        // id = GroupId
        public ActionResult ReturnGroup(int id)
        {
            _db.ProductRepository.ReturnGroup(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #endregion

        #region Products

        #region Index

        public ActionResult Index(FilterProductsViewModel filter)
        {
            filter.Take = 15;
            FilterProductsViewModel filterProducts = _db.ProductRepository.GetProductsByFilter(filter);

            return View(filterProducts);
        }

        #endregion

        #region Create Product

        [HttpGet]
        public ActionResult CreateProduct()
        {
            ViewBag.SelectedGroupsError = false;
            ViewBag.IsImageError = false;
            IEnumerable<ProductGroup> groups = _db.ProductRepository.GetActiveGroups();
            CreateProductViewModel newProduct = new CreateProductViewModel();
            newProduct.Groups = groups.ToList();
            newProduct.SelectedGroups = new List<int>();
            List<ProductBrand> brands = new List<ProductBrand>()
            {
                new ProductBrand() {BrandId = 0, BrandTitle = "-- لطفا برند محصول را انتخاب نمایید --" }
            };
            brands.AddRange(_db.ProductRepository.GetActiveProductBrands().ToList());
            ViewBag.BrandId = new SelectList(brands, "BrandId", "BrandTitle");
            //ViewBag.Groups = _db.ProductRepository.GetActiveProductGroups().ToList();
            return View(newProduct);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(/*[Bind(Include ="")]*/ CreateProductViewModel newProduct, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region Check Product Groups

                if (newProduct.SelectedGroups == null)
                {
                    ViewBag.SelectedGroupsError = true;
                    newProduct.Groups = _db.ProductRepository.GetActiveGroups().ToList();
                    newProduct.SelectedGroups = new List<int>();
                    List<ProductBrand> brands = new List<ProductBrand>()
                    {
                        new ProductBrand() {BrandId = 0, BrandTitle = "-- لطفا برند محصول را انتخاب نمایید --" }
                    };
                    brands.AddRange(_db.ProductRepository.GetActiveProductBrands().ToList());
                    ViewBag.BrandId = new SelectList(brands, "BrandId", "BrandTitle");
                    return View(newProduct);
                }

                #endregion

                #region Add Image

                newProduct.ProductImageName = "no-photo.png";
                if (productImage != null)
                {
                    if (productImage.IsImage())
                    {
                        string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(productImage.FileName);
                        AddImageToServer(productImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath);
                        newProduct.ProductImageName = imageName;
                    }
                    else
                    {
                        ViewBag.IsImageError = true;
                        newProduct.Groups = _db.ProductRepository.GetActiveGroups().ToList();
                        newProduct.SelectedGroups = new List<int>();
                        List<ProductBrand> brands = new List<ProductBrand>()
                        {
                            new ProductBrand() {BrandId = 0, BrandTitle = "-- لطفا برند محصول را انتخاب نمایید --" }
                        };
                        brands.AddRange(_db.ProductRepository.GetActiveProductBrands().ToList());
                        ViewBag.BrandId = new SelectList(brands, "BrandId", "BrandTitle");
                        return View(newProduct);
                    }
                }

                #endregion

                #region Add Product

                Product product = new Product()
                {
                    BrandId = newProduct.BrandId,
                    CreateDate = DateTime.Now,
                    IsActive = newProduct.IsActive,
                    IsExist = newProduct.IsExist,
                    Price = newProduct.Price,
                    ProductCode = newProduct.ProductCode,
                    ProductCount = newProduct.ProductCount,
                    ProductImageName = newProduct.ProductImageName,
                    ProductTitle = newProduct.ProductTitle,
                    SellCount = 0,
                    ShortDescription = FixedText.ConvertNewLineToBr(newProduct.ShortDescription),
                    Text = newProduct.Text
                };
                _db.ProductRepository.InsertProduct(product);
                //_db.Save();

                #endregion

                #region Add Product Groups

                foreach (int groupId in newProduct.SelectedGroups)
                {
                    _db.ProductRepository.AddGroupToProduct(product.ProductId, groupId);
                }
                _db.Save();

                #endregion

                #region Add Tags

                if (newProduct.Tags != null)
                {
                    string[] tags = FixedText.SplitTags(newProduct.Tags);
                    foreach (string tag in tags)
                    {
                        _db.ProductRepository.InsertProductTags(product.ProductId, FixedText.FixedTag(tag));
                    }
                }

                #endregion

                _db.Save();

                return RedirectToAction("Index", "ManageProducts", new { area = "Admin" });
            }

            ViewBag.BrandId = _db.ProductRepository.GetActiveProductBrands().ToList();
            return View(newProduct);
        }
        #endregion

        #region Edit Product

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditProductViewModel editProduct = _db.ProductRepository.GetProductForEdit(id.Value);
            editProduct.ShortDescription = FixedText.ConvertBrToNewLine(editProduct.ShortDescription);

            if (editProduct.ProductTitle == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedGroupsError = false;
            ViewBag.IsImageError = false;
            //ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
            ViewBag.BrandId = new SelectList(_db.ProductRepository.GetActiveProductBrands(), "BrandId", "BrandTitle", editProduct.BrandId);
            return View(editProduct);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(/*[Bind(Include ="")]*/ EditProductViewModel editProduct, HttpPostedFileBase productImage)
        {
            Product mainProduct = _db.ProductRepository.GetProductById(editProduct.ProductId);

            if (ModelState.IsValid)
            {
                #region Check Product Groups

                if (editProduct.SelectedGroups != null)
                {
                    _db.ProductRepository.DeleteProductSelectedGroups(mainProduct.ProductId);
                    foreach (int group in editProduct.SelectedGroups)
                    {
                        _db.ProductRepository.AddGroupToProduct(mainProduct.ProductId, group);
                    }
                }
                else
                {
                    ViewBag.SelectedGroupsError = true;
                    //ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
                    EditProductViewModel returnEditProduct = _db.ProductRepository.GetProductForEdit(editProduct.ProductId);
                    returnEditProduct.ShortDescription = FixedText.ConvertBrToNewLine(editProduct.ShortDescription);
                    ViewBag.BrandId = new SelectList(_db.ProductRepository.GetActiveProductBrands(), "BrandId", "BrandTitle", editProduct.BrandId);
                    return View(returnEditProduct);
                }

                #endregion

                #region Edit Image

                if (productImage != null)
                {

                    if (productImage.IsImage())
                    {
                        string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(productImage.FileName);
                        AddImageToServer(productImage, imageName, PathTools.ProductImageFullPath, 200, 200, PathTools.ProductImageThumbFullPath, editProduct.ProductImageName);
                        editProduct.ProductImageName = imageName;
                    }
                    else
                    {
                        EditProductViewModel returnEditProduct = _db.ProductRepository.GetProductForEdit(editProduct.ProductId);
                        returnEditProduct.ShortDescription = FixedText.ConvertBrToNewLine(editProduct.ShortDescription);
                        ViewBag.BrandId = new SelectList(_db.ProductRepository.GetActiveProductBrands(), "BrandId", "BrandTitle", editProduct.BrandId);
                        ViewBag.IsImageError = true;
                        return View(returnEditProduct);
                    }
                }

                #endregion

                #region Edit Product Information

                mainProduct.BrandId = editProduct.BrandId;
                mainProduct.IsActive = editProduct.IsActive;
                mainProduct.IsExist = editProduct.IsExist;
                mainProduct.Price = editProduct.Price;
                mainProduct.ProductCode = editProduct.ProductCode;
                mainProduct.ProductCount = editProduct.ProductCount;
                mainProduct.ProductImageName = editProduct.ProductImageName;
                mainProduct.ProductTitle = editProduct.ProductTitle;
                mainProduct.ShortDescription = FixedText.ConvertNewLineToBr(editProduct.ShortDescription);
                mainProduct.Text = editProduct.Text;

                #endregion

                #region Manage Tags

                if (editProduct.Tags != null)
                {
                    _db.ProductRepository.DeleteProductTags(mainProduct.ProductId);
                    string[] tags = FixedText.SplitTags(editProduct.Tags);
                    foreach (string tag in tags)
                    {
                        _db.ProductRepository.InsertProductTags(mainProduct.ProductId, FixedText.FixedTag(tag));
                    }
                }

                #endregion

                _db.Save();
                return RedirectToAction("Index", "ManageProducts", new { area = "Admin" });

            }

            //ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
            ViewBag.BrandId = new SelectList(_db.ProductRepository.GetActiveProductBrands(), "BrandId", "BrandTitle", editProduct.BrandId);
            return View(editProduct);
        }

        #endregion

        #region Delete Product

        public ActionResult DeleteProduct(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _db.ProductRepository.DeleteProduct(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Return Product

        public ActionResult ReturnProduct(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _db.ProductRepository.ReturnProduct(id);
            _db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Show Produtcts

        public ActionResult ShowProducts()
        {
            return PartialView();
        }

        #endregion

        #endregion

        #region Product Brands

        #region Index

        public ActionResult ProductBrands()
        {
            IEnumerable<ProductBrand> productBrands = _db.ProductRepository.GetProductBrands();
            return View(productBrands);
        }

        #endregion

        #region Create Brand

        [HttpGet]
        public ActionResult CreateBrand()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand([Bind(Include = "BrandId, BrandTitle")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                if (!_db.ProductRepository.IsExistProductBrandWithBrandTitle(productBrand.BrandTitle))
                {
                    _db.ProductRepository.InsertProductBrand(productBrand);
                    _db.Save();
                    return RedirectToAction("ProductBrands");
                }
                else
                {
                    ModelState.AddModelError("BrandTitle", "عنوان برند تکراری می باشد !");
                }

            }

            return View();
        }

        #endregion

        #region Edit Brand

        [HttpGet]
        public ActionResult EditBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = _db.ProductRepository.GetProductBrandById(id.Value);
            if (productBrand == null)
            {
                return HttpNotFound();
            }

            ViewBag.brandError = false;
            return PartialView(productBrand);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand([Bind(Include = "BrandId, BrandTitle, IsDelete")] ProductBrand productBrand, string oldBrandTitle)
        {
            if (ModelState.IsValid)
            {
                if (!_db.ProductRepository.IsExistProductBrandWithBrandTitle(productBrand.BrandTitle) || productBrand.BrandTitle == oldBrandTitle)
                {
                    _db.ProductRepository.UpdateProductBrand(productBrand);
                    _db.Save();
                    return RedirectToAction("ProductBrands", "ManageProducts");
                }
                else
                {
                    ViewBag.brandError = true;
                }

            }

            return View(productBrand);
        }

        #endregion

        #region Delete Brand

        // id = BrandId
        public ActionResult DeleteBrand(int id)
        {
            _db.ProductRepository.DeleteProductBrand(id);
            _db.Save();

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Return Brand

        // id = BrandId
        public ActionResult ReturnBrand(int id)
        {
            _db.ProductRepository.ReturnProductBrand(id);
            _db.Save();

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #endregion

        #region Product Gallery

        #region Create ProductGallery

        // id = ProductId
        [HttpGet]
        public ActionResult CreateProductGallery(int id)
        {
            ViewBag.ProductImageGalleryError = false;
            ViewBag.ImageEmptyError = false;
            ProductGallery productGallery = new ProductGallery()
            {
                ProductId = id
            };
            ViewBag.ProductGalleries = _db.ProductRepository.GetProductGalleriesByProductId(id).ToList();

            return View(productGallery);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductGallery(ProductGallery gallery, HttpPostedFileBase productGallery)
        {
            if (ModelState.IsValid)
            {
                if (productGallery != null)
                {
                    if (productGallery.IsImage())
                    {
                        string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(productGallery.FileName);
                        AddImageToServer(productGallery, imageName, PathTools.ProductImageGalleryFullPath, 100, 100, PathTools.ProductImageGalleryThumbFullPath);
                        gallery.ImageName = imageName;
                        _db.ProductRepository.InsertGalleryImage(gallery);
                        _db.Save();

                        return RedirectToAction("CreateProductGallery", "ManageProducts", new { area = "Admin", id = gallery.ProductId });
                    }
                    else
                    {
                        ViewBag.ProductImageGalleryError = true;
                        ViewBag.ProductGalleries = _db.ProductRepository.GetProductGalleriesByProductId(gallery.ProductId).ToList();
                    }
                }
                else
                {
                    ViewBag.ProductGalleries = _db.ProductRepository.GetProductGalleriesByProductId(gallery.ProductId).ToList();
                    ViewBag.ImageEmptyError = true;
                }

            }

            return View(gallery);
        }

        #endregion

        #region Delete ProductGallery

        // id = GalleryId
        public ActionResult DeleteProductGallery(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ProductGallery productGallery = _db.ProductRepository.GetProductGalleryById(id);
                if (productGallery != null)
                {
                    if (System.IO.File.Exists(PathTools.ProductImageGalleryFullPath + productGallery.ImageName))
                    {
                        System.IO.File.Delete(PathTools.ProductImageGalleryFullPath + productGallery.ImageName);
                    }

                    if (System.IO.File.Exists(PathTools.ProductImageGalleryThumbFullPath + productGallery.ImageName))
                    {
                        System.IO.File.Delete(PathTools.ProductImageGalleryThumbFullPath + productGallery.ImageName);
                    }

                    _db.ProductRepository.DeleteProductGallery(productGallery);
                    _db.Save();

                    return new HttpStatusCodeResult(HttpStatusCode.Accepted);
                }

                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
        }

        #endregion

        #endregion

        #region Features

        #region Index

        public ActionResult ProductFeatures()
        {
            IEnumerable<ProductFeatures> features = _db.ProductRepository.GetProductFeatures().ToList();
            ViewBag.GroupId = new SelectList(_db.ProductRepository.GetActiveGroups().ToList(), "GroupId", "GroupTitle");

            return View(features);
        }

        #endregion

        #region Create Feature

        [HttpGet]
        public ActionResult CreateFeature()
        {
            ViewBag.GroupId = new SelectList(_db.ProductRepository.GetActiveGroups().ToList(), "GroupId", "GroupTitle");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFeature(ProductFeatures feature)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.InsertFeature(feature);
                _db.Save();

                return RedirectToAction("ProductFeatures", "ManageProducts", new { area = "Admin" });
            }

            return View(feature);
        }



        #endregion

        #region Edit Feature

        [HttpGet]
        public ActionResult EditFeature(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductFeatures features = _db.ProductRepository.GetProductFeatureById(id.Value);
            if (features == null)
            {
                return HttpNotFound();
            }

            ViewBag.GroupId = new SelectList(_db.ProductRepository.GetActiveGroups().ToList(), "GroupId", "GroupTitle", features.GroupId);
            return View(features);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFeature(ProductFeatures editFeature)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.UpdateFeature(editFeature);
                _db.Save();

                return RedirectToAction("ProductFeatures", "ManageProducts", new { area = "Admin" });
            }

            return View(editFeature);
        }

        #endregion

        #region Delete Feature

        public ActionResult DeleteFeature(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductFeatures features = _db.ProductRepository.GetProductFeatureById(id.Value);
            if (features != null)
            {
                features.IsDelete = true;
                _db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Return Feature

        public ActionResult ReturnFeature(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductFeatures features = _db.ProductRepository.GetProductFeatureById(id.Value);
            if (features != null)
            {
                features.IsDelete = false;
                _db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #endregion

        #region Product Feature

        #region Create ProductFeature

        // id = ProductId
        [HttpGet]
        public ActionResult CreateProductFeature(int id)
        {
            ProductSelectedFeatures productSelectedFeatures = new ProductSelectedFeatures()
            {
                ProductId = id
            };
            ViewBag.ProductSelectedFeatures = _db.ProductRepository.GetSelectedFeaturesByProductId(id).ToList();
            ViewBag.FeatureId = new SelectList(_db.ProductRepository.GetActiveProductFeatures().ToList(), "FeatureId", "FeatureTitle");
            return View(productSelectedFeatures);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductFeature(ProductSelectedFeatures features)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.AddFeatureToProduct(features);
                _db.Save();

                return RedirectToAction("CreateProductFeature", "ManageProducts", new { area = "Admin", id = features.ProductId });
            }

            return View();
        }

        #endregion

        #region Edit ProductFeature

        [HttpGet]
        public ActionResult EditProductFeature(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSelectedFeatures features = _db.ProductRepository.GetSelectedFeatureById(id.Value);
            if (features==null)
            {
                return HttpNotFound();
            }
            ViewBag.FeatureId = new SelectList(_db.ProductRepository.GetActiveProductFeatures().ToList(), "FeatureId", "FeatureTitle",features.FeatureId);
            return View(features);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductFeature(ProductSelectedFeatures editFeature)
        {
            if (ModelState.IsValid)
            {
                _db.ProductRepository.UpdateProductSelectedFeature(editFeature);
                _db.Save();
                return RedirectToAction("CreateProductFeature", "ManageProducts", new { area = "Admin", id = editFeature.ProductId });
            }

            return View();
        }

        #endregion

        #region Delete ProductFeature

        // id = PFId
        public ActionResult DeleteProductFeature(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSelectedFeatures selectedFeature = _db.ProductRepository.GetSelectedFeatureById(id.Value);
            if (selectedFeature != null)
            {
                selectedFeature.IsDelete = true;
                _db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Return ProductFeature

        // id = PFId
        public ActionResult ReturnProductFeature(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSelectedFeatures selectedFeature = _db.ProductRepository.GetSelectedFeatureById(id.Value);
            if (selectedFeature != null)
            {
                selectedFeature.IsDelete = false;
                _db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
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
