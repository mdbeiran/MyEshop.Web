using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class ProductFeatures
    {
        #region Ctor

        public ProductFeatures()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int FeatureId { get; set; }


        [Display(Name = "گروه")]
        public int? GroupId { get; set; }


        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string FeatureTitle { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual ICollection<ProductSelectedFeatures> ProductSelectedFeatures { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        #endregion
    }
}
