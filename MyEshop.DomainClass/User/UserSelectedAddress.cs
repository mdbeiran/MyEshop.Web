using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DomainClass
{
    public class UserSelectedAddress
    {
        #region Ctor

        public UserSelectedAddress()
        {

        }

        #endregion

        #region Properties

        public int UserAddress { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        #endregion

        #region Relations



        #endregion

    }
}
