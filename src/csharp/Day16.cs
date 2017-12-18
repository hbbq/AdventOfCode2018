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

    public class Day16
    {
        
        public static string Problem1(string input)
        {

            //input = "s1,x3/4,pe/b";

            var moves = input.Split(',');

            var size = 16;

            var programs = Enumerable.Range(0, size).Select(e => ((char)(65 + e)).ToString().ToLower()).ToArray();

            foreach(var move in moves)
            {

                if (move.StartsWith("s"))
                {
                    var x = int.Parse(move.Substring(1));
                    var nx = size - x;
                    programs = programs.Skip(nx).Take(x).Concat(programs.Take(nx)).ToArray();
                } else if (move.StartsWith("x"))
                {
                    var p = move.Substring(1).Split('/');
                    var n1 = int.Parse(p[0]);
                    var n2 = int.Parse(p[1]);
                    var o = programs[n1];
                    programs[n1] = programs[n2];
                    programs[n2] = o;
                }else if (move.StartsWith("p"))
                {
                    var p = move.Substring(1).Split('/');
                    var n1 = Array.IndexOf(programs, p[0]);
                    var n2 = Array.IndexOf(programs, p[1]);
                    var o = programs[n1];
                    programs[n1] = programs[n2];
                    programs[n2] = o;
                }

            }

            return string.Join("", programs);

        }

        public static string Problem2(string input)
        {

            //input = "s1,x3/4,pe/b";

            var moves = input.Split(',');

            var size = 16;

            var programs = Enumerable.Range(0, size).Select(e => ((char)(65 + e)).ToString().ToLower()).ToArray();
                      
            var oloops = 1_000_000_000;

            var l = new List<string>();
            var c = true;

            for (var oo = 0; oo < oloops; oo++)
            {

                if (oo % 1000 == 0) Trace.WriteLine(oo);


                foreach (var move in moves)
                {

                    if (move.StartsWith("s"))
                    {
                        var x = int.Parse(move.Substring(1));
                        var nx = size - x;
                        programs = programs.Skip(nx).Take(x).Concat(programs.Take(nx)).ToArray();
                    }
                    else if (move.StartsWith("x"))
                    {
                        var p = move.Substring(1).Split('/');
                        var n1 = int.Parse(p[0]);
                        var n2 = int.Parse(p[1]);
                        var o = programs[n1];
                        programs[n1] = programs[n2];
                        programs[n2] = o;
                    }
                    else if (move.StartsWith("p"))
                    {
                        var p = move.Substring(1).Split('/');
                        var n1 = Array.IndexOf(programs, p[0]);
                        var n2 = Array.IndexOf(programs, p[1]);
                        var o = programs[n1];
                        programs[n1] = programs[n2];
                        programs[n2] = o;
                    }

                }


                if (c)
                {

                    var st = string.Join("", programs);

                    if (l.Contains(st))
                    {
                        var ls = l.Count() - l.IndexOf(st);
                        while (oo + ls < oloops) oo += ls;
                        c = false;
                    }

                    l.Add(st);
                }

            }

            return string.Join("", programs);

        }
        
    }

}
