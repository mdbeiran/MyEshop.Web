using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class ProductSelectedFeatures
    {
        #region Ctor

        public ProductSelectedFeatures()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int PF_ID { get; set; }


        [Required]
        public int ProductId { get; set; }


        [Required]
        [Display(Name = "ویژگی")]
        public int FeatureId { get; set; }


        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Value { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual Product Product { get; set; }
        public virtual ProductFeatures ProductFeatures { get; set; }
        #endregion
    }
}
