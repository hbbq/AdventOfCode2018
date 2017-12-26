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

    public class Day24
    {
        
        public static int Problem1(string input)
        {

            //input = "0/2\r2/2\r2/3\r3/4\r3/5\r0/1\r10/1\r9/10";

            input = input.Replace("\n", "\r").Replace("\r\r", "\r");

            var components = input.Split('\r').Select(e => e.Split('/')).Select(e => new component(int.Parse(e[0]), int.Parse(e[1]))).ToList();

            var checks = new Queue<Tuple<int, int ,int[]>>();

            foreach(var c in components.Where(e => e.L==0 || e.R == 0))
            {
                checks.Enqueue(new Tuple<int, int, int[]>(0, 0, new int[] {}));
            }

            var max = 0;

            var l = 0;
            var lvl = 0;

            while (checks.Any())
            {


                l++;
                var cn = checks.Count();
                if (l % 10000 == 100 || cn < 500) Trace.WriteLine($"{l}: {cn} ({lvl} / {components.Count()})");

                var check = checks.Dequeue();
                lvl = check.Item3.Count();

                var subs = Enumerable.Range(0, components.Count()).Where(e => !check.Item3.Contains(e)).Select(e => new Tuple<int,component>(e, components[e])).Where(e => e.Item2.L == check.Item1 || e.Item2.R == check.Item1).ToList();

                if (!subs.Any())
                {
                    max = Math.Max(max, check.Item2);
                    continue;
                }

                foreach(var c in subs)
                {

                    var n = 0;

                    if (c.Item2.L == check.Item1)
                    {
                        n = c.Item2.R;
                    } else
                    {
                        n = c.Item2.L;
                    }

                    checks.Enqueue(new Tuple<int, int, int[]>(n, check.Item2 + c.Item2.L + c.Item2.R, check.Item3.Concat(new[] { c.Item1 }).ToArray()));

                }

            }

            return max;

        }

        public static int Problem2(string input)
        {

            //input = "0/2\r2/2\r2/3\r3/4\r3/5\r0/1\r10/1\r9/10";

            input = input.Replace("\n", "\r").Replace("\r\r", "\r");

            var components = input.Split('\r').Select(e => e.Split('/')).Select(e => new component(int.Parse(e[0]), int.Parse(e[1]))).ToList();

            var checks = new Queue<Tuple<int, int, int[]>>();

            foreach (var c in components.Where(e => e.L == 0 || e.R == 0))
            {
                checks.Enqueue(new Tuple<int, int, int[]>(0, 0, new int[] { }));
            }

            var max = 0;

            var l = 0;
            var lvl = 0;
            var lom = 0;

            while (checks.Any())
            {


                l++;
                var cn = checks.Count();
                if (l % 10000 == 100 || cn < 500) Trace.WriteLine($"{l}: {cn} ({lvl} / {components.Count()})");

                var check = checks.Dequeue();
                lvl = check.Item3.Count();

                var subs = Enumerable.Range(0, components.Count()).Where(e => !check.Item3.Contains(e)).Select(e => new Tuple<int, component>(e, components[e])).Where(e => e.Item2.L == check.Item1 || e.Item2.R == check.Item1).ToList();

                if (!subs.Any())
                {
                    if (lom != lvl) max = 0;
                    lom = lvl;
                    max = Math.Max(max, check.Item2);
                    continue;
                }

                foreach (var c in subs)
                {

                    var n = 0;

                    if (c.Item2.L == check.Item1)
                    {
                        n = c.Item2.R;
                    }
                    else
                    {
                        n = c.Item2.L;
                    }

                    checks.Enqueue(new Tuple<int, int, int[]>(n, check.Item2 + c.Item2.L + c.Item2.R, check.Item3.Concat(new[] { c.Item1 }).ToArray()));

                }

            }

            return max;

        }
        
        private class component
        {
            public int L;
            public int R;
            public component(int l, int r)
            {
                L = l;
                R = r;
            }
        }
        
    }

}
