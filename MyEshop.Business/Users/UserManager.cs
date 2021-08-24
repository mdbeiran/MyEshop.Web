using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;

namespace MyEshop.Business
{
    public class UserManager
    {
        public static int GetCurrentUserId()
        {
            return int.Parse(HttpContext.Current.User.Identity.Name);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
