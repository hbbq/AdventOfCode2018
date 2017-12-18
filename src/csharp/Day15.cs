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

    public class Day15
    {
        
        public static int Problem1()
        {

            long g1 = 516;
            long g2 = 190;

            long mod = 1 << 16;

            long mult1 = 16807;
            long mult2 = 48271;
            long mods = 2147483647;

            var loops = 40_000_000;
            var hit = 0;

            for(var loop = 1; loop <= loops; loop++)
            {

                if (loop % 1_000_000 == 0) Trace.WriteLine(loop);

                g1 *= mult1;
                g1 %= mods;

                g2 *= mult2;
                g2 %= mods;

                var s1 = g1 % mod;
                var s2 = g2 % mod;

                //Trace.WriteLine(Last16Bits(s1));
                //Trace.WriteLine(Last16Bits(s2));

                if (s1 == s2) hit++;

            }

            return hit;

        }

        public static int Problem2()
        {

            long g1 = 516;
            long g2 = 190;

            long mod = 1 << 16;

            long mult1 = 16807;
            long mult2 = 48271;
            long mods = 2147483647;

            var loops = 5_000_000;
            var hit = 0;

            for (var loop = 1; loop <= loops; loop++)
            {

                if (loop % 100_000 == 0) Trace.WriteLine(loop);

                a:
                g1 *= mult1;
                g1 %= mods;
                if (g1 % 4 != 0) goto a;
                        
                b:
                g2 *= mult2;
                g2 %= mods;
                if (g2 % 8 != 0) goto b;

                var s1 = g1 % mod;
                var s2 = g2 % mod;

                //Trace.WriteLine(Last16Bits(s1));
                //Trace.WriteLine(Last16Bits(s2));

                if (s1 == s2) hit++;

            }

            return hit;

        }

        private static string Last16Bits(long val) => Convert.ToString(val % (1 << 16), 2);

    }

}
