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

    public class Day19
    {
        
        public static string Problem1(string input)
        {
            
            //input = 
            //    "     |          \n" +
            //    "     |  +--+\n" +
            //    "     A  |  C\n" +
            //    " F---|----E|--+\n" +
            //    "     |  |  |  D\n" +
            //    "     +B-+  +--+\n" +
            //    "";

            var map = new map(input);
            var x = 0;
            var y = 0;
            var d = 2;

            for(var i = 0; i < 1_000_000; i++)
            {
                if(map.CharAt(i, 0) == '|')
                {
                    x = i;
                    break;
                }
            }

            var collected = "";

            while (true)
            {

                switch (d)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

                var tc = map.CharAt(x, y);

                if (tc == '#') break;
                if (tc == ' ') break;

                switch (tc)
                {
                    case '|': break;
                    case '-': break;
                    case ' ': break;
                    case '+':
                        if (d != 0 && map.CharAt(x, y + 1).ToString().Replace("#", " ") != " ") d = 2;
                        else if (d != 1 && map.CharAt(x - 1, y).ToString().Replace("#", " ") != " ") d = 3;
                        else if (d != 2 && map.CharAt(x, y - 1).ToString().Replace("#", " ") != " ") d = 0;
                        else if (d != 3 && map.CharAt(x + 1, y).ToString().Replace("#", " ") != " ") d = 1;
                        break;
                    default:
                        collected += tc;
                        break;
                }

            }

            return collected;

        }

        public static int Problem2(string input)
        {

            //input =
            //    "     |          \n" +
            //    "     |  +--+\n" +
            //    "     A  |  C\n" +
            //    " F---|----E|--+\n" +
            //    "     |  |  |  D\n" +
            //    "     +B-+  +--+\n" +
            //    "";

            var map = new map(input);
            var x = 0;
            var y = 0;
            var d = 2;

            for (var i = 0; i < 1_000_000; i++)
            {
                if (map.CharAt(i, 0) == '|')
                {
                    x = i;
                    break;
                }
            }

            var steps = 1;
            var stepstolast = 0;

            while (true)
            {

                switch (d)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

                steps++;

                var tc = map.CharAt(x, y);

                if (tc == '#') break;
                if (tc == ' ') break;

                switch (tc)
                {
                    case '|': break;
                    case '-': break;
                    case ' ': break;
                    case '+':
                        if (d != 0 && map.CharAt(x, y + 1).ToString().Replace("#", " ") != " ") d = 2;
                        else if (d != 1 && map.CharAt(x - 1, y).ToString().Replace("#", " ") != " ") d = 3;
                        else if (d != 2 && map.CharAt(x, y - 1).ToString().Replace("#", " ") != " ") d = 0;
                        else if (d != 3 && map.CharAt(x + 1, y).ToString().Replace("#", " ") != " ") d = 1;
                        break;
                    default:
                        stepstolast = steps;
                        break;
                }

            }

            return stepstolast;

        }

        private class map
        {

            private char[,] chars;

            public map(string data)
            {

                var lines = data.Replace("\n", "\r").Replace("\r\r", "\r").Split('\r');
                var max = lines.Max(e => e.Length);
                chars = new char[max, lines.Count()];

                var y = -1;

                foreach(var line in lines)
                {
                    y++;
                    var ps = line.PadRight(max, ' ').ToCharArray();
                    var x = -1;
                    foreach(var c in ps)
                    {
                        x++;
                        chars[x, y] = c;
                    }
                }

            }

            public char CharAt(int x, int y)
            {
                try
                {
                    return chars[x, y];
                }
                catch
                {
                    return '#';
                }
            }

        }
        
    }

}
