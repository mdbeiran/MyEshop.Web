using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class UserAddress
    {
        #region Ctor

        public UserAddress()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int AddressId { get; set; }


        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Province { get; set; }


        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string City { get; set; }


        [Display(Name = "بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Part { get; set; }


        [Display(Name = "محله")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Neighbourhood { get; set; }


        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string PostalAddress { get; set; }


        [Display(Name = "پلاک")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string Plaque { get; set; }


        [Display(Name = "واحد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]

        public string Unit { get; set; }


        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string PostalCode { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations



        #endregion

    }
}
