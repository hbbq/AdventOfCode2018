using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{

    public class Day7
    {

        public static string Problem1(string input)
        {
           
            var lines = input.Replace("\n", "\r").Split('\r').Where(e => e != "").ToList();

            var subs = lines.Where(e => e.Contains("->")).Select(e => e.Replace("->", ">").Split('>')[1].Trim())
                .SelectMany(e => e.Split(',').Select(o => o.Trim())).ToList();

            var mains = lines.Select(e => e.Split('(')[0].Trim()).ToList();

            var head = mains.Where(e => !subs.Contains(e)).ToList();

            return head.Single();

        }

        public static int Problem2(string input)
        {

            //input = "pbga (66)\rxhth (57)\rebii (61)\rhavc (66)\rktlj (57)\rfwft (72) -> ktlj, cntj, xhth\rqoyq (66)\rpadx (45) -> pbga, havc, qoyq\rtknk (41) -> ugml, padx, fwft\rjptl (61)\rugml (68) -> gyxo, ebii, jptl\rgyxo (61)\rcntj (57)";

            var lines = input.Replace("\n", "\r").Split('\r').Where(e => e != "").ToList();

            var progs = new Dictionary<string, Prog>();

            foreach (var line in lines)
            {
                var p = new Prog();
                var ps = line.Replace("->", ">").Split('>');
                var ps2 = ps[0].Split('(');
                p.name = ps2[0].Trim();
                p.weight = int.Parse(ps2[1].Split(')')[0]);
                p.subsstring =
                    ps.Length == 1
                        ? ""
                        : ps[1].Replace(" ", "");
                progs.Add(p.name, p);
            }

            foreach (var p in progs.Values)
            {
                if (p.subsstring != "")
                {
                    foreach (var s in p.subsstring.Split(','))
                    {
                        p.subs.Add(progs[s]);
                    }
                }
            }

            var subs = lines.Where(e => e.Contains("->")).Select(e => e.Replace("->", ">").Split('>')[1].Trim())
                .SelectMany(e => e.Split(',').Select(o => o.Trim())).ToList();

            var mains = lines.Select(e => e.Split('(')[0].Trim()).ToList();

            var head = progs[mains.Single(e => !subs.Contains(e))];

            var wu = head;
            var weightmm = 0;

            if (wu.subs.Count > 2)
            {
                var w = wu.subs.Select(s => s.TotalWeight).OrderBy(s => s).ToList();
                var rw = w[1];
                var fw = w.Single(e => e != rw);
                weightmm = rw - fw;
            }

            while (wu.subs.Any(s => !s.IsBalanced))
            {
                if (wu.subs.Count > 2)
                {
                    var w = wu.subs.Select(s => s.TotalWeight).OrderBy(s => s).ToList();
                    var rw = w[1];
                    var fw = w.Single(e => e != rw);
                    weightmm = rw - fw;
                }
                wu = wu.subs.Single(s => !s.IsBalanced);
                
            }
            {
                var w = wu.subs.Select(s => s.TotalWeight).OrderBy(s => s).ToList();
                var rw = w[1];
                var fw = w.Single(e => e != rw);
                weightmm = rw - fw;
                return wu.subs.Single(s => s.TotalWeight == fw).weight + weightmm;
            }

        }

        private class Prog
        {
            public int weight;
            public string name;
            public string subsstring;
            public List<Prog> subs = new List<Prog>();
            public int TotalWeight => weight + subs.Sum(s => s.TotalWeight);
            public bool IsBalanced => subs.Select(s => s.TotalWeight).Distinct().Count() < 2;
        }
        
    }

}
