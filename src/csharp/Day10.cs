using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using csharp.Properties;

namespace csharp
{

    public class Day10
    {

        public static int Problem1()
        {

            var input = "183,0,31,146,254,240,223,150,2,206,161,1,255,232,199,88";

            var lengths = input.Split(',').Select(e => int.Parse(e.Trim())).ToArray();

            var list = Enumerable.Range(0, 256).ToList();

            var skip = 0;
            var jumped = 0;

            foreach (var length in lengths)
            {
                if (length > list.Count) continue;
                if (length == list.Count)
                {
                    list.Reverse();
                }
                else
                {
                    var sublist = list.Take(length).ToList();
                    sublist.Reverse();
                    sublist.AddRange(list.Skip(length));
                    list = sublist;
                }
                var jump = (length + skip) % list.Count;
                list = list.Skip(jump).Concat(list.Take(jump)).ToList();
                jumped += jump;
                skip++;
            }

            jumped %= list.Count;

            jumped = (list.Count - jumped) % list.Count;

            list = list.Skip(jumped).Concat(list.Take(jumped)).ToList();

            return list[0] * list[1];

        }

        public static string Problem2()
        {

            var input = "183,0,31,146,254,240,223,150,2,206,161,1,255,232,199,88";

            var lengths = System.Text.Encoding.ASCII.GetBytes(input).Select(e => (int) e)
                .Concat(new int[] {17, 31, 73, 47, 23}).ToList();

            var list = Enumerable.Range(0, 256).ToList();

            var skip = 0;
            var jumped = 0;

            for (var round = 0; round < 64; round++)
            {
                foreach (var length in lengths)
                {
                    if (length > list.Count) continue;
                    if (length == list.Count)
                    {
                        list.Reverse();
                    }
                    else
                    {
                        var sublist = list.Take(length).ToList();
                        sublist.Reverse();
                        sublist.AddRange(list.Skip(length));
                        list = sublist;
                    }
                    var jump = (length + skip) % list.Count;
                    list = list.Skip(jump).Concat(list.Take(jump)).ToList();
                    jumped += jump;
                    skip++;
                }
            }

            jumped %= list.Count;

            jumped = (list.Count - jumped) % list.Count;

            list = list.Skip(jumped).Concat(list.Take(jumped)).ToList();

            var hash = "";

            for (var pos = 0; pos < list.Count; pos += 16)
            {
                var nums = list.Skip(pos).Take(16).ToList();
                var xo = nums.Aggregate(0, (current, num) => current ^ num);
                hash += xo.ToString("X2");
            }

            return hash.ToLower();

        }
        
    }

}
