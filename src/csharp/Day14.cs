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

    public class Day14
    {
        
        public static int Problem1()
        {

            var input = "ugkiagan";

            var total = 0;

            for(var i = 0; i < 128; i++)
            {
                total += HexToBits(KnotHash($"{input}-{i}")).Count(e => e == 1);
            }

            return total;

        }

        public static int Problem2()
        {

            var input = "ugkiagan";

            var map = new int[128, 128];
            
            for (var i = 0; i < 128; i++)
            {
                var line = HexToBits(KnotHash($"{input}-{i}"));
                for(var j = 0; j < 128; j++)
                {
                    map[i, j] = line[j] * -1;
                }
            }

            var group = 0;

            for(var x = 0; x < 128; x++)
            {
                for(var y = 0; y < 128; y++)
                {
                    if(map[x,y] == -1)
                    {
                        group++;
                        Set(ref map, x, y, group);
                    }
                }
            }

            return group;

        }

        private static void Set(ref int[,] map, int x, int y, int val)
        {
            var q = new Queue<Tuple<int, int>>();
            q.Enqueue(new Tuple<int, int>(x, y));
            while (q.Any())
            {
                var t = q.Dequeue();
                map[t.Item1, t.Item2] = val;
                for (var test = 0; test < 4; test++)
                {
                    var x2 = t.Item1;
                    var y2 = t.Item2;
                    if (test == 0) x2--;
                    if (test == 1) y2--;
                    if (test == 2) x2++;
                    if (test == 3) y2++;
                    if (x2 < 0 || y2 < 0 || x2 > 127 || y2 > 127) continue;
                    if (map[x2, y2] == -1) q.Enqueue(new Tuple<int, int>(x2, y2));
                }
            }
        }

        private static int[] HexToBits(string hex)
        {
            var l = new List<int>();

            foreach(var c in hex.ToCharArray())
            {
                var p = "";
                switch (c.ToString())
                {
                    case "0": p = "0000"; break;
                    case "1": p = "0001"; break;
                    case "2": p = "0010"; break;
                    case "3": p = "0011"; break;
                    case "4": p = "0100"; break;
                    case "5": p = "0101"; break;
                    case "6": p = "0110"; break;
                    case "7": p = "0111"; break;
                    case "8": p = "1000"; break;
                    case "9": p = "1001"; break;
                    case "a": p = "1010"; break;
                    case "b": p = "1011"; break;
                    case "c": p = "1100"; break;
                    case "d": p = "1101"; break;
                    case "e": p = "1110"; break;
                    case "f": p = "1111"; break;
                }
                l.AddRange(p.ToCharArray().Select(e => int.Parse(e.ToString())));
            }

            return l.ToArray();

        }

        private static string KnotHash(string input)
        {

            var lengths = System.Text.Encoding.ASCII.GetBytes(input).Select(e => (int)e)
                .Concat(new int[] { 17, 31, 73, 47, 23 }).ToList();

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
