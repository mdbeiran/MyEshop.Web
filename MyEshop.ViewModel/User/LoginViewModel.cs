using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel
{
    public class LoginViewModel
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد !")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}
