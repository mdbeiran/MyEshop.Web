using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.DomainClass
{
    public class ProductSelectedGroup
    {
        #region Ctor

        public ProductSelectedGroup()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int PG_Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductGroupId { get; set; }

        #endregion

        #region Relations

        public virtual Product Product { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        #endregion
    }
}
