using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class RolePermission
    {
        #region Ctor

        public RolePermission()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int RolePermissionId { get; set; }
        public int UserRoleId { get; set; }
        public int Permission { get; set; }

        #endregion

        #region Relations

        public virtual Permission UserPermission { get; set; }
        public virtual UserRole UserRole { get; set; }

        #endregion

    }
}
