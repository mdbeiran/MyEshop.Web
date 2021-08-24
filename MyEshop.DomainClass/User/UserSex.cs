using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class UserSex
    {
        #region Ctor

        public UserSex()
        {

        }

        #endregion

        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SexId { get; set; }


        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد !")]
        public string SexTitle { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations



        #endregion

    }
}
