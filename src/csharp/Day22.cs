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

    public class Day22
    {
        
        public static int Problem1(string input)
        {

            //input = "..#\r#..\r...";

            var loops = 10000;

            var map = new bool[20000, 20000];

            var x = 10000;
            var y = 10000;
            var d = 0;

            var lines = input.Replace("\n", "\r").Replace("\r\r", "\r").Replace(" ", "").Split('\r');

            var size = lines.Count();

            var mod = 10000 - (size - 1) / 2;

            for(var py = 0; py < lines.Count(); py++)
            {
                var chars = lines[py].ToCharArray();
                for(var px = 0; px < lines.Count(); px++)
                {
                    if (chars[px] == '#') map[mod+ px, mod + py] = true;
                }
            }

            var infections = 0;

            for(var loop = 0; loop < loops; loop++)
            {

                if (loop % 100 == 0) Trace.WriteLine(loop);

                if (map[x, y]) { d = (d + 1) % 4; } else { d = (d + 3) % 4; }

                map[x, y] = !map[x, y];

                if (map[x, y]) infections++;

                switch (d)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

            }

            return infections;

        }

        public static int Problem2(string input)
        {

            //input = "..#\r#..\r...";

            var loops = 10_000_000;

            var map = new map();

            var x = 0;
            var y = 0;
            var d = 0;

            var lines = input.Replace("\n", "\r").Replace("\r\r", "\r").Replace(" ", "").Split('\r');

            var size = lines.Count();

            var mod = 0 - (size - 1) / 2;

            for (var py = 0; py < lines.Count(); py++)
            {
                var chars = lines[py].ToCharArray();
                for (var px = 0; px < lines.Count(); px++)
                {
                    if (chars[px] == '#') map.SetValue(mod + px, mod + py, 2);
                }
            }

            var infections = 0;

            for (var loop = 0; loop < loops; loop++)
            {

                if(loop % 100_000 == 0) Trace.WriteLine(loop);

                var state = map.GetValue(x,y);

                switch (state)
                {
                    case 0: d = (d + 3) % 4; break;
                    case 1: infections++; break;
                    case 2: d = (d + 1) % 4; break;
                    case 3: d = (d + 2) % 4; break;

                }

                map.SetValue(x, y, (state + 1) % 4);
                
                switch (d)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

            }

            return infections;

        }

        private class map
        {
            private Dictionary<Tuple<int, int>, int> dta = new Dictionary<Tuple<int, int>, int>();
            public int GetValue(int x, int y)
            {
                var tp = new Tuple<int, int>(x, y);
                if (dta.ContainsKey(tp)) return dta[tp];
                return 0;
            }
            public void SetValue(int x, int y, int value)
            {
                var tp = new Tuple<int, int>(x, y);
                if (dta.ContainsKey(tp))
                {
                    dta[tp] = value;
                }
                else
                {
                    dta.Add(tp, value);
                }
            }
        }
        
    }

}
