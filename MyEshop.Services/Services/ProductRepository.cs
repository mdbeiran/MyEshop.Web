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
    using MyEshop.ViewModel.Products;
    using System.Globalization;

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
            return _context.Products.Where(p => p.IsActive && !p.IsDelete).OrderByDescending(p => p.ProductId);
        }

        public IEnumerable<Product> LastProducts()
        {
            return _context.Products.OrderByDescending(p => p.ProductId).Where(p => !p.IsDelete).Take(6);
        }

        public IEnumerable<Product> GetFilteredProducts(string searchQuery)
        {
            return _context.Products.Where(p => p.IsActive && !p.IsDelete && p.ProductTitle.Contains(searchQuery) || p.ShortDescription.Contains(searchQuery) || p.Text.Contains(searchQuery)).OrderByDescending(p => p.ProductId).Distinct();
        }

        public IEnumerable<Product> GetProductsByTagTitle(string tagTitle)
        {
            return _context.ProductTags.Where(t => t.TagTitle == tagTitle).Select(t => t.Product).Distinct();
        }

        public FilterProductsViewModel GetProductsByFilter(FilterProductsViewModel filter)
        {
            int take = filter.Take;
            int skip = (filter.PageId - 1) * take;

            FilterProductsViewModel data = new FilterProductsViewModel();
            IQueryable<Product> query = _context.Products;

            #region State

            switch (filter.State)
            {
                case "All":
                    {
                        break;
                    }
                case "Active":
                    {
                        query = query.Where(p => p.IsActive && p.IsDelete == false);
                        break;
                    }
                case "NotActive":
                    {
                        query = query.Where(p => p.IsActive == false && p.IsDelete == false);
                        break;
                    }
                case "Deleted":
                    {
                        query = query.Where(p => p.IsDelete);
                        break;
                    }
                case "IsExist":
                    {
                        query = query.Where(p => p.IsExist && !p.IsDelete);
                        break;
                    }
                case "NotExist":
                    {
                        query = query.Where(p => !p.IsExist && !p.IsDelete);
                        break;
                    }
            }

            #endregion

            #region Impelementing Filters

            if (filter.ProductCode != null && filter.ProductCode != 0)
            {
                query = query.Where(p => p.ProductCode == filter.ProductCode);
                data.ProductCode = filter.ProductCode;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(p => p.ProductTitle.Trim().ToLower().Contains(filter.Title.Trim().ToLower()));
                data.Title = filter.Title;
            }

            if (filter.FromDate != null)
            {
                DateTime fromDate = new DateTime(filter.FromDate.Value.Year, filter.FromDate.Value.Month, filter.FromDate.Value.Day, new PersianCalendar());
                query = query.Where(p => p.CreateDate >= fromDate);
                data.FromDate = filter.FromDate;
            }

            if (filter.ToDate != null)
            {
                DateTime toDate = new DateTime(filter.ToDate.Value.Year, filter.ToDate.Value.Month, filter.ToDate.Value.Day, new PersianCalendar());
                query = query.Where(p => p.CreateDate <= toDate);
                data.ToDate = filter.ToDate;
            }

            #endregion

            #region Pagging

            int thisPageCount = query.Count();
            if (thisPageCount % take > 0)
            {
                data.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                data.PageCount = thisPageCount / take;
            }

            data.ActivePage = filter.PageId;
            data.StartPage = filter.PageId - 3;
            data.EndPage = data.ActivePage + 3;
            if (data.StartPage <= 0) data.StartPage = 1;
            if (data.EndPage > data.PageCount) data.EndPage = data.PageCount;

            #endregion

            data.State = filter.State;
            data.Products = query.OrderByDescending(p => p.CreateDate).Skip(skip).Take(take).AsNoTracking().ToList();

            return data;
        }

        public IEnumerable<Product> GetProductsByTitle(string productTitle)
        {
            return _context.Products.Where(p => p.ProductTitle.Trim().ToLower().Contains(productTitle.Trim().ToLower()));
        }

        public FilterProductsByArchiveProductViewModel GetArchiveProductsByFilter(FilterProductsByArchiveProductViewModel filter)
        {
            int take = filter.Take;
            int skip = (filter.PageId - 1) * take;

            FilterProductsByArchiveProductViewModel data = new FilterProductsByArchiveProductViewModel();
            //List<Product> query = _context.Products.Where(p => p.IsActive && !p.IsDelete).ToList();
            List<Product> query = new List<Product>();

            #region State

            switch (filter.Sort)
            {
                case "allProduct":
                    {
                        break;
                    }
                case "PriceLowToHigh":
                    {
                        //query = query.Where(p => p.IsActive && p.IsDelete == false);
                        break;
                    }
                case "PriceHighToLow":
                    {
                        //query = query.Where(p => p.IsActive == false && p.IsDelete == false);
                        break;
                    }
            }

            #endregion

            #region Impelementing Filters

            if (filter.SelectedGroups != null)
            {
                foreach (int groupId in filter.SelectedGroups)
                {
                    query.AddRange(_context.ProductSelectedGroups.Where(p => p.ProductGroupId == groupId).Select(p => p.Product));
                    //products.AddRange(context.ProductSelectedGroups.Where(p => p.ProductGroupId == groupId).Select(p => p.Product));
                }
                query = query.Where(p => p.IsActive && !p.IsDelete).Distinct().ToList();
            }
            else
            {
                query = _context.Products.Where(p => p.IsActive && !p.IsDelete).ToList();
            }

            if (!string.IsNullOrEmpty(filter.ProductTitle))
            {
                query = query.Where(p => p.ProductTitle.Trim().ToLower().Contains(filter.ProductTitle.Trim().ToLower())).ToList();
                data.ProductTitle = filter.ProductTitle;
            }

            if (filter.MinPrice != 0 && filter.MinPrice != null)
            {
                query = query.Where(p => p.Price >= filter.MinPrice).ToList();
                data.MinPrice = filter.MinPrice;
            }

            if (filter.MaxPrice != 0 && filter.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice).ToList();
                data.MaxPrice = filter.MaxPrice;
            }

            #endregion

            #region Pagging

            int thisPageCount = query.Count();
            if (thisPageCount % take > 0)
            {
                data.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                data.PageCount = thisPageCount / take;
            }

            data.ActivePage = filter.PageId;
            data.StartPage = filter.PageId - 3;
            data.EndPage = data.ActivePage + 3;
            if (data.StartPage <= 0) data.StartPage = 1;
            if (data.EndPage > data.PageCount) data.EndPage = data.PageCount;

            #endregion

            data.Sort = filter.Sort;
            data.Products = query.OrderByDescending(p => p.CreateDate).Skip(skip).Take(take).ToList();
            data.ProductGroups = _context.ProductGroups.Where(g => !g.IsDelete).ToList();
            data.SelectedGroups = filter.SelectedGroups;

            return data;
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
