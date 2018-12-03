using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{

    public static class Day1
    {

        public static int Problem1(int[] sequence)
        {

            var freq = 0;
            var freqs = new List<int>();
            freqs.Add(freq);

            while(true)
            {

                foreach(var n in sequence)
                {
                    freq += n;
                    if (freqs.Contains(freq)) return freq;
                    freqs.Add(freq);
                }

            }

        }

    }

}
