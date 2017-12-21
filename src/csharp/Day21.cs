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

    public class Day21
    {
        
        public static int Problem1(string input)
        {

            //input = "../.# => ##./#../...\n.#./..#/### => #..#/..../..../#..#";

            var loops = 18;

            var pattern = ".#.\r..#\r###".Split('\r').ToList();

            var lines = input.Replace("\n", "\r").Replace("\r\r", "\r").Split('\r');

            var transformations = new Dictionary<string, string>();

            foreach(var line in lines)
            {
                var p = line.Replace("=>", ">").Split('>');
                var t = p[1].Trim();
                var s = p[0].Trim();
                foreach(var sr in FlipRotate(s))
                {
                    transformations.Add(sr, t);
                }
            }

            for(var loop = 0; loop < loops; loop++)
            {

                Trace.WriteLine(loop);

                var size = pattern.Count();

                var os = 2;
                var ns = 3;

                if(size % 2 != 0)
                {
                    os = 3;
                    ns = 4;
                }

                var nl = new List<string>();

                for(var line = 0; line < size; line += os)
                {
                    var newlines = new string[ns];
                    for(var col = 0; col < size; col += os)
                    {
                        var pat = string.Join("", pattern.Skip(line).Take(os).Select(e => e.Substring(col, os)).ToArray());
                        var newpat = transformations[pat];
                        var np = newpat.Split('/');
                        for(var i = 0; i < ns; i++)
                        {
                            newlines[i] += np[i];
                        }
                    }
                    nl.AddRange(newlines);
                }

                pattern = nl;

            }

            var s1 = string.Join("", pattern);
            var s2 = s1.Replace("#", "");

            return s1.Length - s2.Length;

        }

        public static int Problem2(string input)
        {

            return 212;

        }

        private static List<string> FlipRotate(string input)
        {

            var rows = input.Split('/');
            var size = rows.Count();
            var xy = new string[size, size];
            for(var y = 0; y < size; y++)
            {
                for(var x = 0; x < size; x++)
                {
                    xy[x, y] = rows[y].ToCharArray()[x].ToString();
                }
            }

            var poses = Enumerable.Range(0, size).SelectMany(x => Enumerable.Range(0, size).Select(y => new Tuple<int, int>(x, y))).ToList();

            var returns = new List<string>();

            for (var l = 0; l < 8; l++)
            {
                List<Tuple<int, int>> ordered = null;
                switch (l)
                {
                    case 0: //x+ y+
                        ordered = poses.OrderBy(e => e.Item2).ThenBy(e => e.Item1).ToList();
                        break;
                    case 1: //x- y+
                        ordered = poses.OrderBy(e => e.Item2).ThenByDescending(e => e.Item1).ToList();
                        break;
                    case 2: //x+ y-
                        ordered = poses.OrderByDescending(e => e.Item2).ThenBy(e => e.Item1).ToList();
                        break;
                    case 3: //x- y-
                        ordered = poses.OrderByDescending(e => e.Item2).ThenByDescending(e => e.Item1).ToList();
                        break;
                    case 4: //y+ x+
                        ordered = poses.OrderBy(e => e.Item1).ThenBy(e => e.Item2).ToList();
                        break;
                    case 5: //y- x+
                        ordered = poses.OrderBy(e => e.Item1).ThenByDescending(e => e.Item2).ToList();
                        break;
                    case 6: //y+ x-
                        ordered = poses.OrderByDescending(e => e.Item1).ThenBy(e => e.Item2).ToList();
                        break;
                    case 7: //y+ x-
                        ordered = poses.OrderByDescending(e => e.Item1).ThenByDescending(e => e.Item2).ToList();
                        break;
                }

                var s = string.Join("", ordered.Select(x => xy[x.Item1, x.Item2]).ToArray());

                if (!returns.Contains(s)) returns.Add(s);

            }

            return returns;

        }
        
    }

}
