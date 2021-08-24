using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Utility
{
    public interface IPasswordHelper
    {
        string  EncodePasswordMd5(string password);
    }
}
