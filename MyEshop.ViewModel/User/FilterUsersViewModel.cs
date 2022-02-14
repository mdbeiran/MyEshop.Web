using MyEshop.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.ViewModel.User
{
    public class FilterUsersViewModel
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

        public string State { get; set; } = "Active";
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? FromeDate { get; set; }
        public DateTime? ToDate { get; set; }

        #endregion

        #region Return Values

        public List<MyEshop.DomainClass.User> Users { get; set; }

        #endregion
    }
}
