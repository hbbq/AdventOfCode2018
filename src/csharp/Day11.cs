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

    public class Day11
    {

        public static int Problem1(string input)
        {

            //input = "ne,ne,ne,se,sw,se,sw,ne,ne,s,s,sw,ne,ne,sw,sw";

            var steps = input.Split(',').Select(e => e.Trim()).ToList();

            steps = Reduce(steps);

            return steps.Count;

        }

        public static int Problem2(string input)
        {

            //input = "ne,ne,ne,se,sw,se,sw,ne,ne,s,s,sw,ne,ne,sw,sw";

            var steps = input.Split(',').Select(e => e.Trim()).ToList();

            var max = 0;

            var l = new List<string>();

            for (var i = 0; i < steps.Count; i++)
            {
                l.Add(steps[i]);
                l = Reduce(l);
                max = Math.Max(max, l.Count);
            }
            
            return max;

        }

        private static List<string> Reduce(List<string> steps)
        {
            var redo = true;

            while (redo)
            {
                redo = false;

                if (Join(steps, "n", "se", "ne")) redo = true;
                if (Join(steps, "n", "s", "")) redo = true;
                if (Join(steps, "n", "sw", "nw")) redo = true;

                if (Join(steps, "ne", "nw", "n")) redo = true;
                if (Join(steps, "ne", "s", "se")) redo = true;
                if (Join(steps, "ne", "sw", "")) redo = true;

                if (Join(steps, "se", "n", "ne")) redo = true;
                if (Join(steps, "se", "nw", "")) redo = true;
                if (Join(steps, "se", "sw", "s")) redo = true;

                if (Join(steps, "s", "ne", "se")) redo = true;
                if (Join(steps, "s", "n", "")) redo = true;
                if (Join(steps, "s", "nw", "sw")) redo = true;

                if (Join(steps, "sw", "se", "s")) redo = true;
                if (Join(steps, "sw", "ne", "")) redo = true;
                if (Join(steps, "sw", "n", "nw")) redo = true;

                if (Join(steps, "nw", "s", "sw")) redo = true;
                if (Join(steps, "nw", "se", "")) redo = true;
                if (Join(steps, "nw", "ne", "n")) redo = true;

            }
            return steps;
        }

        private static bool Join(List<string> list, string value1, string value2, string joinas)
        {
            var removed = false;
            while (list.Contains(value1) && list.Contains(value2))
            {
                RemoveOne(list, value1);
                RemoveOne(list, value2);
                list.Add(joinas);
                removed = true;
            }
            while(list.Contains("")) RemoveOne(list, "");
            //Trace.WriteLine(ListInfo(list) + " - " + string.Join(",", list));
            return removed;
        }

        private static void RemoveOne<T>(List<T> list, T value)
        {
            list.RemoveAt(list.IndexOf(value));
        }

        private static string ListInfo(List<string> list)
        {
            var dist = list.Distinct().ToList();
            return string.Join(", ", dist.Select(e => $"{e} x{list.Count(x => x == e)}"));
        }
        
    }

}
