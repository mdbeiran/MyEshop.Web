using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel
{
    public class ProductGroupViewModel
    {
        public int? GroupId { get; set; }
        public string GroupTitle { get; set; }
        public int? ParentID { get; set; }
        public string NameInUrl { get; set; }
        public bool IsDelete { get; set; }
    }
}
