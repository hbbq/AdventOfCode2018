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

    public class Day12
    {

        public static int Problem1(string input)
        {

            //input = "0 <-> 2\n1 <-> 1\n2 <-> 0, 3, 4\n3 <-> 2, 4\n4 <-> 2, 3, 6\n5 <-> 6\n6 <-> 4, 5";

            input = input.Replace("<->", ":").Replace(" ", "").Replace("\n", "\r");

            var lines = input.Split('\r');

            var pipes = new Dictionary<int, List<int>>();

            foreach (var line in lines)
            {
                if (line == "") continue;
                var p = line.Split(':');
                var i = int.Parse(p[0]);
                pipes.Add(i, p[1].Split(',').Select(e => int.Parse(e)).ToList());
            }

            var picked = new List<int>();

            var q = new Queue<int>();
            q.Enqueue(0);

            while(q.Count > 0)
            {
                var t = q.Dequeue();

                if (picked.Contains(t)) continue;

                picked.Add(t);

                foreach(var i in pipes[t])
                {
                    q.Enqueue(i);
                }

            }

            return picked.Count;

        }

        public static int Problem2(string input)
        {

            //input = "0 <-> 2\n1 <-> 1\n2 <-> 0, 3, 4\n3 <-> 2, 4\n4 <-> 2, 3, 6\n5 <-> 6\n6 <-> 4, 5";

            input = input.Replace("<->", ":").Replace(" ", "").Replace("\n", "\r");

            var lines = input.Split('\r');

            var pipes = new Dictionary<int, List<int>>();

            foreach (var line in lines)
            {
                if (line == "") continue;
                var p = line.Split(':');
                var i = int.Parse(p[0]);
                pipes.Add(i, p[1].Split(',').Select(e => int.Parse(e)).ToList());
            }

            var picked = new List<int>();

            var g = 0;

            while (pipes.Any())
            {

                g++;

                var q = new Queue<int>();
                q.Enqueue(pipes.Select(e => e.Key).First());

                while (q.Count > 0)
                {
                    var t = q.Dequeue();

                    if (picked.Contains(t)) continue;

                    picked.Add(t);

                    foreach (var i in pipes[t])
                    {
                        q.Enqueue(i);
                    }

                    pipes.Remove(t);

                }

            }

            return g;

        }
        
    }

}
