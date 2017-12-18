using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using csharp.Properties;

namespace csharp
{

    public class Day17
    {
        
        public static int Problem1()
        {

            var input = 355;

            var count = 2017;

            var list = new List<int> { 0 };

            var pos = 0;

            for(var num = 1; num <= count; num++)
            {
                pos = (pos + input) % num;
                list.Insert(pos + 1, num);
                pos++;
            }

            return list[pos + 1];

        }

        public static int Problem2()
        {

            var input = 355;

            long count = 50_000_000;
                        
            var pos = 0;
            var last1 = 0;

            for (var num = 1; num <= count; num++)
            {
                if (num % 100_000 == 0) Trace.WriteLine(num);
                pos = (pos + input) % num;
                pos++;
                if (pos == 1) last1 = num;
            }

            return last1;

        }
        
    }

}
