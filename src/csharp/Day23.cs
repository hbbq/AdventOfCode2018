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

    public class Day23
    {
        
        public static int Problem1(string input)
        {
            
            var mem = new mem();

            var lines = input.Replace("\n", "\r").Split('\r').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();

            long pos = 0;

            var mulcount = 0;
            var timesincemul = 0;
            var l = 0;

            while (timesincemul < 1_000_000 && pos >= 0 && pos < lines.Count())
            {

                var op = pos;
                var line = lines[pos];

                var p = line.Split(' ');

                timesincemul++;

                switch (p[0])
                {
                    
                    case "set":
                        mem.Set(p[1], mem.Get(p[2]));
                        break;

                    case "sub":
                        mem.Set(p[1], mem.Get(p[1]) - mem.Get(p[2]));
                        break;

                    case "mul":
                        mem.Set(p[1], mem.Get(p[1]) * mem.Get(p[2]));
                        mulcount++;
                        timesincemul = 0;
                        break;

                    case "jnz":
                        if (mem.Get(p[1]) != 0)
                        {
                            pos += mem.Get(p[2]);
                            pos--;
                        }
                        break;

                }

                pos++;

                //Trace.WriteLine($"{op}: {line} ({pos})");
                //Trace.WriteLine(mem.ToString());

                l++;
                if(l % 1000 == 0) Trace.WriteLine($"{l}: {timesincemul}");
                

            }

            return mulcount;

        }

        public static long Problem2(string input)
        {

            var b = 106500;
            var c = 123500;
            var h = 0;

            while (true)
            {

                var f = 1;

                for (var d = 2; d < b; d++)
                {
                    
                    if (b % d == 0)
                    {
                        f = 0;
                        break;
                    }

                }

                if (f == 0) h++;

                if (b == c) return h;

                b += 17;

            }

        }

        private class mem
        {

            private Dictionary<string, long> dict = new Dictionary<string, long>();
            public long Get(string name) => long.TryParse(name, out var x) ? x : (dict.ContainsKey(name) ? dict[name] : 0);
            public void Set(string name, long value)
            {
                if (dict.ContainsKey(name))
                {
                    dict[name] = value;
                }
                else
                {
                    dict.Add(name, value);
                }
            }
            public override string ToString() => string.Join(", ", dict.Select(e => $"{e.Key}:{e.Value}"));

        }

    }

}
