using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class ProductBrand
    {
        #region Ctor

        public ProductBrand()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int BrandId { get; set; }


        [Display(Name = "عنوان برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string BrandTitle { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual ICollection<Product> Products { get; set; }

        #endregion
    }
}
