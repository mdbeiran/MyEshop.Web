using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class UserRole
    {
        #region Ctor

        public UserRole()
        {

        }

        #endregion

        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int RoleId { get; set; }


        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250)]
        public string RoleTitle { get; set; }


        [Display(Name = "عنوان سیستمی نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(150)]
        public string RoleName { get; set; }


        [Display(Name = "نقش پیشفرض")]
        public bool IsDefaultRole { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual List<User> Users { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
