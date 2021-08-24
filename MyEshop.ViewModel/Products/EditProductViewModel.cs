using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyEshop.ViewModel
{

    using MyEshop.DomainClass;

    public class EditProductViewModel
    {

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
        [AllowHtml]
        public string Text { get; set; }


        [Display(Name = "تصویر")]
        public string ProductImageName { get; set; }


        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int ProductCount { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int Price { get; set; }


        [Display(Name = "موجود هست")]
        public bool IsExist { get; set; }


        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }


        [Display(Name = "کلمات کلیدی")]
        [MaxLength(70, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Tags { get; set; }


        public IEnumerable<ProductGroup> Groups { get; set; }

        public IEnumerable<int> SelectedGroups { get; set; }

    }
}
