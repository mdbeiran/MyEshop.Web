using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyEshop.Business
{
    public static class PathTools
    {
        #region Default

        public static readonly string UserAvatar = "/Images/DefaultImages/UserAvatarDefault.jpg";
        public static readonly string NoPhoto = "/Images/DefaultImages/no-photo.png";

        #endregion

        #region Products

        // Product
        public static readonly string ProductImagePath = "/Images/Products/Origin/";
        public static readonly string ProductImageThumbPath = "/Images/Products/Thumb/";

        // ProductGallery
        public static readonly string ProductImageGalleryPath = "/Images/ProductGallery/Origin/";
        public static readonly string ProductImageGalleryThumbPath = "/Images/ProductGallery/Thumb/";

        // Product FullPath
        public static readonly string ProductImageFullPath = HttpContext.Current.Server.MapPath("/Images/Products/Origin/");
        public static readonly string ProductImageThumbFullPath = HttpContext.Current.Server.MapPath("/Images/Products/Thumb/");

        // ProductGallery FullPath
        public static readonly string ProductImageGalleryFullPath = HttpContext.Current.Server.MapPath("/Images/ProductGallery/Origin/");
        public static readonly string ProductImageGalleryThumbFullPath = HttpContext.Current.Server.MapPath("/Images/ProductGallery/Thumb/");

        #endregion
    }
}
