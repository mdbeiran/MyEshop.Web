using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.DomainClass
{
    public class ProductGallery
    {
        #region Ctor

        public ProductGallery()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int GalleryId { get; set; }


        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int ProductId { get; set; }


        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string ImageTitle { get; set; }


        [Display(Name = "تصویر")]
        //[Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public string ImageName { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        #endregion
    }
}
