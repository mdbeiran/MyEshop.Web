using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.DomainClass
{
    public class UserProfile
    {
        #region Ctor

        public UserProfile()
        {

        }

        #endregion

        #region Properties

        [Key, ForeignKey("User")]
        public int UserId { get; set; }


        [Display(Name = "جنسیت")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Sex { get; set; }


        [Display(Name = "نشانی")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Address { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(250, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string FullName { get; set; }


        [Display(Name = "تلفن همراه")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        [RegularExpression(@"(0|\+98)?([ ]|-|[()]){0,2}9[0|1|2|3|4|9]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}",
                    ErrorMessage = "شماره موبایل وارد شده اشتباه می باشد.")]
        public string Mobile { get; set; }


        [Display(Name = "تلفن ثابت")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string PhoneNumber { get; set; }


        [Display(Name = "شماره ملی")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string NationalCode { get; set; }


        [Display(Name = "تاریخ تولد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "بیوگرافی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Bio { get; set; }


        [Display(Name = "شماره کارت")]
        [MaxLength(16, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string CardNumber { get; set; }


        [Display(Name = "تصویر")]
        [MaxLength(200)]
        public string UserImageName { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual User User { get; set; }

        #endregion

    }
}
