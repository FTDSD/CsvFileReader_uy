using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uygomanTest
{
   public  class Validation
    {
        public bool ValidateFilePath(string path)
        {
         string pattern= @"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$";
         Regex reg =new Regex(pattern,RegexOptions.Compiled|RegexOptions.IgnoreCase);
         return reg.IsMatch(path);

        }
    }
}
