using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Extentions
{
    public static class StringExtentions
    {
        public static string RemoveWhiteSpace(this string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Trim();
        }
        public static bool IsNullOrEmpty(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return true;
            else return false;
        }
       
    }
}
