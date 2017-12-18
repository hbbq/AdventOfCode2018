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

    public class Day18
    {
        
        public static long Problem1(string input)
        {

            //input = "set a 1\nadd a 2\nmul a a\nmod a 5\nsnd a\nset a 0\nrcv a\njgz a -1\nset a 1\njgz a -2";

            var mem = new mem();

            var lines = input.Replace("\n", "\r").Split('\r').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();

            long pos = 0;

            long lastSound = 0;

            while(true)
            {

                var op = pos;
                var line = lines[pos];

                var p = line.Split(' ');

                switch (p[0])
                {

                    case "snd":
                        lastSound = mem.Get(p[1]);
                        break;

                    case "set":
                        mem.Set(p[1], mem.Get(p[2]));
                        break;

                    case "add":
                        mem.Set(p[1], mem.Get(p[1]) + mem.Get(p[2]));
                        break;

                    case "mul":
                        mem.Set(p[1], mem.Get(p[1]) * mem.Get(p[2]));
                        break;

                    case "mod":
                        mem.Set(p[1], mem.Get(p[1]) % mem.Get(p[2]));
                        break;

                    case "rcv":
                        if (mem.Get(p[1]) != 0) return lastSound;
                        break;

                    case "jgz":
                        if(mem.Get(p[1]) > 0)
                        {
                            pos += mem.Get(p[2]);
                            pos--;
                        }
                        break;

                }

                pos++;

                Trace.WriteLine($"{op}: {line} ({pos})");
                Trace.WriteLine(mem.ToString());

            }

        }

        public static long Problem2(string input)
        {

            //input = "snd 1\nsnd 2\nsnd p\nrcv a\nrcv b\nrcv c\nrcv d";

            var q0t1 = new Queue<long>();
            var q1t0 = new Queue<long>();

            var pgm0 = new pgm(input, 0) { Input = q1t0, Output = q0t1 };
            var pgm1 = new pgm(input, 1) { Input = q0t1, Output = q1t0 };

            while (true)
            {

                pgm0.Step();
                pgm1.Step();

                if (pgm0.terminated && pgm1.terminated) break;
                if (pgm0.waitforinput && pgm1.waitforinput && !q0t1.Any() && !q1t0.Any()) break;

            }

            return pgm1.sendcount;

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

        private class pgm
        {

            private string[] lines;
            private long pos;
            private mem mem = new mem();
            public Queue<long> Input;
            public Queue<long> Output;
            public bool waitforinput = false;
            public bool terminated = false;
            public long sendcount = 0;
            
            public pgm(string program, int PID)
            {
                lines = program.Replace("\n", "\r").Split('\r').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
                mem.Set("p", PID);
            }

            public void Step()
            {

                if (terminated) return;

                var op = pos;
                var line = lines[pos];

                var p = line.Split(' ');

                switch (p[0])
                {

                    case "snd":
                        Output.Enqueue(mem.Get(p[1]));
                        sendcount++;
                        break;

                    case "set":
                        mem.Set(p[1], mem.Get(p[2]));
                        break;

                    case "add":
                        mem.Set(p[1], mem.Get(p[1]) + mem.Get(p[2]));
                        break;

                    case "mul":
                        mem.Set(p[1], mem.Get(p[1]) * mem.Get(p[2]));
                        break;

                    case "mod":
                        mem.Set(p[1], mem.Get(p[1]) % mem.Get(p[2]));
                        break;

                    case "rcv":
                        if (!Input.Any())
                        {
                            waitforinput = true;
                            return;
                        }
                        waitforinput = false;
                        mem.Set(p[1], Input.Dequeue());
                        break;

                    case "jgz":
                        if (mem.Get(p[1]) > 0)
                        {
                            pos += mem.Get(p[2]);
                            pos--;
                        }
                        break;

                }

                pos++;

                Trace.WriteLine($"{op}: {line} ({pos})");
                Trace.WriteLine(mem.ToString());

                if (pos < 0 || pos >= lines.Length) terminated = true;

            }

        }
        
    }

}
