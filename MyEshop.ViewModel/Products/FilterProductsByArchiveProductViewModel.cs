using MyEshop.DomainClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel.Products
{
    public class FilterProductsByArchiveProductViewModel
    {
        #region Pagging

        public int Take { get; set; } = 9;
        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Search Options

        public string Sort { get; set; } = "All";


        public string ProductTitle { get; set; }


        public int? MinPrice { get; set; }


        public int? MaxPrice { get; set; }


        public List<int?> SelectedGroups { get; set; }

        #endregion

        #region Returned Data

        public List<Product> Products { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }

        #endregion
    }
}
