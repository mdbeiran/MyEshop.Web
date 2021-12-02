using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace MyEshop.Services
{

    using MyEshop.DomainClass;
    using MyEshop.DataLayer;
    using MyEshop.ViewModel;

    public class ProductRepository : IProductRepository
    {

        #region Ctor

        private MyEshopDbContext _context;
        public ProductRepository(MyEshopDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Groups

        public void InsertGroup(ProductGroup group)
        {
            _context.ProductGroups.Add(group);
        }

        public void UpdateGroup(ProductGroup group)
        {
            _context.Entry(group).State = EntityState.Modified;
        }

        public void DeleteGroup(int groupId)
        {
            ProductGroup productGroup = GetGroupByGroupId(groupId);
            productGroup.IsDelete = true;
        }

        public void ReturnGroup(int groupId)
        {
            ProductGroup productGroup = GetGroupByGroupId(groupId);
            productGroup.IsDelete = false;
        }

        public ProductGroup GetGroupByGroupId(int groupId)
        {
            return _context.ProductGroups.Find(groupId);
        }

        public IEnumerable<ProductGroup> GetGroups(int? parentId)
        {
            return _context.ProductGroups.Where(g => g.ParentID == parentId);
        }

        public IEnumerable<ProductGroup> GetActiveGroups()
        {
            return _context.ProductGroups.Where(g => !g.IsDelete);
        }

        public bool IsExistUrlTitleInGroups(string nameInUrl)
        {
            return _context.ProductGroups.Any(g => g.NameInUrl == nameInUrl);
        }

        //public List<ProductGroupViewModel> GetActiveGroupsForViewModel()
        //{
        //    return _context.ProductGroups.Where(g => !g.IsDelete).ToList();
        //}

        #endregion

        #region Products

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public EditProductViewModel GetProductForEdit(int productId)
        {
            Product currentProduct = GetProductById(productId);
            IEnumerable<ProductGroup> groups = GetActiveGroups();
            IEnumerable<ProductSelectedGroup> selectedGroups = GetProductSelectedGroups(productId);

            EditProductViewModel returnProduct = new EditProductViewModel()
            {
                ProductId = currentProduct.ProductId,
                BrandId = currentProduct.BrandId,
                ProductCode = currentProduct.ProductCode,
                IsActive = currentProduct.IsActive,
                IsExist = currentProduct.IsExist,
                Price = currentProduct.Price,
                ProductCount = currentProduct.ProductCount,
                ProductImageName = currentProduct.ProductImageName,
                ProductTitle = currentProduct.ProductTitle,
                ShortDescription = currentProduct.ShortDescription,
                Text = currentProduct.Text,
                Groups = groups.ToList(),
                SelectedGroups = selectedGroups.Select(g => g.ProductGroupId).ToList(),
                Tags = string.Join("، ", currentProduct.ProductTags.Select(t => t.TagTitle).ToList())
            };

            return returnProduct;
        }

        public void DeleteProduct(int productId)
        {
            Product product = GetProductById(productId);
            product.IsDelete = true;
        }

        public void ReturnProduct(int productId)
        {
            Product product = GetProductById(productId);
            product.IsDelete = false;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        public IEnumerable<Product> LastProducts()
        {
            return _context.Products.OrderByDescending(p => p.ProductId).Where(p => !p.IsDelete).Take(6);
        }

        #endregion

        #region Product Brands

        public void InsertProductBrand(ProductBrand productBrand)
        {
            _context.ProductBrands.Add(productBrand);
        }

        public void UpdateProductBrand(ProductBrand productBrand)
        {
            _context.Entry(productBrand).State = EntityState.Modified;
        }

        public void DeleteProductBrand(int brandId)
        {
            ProductBrand productBrand = GetProductBrandById(brandId);
            productBrand.IsDelete = true;
        }

        public void ReturnProductBrand(int brandId)
        {
            ProductBrand productBrand = GetProductBrandById(brandId);
            productBrand.IsDelete = false;
        }

        public ProductBrand GetProductBrandById(int brandId)
        {
            return _context.ProductBrands.Find(brandId);
        }

        public IEnumerable<ProductBrand> GetProductBrands()
        {
            return _context.ProductBrands;
        }

        public bool IsExistProductBrandWithBrandTitle(string title)
        {
            return _context.ProductBrands.Any(b => b.BrandTitle == title);
        }

        public IEnumerable<ProductBrand> GetActiveProductBrands()
        {
            return _context.ProductBrands.Where(b => !b.IsDelete);
        }

        #endregion

        #region Product Selected Groups

        public void AddGroupToProduct(int productId, int groupId)
        {
            ProductSelectedGroup productSelectedGroup = new ProductSelectedGroup()
            {
                ProductId = productId,
                ProductGroupId = groupId
            };
            _context.ProductSelectedGroups.Add(productSelectedGroup);
        }

        public void DeleteProductSelectedGroups(int productId)
        {
            IEnumerable<ProductSelectedGroup> selectedGroups = this.GetProductSelectedGroups(productId);
            foreach (ProductSelectedGroup selectedGroup in selectedGroups)
            {
                _context.Entry(selectedGroup).State = EntityState.Deleted;
            }
        }

        public IEnumerable<ProductSelectedGroup> GetProductSelectedGroups(int productId)
        {
            return _context.ProductSelectedGroups.Where(p => p.ProductId == productId);
        }

        #endregion

        #region Product Tags

        public void InsertProductTags(int productId, string tagTitle)
        {
            ProductTags newTag = new ProductTags()
            {
                ProductId = productId,
                TagTitle = tagTitle
            };
            _context.ProductTags.Add(newTag);
        }


        public IEnumerable<ProductTags> GetProductTagsById(int productId)
        {
            return _context.ProductTags.Where(t => t.ProductId == productId);
        }

        public void DeleteProductTags(int productId)
        {
            IEnumerable<ProductTags> productTags = this.GetProductTagsById(productId).ToList();
            foreach (ProductTags productTag in productTags)
            {
                _context.Entry(productTag).State = EntityState.Deleted;
            }
        }

        #endregion

        #region Product Galleries

        public IEnumerable<ProductGallery> GetProductGalleriesByProductId(int productId)
        {
            return _context.ProductGalleries.Where(g => g.ProductId == productId);
        }

        public void InsertGalleryImage(ProductGallery gallery)
        {
            _context.ProductGalleries.Add(gallery);
        }

        public ProductGallery GetProductGalleryById(int galleryId)
        {
            return _context.ProductGalleries.Find(galleryId);
        }

        public void DeleteProductGallery(ProductGallery productGallery)
        {
            _context.Entry(productGallery).State = EntityState.Deleted;
        }

        #endregion

        #region Product Features

        public void InsertFeature(ProductFeatures features)
        {
            _context.ProductFeatures.Add(features);
        }

        public void UpdateFeature(ProductFeatures features)
        {
            _context.Entry(features).State = EntityState.Modified;
        }

        public ProductFeatures GetProductFeatureById(int featureId)
        {
            return _context.ProductFeatures.Find(featureId);
        }

        public IEnumerable<ProductFeatures> GetProductFeatures()
        {
            return _context.ProductFeatures;
        }

        public IEnumerable<ProductFeatures> GetActiveProductFeatures()
        {
            return _context.ProductFeatures.Where(f => !f.IsDelete);
        }

        #endregion

        #region Product Selected Features

        public IEnumerable<ProductSelectedFeatures> GetSelectedFeaturesByProductId(int productId)
        {
            return _context.ProductSelectedFeatures.Where(pf => pf.ProductId == productId);
        }

        public void AddFeatureToProduct(ProductSelectedFeatures features)
        {
            _context.ProductSelectedFeatures.Add(features);
        }

        public ProductSelectedFeatures GetSelectedFeatureById(int pfId)
        {
            return _context.ProductSelectedFeatures.Find(pfId);
        }

        public void UpdateProductSelectedFeature(ProductSelectedFeatures feature)
        {
            _context.Entry(feature).State = EntityState.Modified;
        }

        #endregion

        #region Product Comments

        public void InsertComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
        }

        public IEnumerable<ProductComment> GetProductCommentsByProductId(int productId)
        {
            return _context.ProductComments.Where(c => c.ProductId == productId && c.IsDelete == false);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _context.Dispose();
        }

        



























































        #endregion



    }
}
