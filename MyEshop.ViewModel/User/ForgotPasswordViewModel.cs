using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد !")]
        public string Email { get; set; }
    }
}
