using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{

    public static class Ext
    {

        public static string[] GetLines(this string @this)
        {
            return @this.Replace("\n", "\r").Split('\r').Where(e => e != "").Select(e => e.Trim()).ToArray();
        }

        public static string[] GetWords(this string @this)
        {
            return @this.Split(' ').Where(e => e != "").ToArray();
        }

    }

}
