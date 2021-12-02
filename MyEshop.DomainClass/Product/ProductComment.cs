using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class ProductComment
    {
        #region Ctor

        public ProductComment()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int ProductCommentId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }


        [Display(Name ="متن نظر شما")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }


        [Display(Name = "تاریخ ثبت")]
        
        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }
        public int? ParentId { get; set; }

        #endregion

        #region Relations

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("ParentId")]
        public virtual ICollection<ProductComment> ProductComments { get; set; }

        #endregion
    }
}
