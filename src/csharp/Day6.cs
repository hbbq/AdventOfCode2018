using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{

    public class Day6
    {

        public static int Problem1()
        {

            var input = "5	1	10	0	1	7	13	14	3	12	8	10	7	12	0	6";
            var banks = input.Replace("\t", " ").Split(' ').Select(int.Parse).ToArray();

            var history = new List<string>
            {
                BanksToSignature(banks)
            };

            var count = 0;

            while (true)
            {
                count++;
                var max = banks.Max();
                var index = banks.TakeWhile(x => x != max).Count();
                var blocks = banks[index];
                banks[index] = 0;
                for (var i = 0; i < blocks; i++)
                {
                    var ni = (index + 1 + i) % banks.Length;
                    banks[ni]++;
                }
                var signature = BanksToSignature(banks);
                if (history.Contains(signature)) return count;
                history.Add(signature);
            }

        }
        public static int Problem2()
        {

            var input = "5	1	10	0	1	7	13	14	3	12	8	10	7	12	0	6";
            var banks = input.Replace("\t", " ").Split(' ').Select(int.Parse).ToArray();

            var history = new List<string>
            {
                BanksToSignature(banks)
            };

            var count = 0;

            while (true)
            {
                count++;
                var max = banks.Max();
                var index = banks.TakeWhile(x => x != max).Count();
                var blocks = banks[index];
                banks[index] = 0;
                for (var i = 0; i < blocks; i++)
                {
                    var ni = (index + 1 + i) % banks.Length;
                    banks[ni]++;
                }
                var signature = BanksToSignature(banks);
                if (history.Contains(signature)) return count - history.IndexOf(signature);
                history.Add(signature);
            }

        }

        private static string BanksToSignature(int[] banks) => string.Join(";", banks.Select(x => x.ToString()).ToArray());
        
    }

}
