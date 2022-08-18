using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uygomanTest
{
    public static class ConnectionString
    {
       public static string UYConnectionString =ConfigurationManager.ConnectionStrings["uygomanCS"].ConnectionString;
    }
}
