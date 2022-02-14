using MyEshop.DomainClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel.Products
{
    public class FilterProductsViewModel
    {
        #region Pagging

        public int Take { get; set; } = 6;
        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Search Options

        public string State { get; set; } = "All";


        public string Title { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? FromDate { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? ToDate { get; set; }


        public int? ProductCode { get; set; }

        #endregion

        #region Returned Data

        public List<Product> Products { get; set; }

        #endregion
    }
}
