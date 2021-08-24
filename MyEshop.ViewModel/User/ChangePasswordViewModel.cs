using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }

    }
}
