using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class Permission
    {
        #region Ctor

        public Permission()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int PermissionId { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250)]
        public string Title { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید !")]
        [MaxLength(250)]
        public string Name { get; set; }


        public int? ParentId { get; set; }
        public int? DisplayPriority { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public virtual List<Permission> Permissions { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
