using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.DomainClass
{
    public class ProductGroup
    {
        #region Ctor

        public ProductGroup()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int GroupID { get; set; }


        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string GroupTitle { get; set; }


        [Display(Name = "زیر گروه ها")]
        public int? ParentID { get; set; }


        [Display(Name = "عنوان در URL")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string NameInUrl { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ParentID")]
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public virtual ICollection<ProductSelectedGroup> ProductSelectedGroups { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }

        #endregion

    }
}
