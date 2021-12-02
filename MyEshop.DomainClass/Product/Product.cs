using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class Product
    {
        #region Ctor

        public Product()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int ProductId { get; set; }


        [Display(Name = "برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int BrandId { get; set; }


        [Display(Name = "شناسه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int ProductCode { get; set; }


        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(400, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string ProductTitle { get; set; }


        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }


        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }


        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public string ProductImageName { get; set; }


        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int ProductCount { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int Price { get; set; }


        [Display(Name = "تعداد فروش")]
        public long SellCount { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "موجود هست")]
        public bool IsExist { get; set; }


        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual ProductBrand ProductBrand { get; set; }
        public virtual ICollection<ProductGallery> ProductGalleries { get; set; }
        public virtual ICollection<ProductTags> ProductTags { get; set; }
        public virtual ICollection<ProductSelectedGroup> ProductSelectedGroups { get; set; }
        public virtual ICollection<ProductSelectedFeatures> ProductSelectedFeatures { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }

        #endregion
    }
}
