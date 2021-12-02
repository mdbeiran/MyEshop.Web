using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEshop.DomainClass;

namespace MyEshop.DataLayer
{
    public class MyEshopDbContext : DbContext
    {
        #region Ctor

        public MyEshopDbContext() : base()
        {

        }

        #endregion

        #region Users

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Products

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSelectedGroup> ProductSelectedGroups { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductTags> ProductTags { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductSelectedFeatures> ProductSelectedFeatures { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }

        #endregion

    }
}
