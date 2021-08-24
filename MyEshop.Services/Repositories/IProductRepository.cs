using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Services
{

    using MyEshop.DomainClass;
    using MyEshop.ViewModel;

    public interface IProductRepository : IDisposable
    {

        #region Groups

        void InsertGroup(ProductGroup group);
        void UpdateGroup(ProductGroup group);
        void DeleteGroup(int groupId);
        void ReturnGroup(int groupId);
        ProductGroup GetGroupByGroupId(int groupId);
        IEnumerable<ProductGroup> GetGroups(int? parentId);
        IEnumerable<ProductGroup> GetActiveGroups();
        //IEnumerable<ProductGroupViewModel> GetActiveGroupsForViewModel();
        bool IsExistUrlTitleInGroups(string nameInUrl);

        #endregion

        #region Products

        void InsertProduct(Product product);
        EditProductViewModel GetProductForEdit(int productId);
        void DeleteProduct(int productId);
        void ReturnProduct(int productId);
        Product GetProductById(int productId);
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> LastProducts();

        #endregion

        #region Product Brands

        void InsertProductBrand(ProductBrand productBrand);
        void UpdateProductBrand(ProductBrand productBrand);
        void DeleteProductBrand(int brandId);
        void ReturnProductBrand(int brandId);
        ProductBrand GetProductBrandById(int brandId);
        IEnumerable<ProductBrand> GetProductBrands();
        bool IsExistProductBrandWithBrandTitle(string title);
        IEnumerable<ProductBrand> GetActiveProductBrands();

        #endregion

        #region Product Selected Groups

        void AddGroupToProduct(int productId, int groupId);
        void DeleteProductSelectedGroups(int productId);
        IEnumerable<ProductSelectedGroup> GetProductSelectedGroups(int productId);

        #endregion

        #region Product Tags

        void InsertProductTags(int productId, string tagTitle);
        IEnumerable<ProductTags> GetProductTagsById(int productId);
        void DeleteProductTags(int productId);

        #endregion

        #region Product Galleries

        IEnumerable<ProductGallery> GetProductGalleriesByProductId(int productId);
        void InsertGalleryImage(ProductGallery gallery);
        ProductGallery GetProductGalleryById(int galleryId);
        void DeleteProductGallery(ProductGallery productGallery);

        #endregion

        #region Product Features

        void InsertFeature(ProductFeatures features);
        void UpdateFeature(ProductFeatures features);
        ProductFeatures GetProductFeatureById(int featureId);
        IEnumerable<ProductFeatures> GetProductFeatures();
        IEnumerable<ProductFeatures> GetActiveProductFeatures();

        #endregion

        #region Product Selected Features

        IEnumerable<ProductSelectedFeatures> GetSelectedFeaturesByProductId(int productId);
        void AddFeatureToProduct(ProductSelectedFeatures features);
        ProductSelectedFeatures GetSelectedFeatureById(int pfId);
        void UpdateProductSelectedFeature(ProductSelectedFeatures feature);

        #endregion
    }
}
