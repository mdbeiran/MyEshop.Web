using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEshop.DomainClass
{
    public class User
    {
        #region Ctor

        public User()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int UserId { get; set; }


        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        public int RoleId { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد !")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Password { get; set; }


        [Display(Name = "کد فعالسازی")]
        [MaxLength(50)]
        public string ActiveCode { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [Required]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime RegisterDate { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool ViewByAdmin { get; set; }

        #endregion

        #region Relations

        public virtual UserRole UserRole { get; set; }
        public virtual UserProfile Profile { get; set; }

        #endregion

    }
}
