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

    public class Day13
    {
        
        public static int Problem1(string input)
        {

            //input = "0: 3\n1: 2\n4: 4\n6: 4";

            input = input.Replace(" ", "").Replace("\n", "\r");

            var lines = input.Split('\r');

            var layers = new Dictionary<int, Layer>();

            foreach (var line in lines)
            {
                if (line == "") continue;
                var p = line.Split(':');
                layers.Add(int.Parse(p[0]), new Layer { index = int.Parse(p[0]), range = int.Parse(p[1]) });
            }

            var mx = layers.Keys.Max();

            var total = 0;

            for (var pos = 0; pos <= mx; pos++)
            {
                if (layers.ContainsKey(pos) && layers[pos].pos == 0) total += layers[pos].severity;
                foreach (var layer in layers.Values)
                {
                    layer.Move();
                }
            }

            return total;

        }

        public static int Problem2(string input)
        {

            //input = "0: 3\n1: 2\n4: 4\n6: 4";

            input = input.Replace(" ", "").Replace("\n", "\r");

            var lines = input.Split('\r');

            var layers = new Dictionary<int, int>();

            foreach(var line in lines)
            {
                if (line == "") continue;
                var p = line.Split(':');
                layers.Add(int.Parse(p[0]), int.Parse(p[1]) * 2 - 2);
            }

            var mx = layers.Keys.Max();

            var delay = 0;

            while (true)
            {

                var total = 0;

                if(delay % 1000 == 0) System.Diagnostics.Trace.WriteLine(delay);
                
                for (var pos = 0; pos <= mx; pos++)
                {
                    if (layers.ContainsKey(pos) && (pos + delay) % layers[pos] == 0) total += 1;
                    if (total > 0) break;
                }

                if (total == 0) return delay;

                delay++;

            }
            
        }

        private class Layer
        {
            public int index;
            public int range;
            public int severity => index * range;
            private bool down = true;
            public int pos = 0;
            public void Reset()
            {
                pos = 0;
                down = true;
            }
            public void Move()
            {
                if (down)
                {
                    pos++;
                    if (pos >= range - 1) down = false;
                }
                else
                {
                    pos--;
                    if (pos <= 0) down = true;
                }
            }
        }

    }

}
