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

    public class Day20
    {
        
        public static int Problem1(string input)
        {

            //input = "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>\np =<4,0,0>, v=<0,0,0>, a=<-2,0,0>";

            var lines = input.Replace("\n", "\r").Replace("\r\r", "\r").Split('\r');

            var accs = new List<Tuple<int, int, int>>();

            var p = -1;

            foreach(var line in lines)
            {
                p++;
                var ps = line.Split('<')[3].Split('>')[0].Split(',');
                var ax = int.Parse(ps[0]);
                var ay = int.Parse(ps[1]);
                var az = int.Parse(ps[2]);
                var acc = Math.Abs(ax) + Math.Abs(ay) + Math.Abs(az);
                ps = line.Split('<')[2].Split('>')[0].Split(',');
                var sx = int.Parse(ps[0]);
                var sy = int.Parse(ps[1]);
                var sz = int.Parse(ps[2]);
                var sp = Math.Abs(sx) + Math.Abs(sy) + Math.Abs(sz);
                accs.Add(new Tuple<int, int, int>(p, acc, sp));
            }

            return accs.OrderBy(e => e.Item2).ThenBy(e => e.Item3).First().Item1;

        }

        public static int Problem2(string input)
        {

            //input =
            //    "p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>\n" +
            //    "p=<-4,0,0>, v=< 2,0,0 >, a =< 0,0,0 >\n" +
            //    "p=<-2,0,0>, v=< 1,0,0 >, a =< 0,0,0 >\n" +
            //    "p=<3,0,0>,  v=< -1,0,0 >, a =< 0,0,0 >";

            var lines = input.Replace("\n", "\r").Replace("\r\r", "\r").Split('\r');

            var particles = new List<long[,]>();

            foreach (var line in lines)
            {

                var parts = line.Split('<');

                GetXYX(parts[1], out var px, out var py, out var pz);
                GetXYX(parts[2], out var sx, out var sy, out var sz);
                GetXYX(parts[3], out var ax, out var ay, out var az);

                particles.Add(new long[,] { { px, py, pz }, { sx, sy, sz }, { ax, ay, az } });
                                
            }

            while (true)
            {


                foreach(var p in particles)
                {
                    p[1, 0] += p[2, 0];
                    p[1, 1] += p[2, 1];
                    p[1, 2] += p[2, 2];
                    p[0, 0] += p[1, 0];
                    p[0, 1] += p[1, 1];
                    p[0, 2] += p[1, 2];
                }

                var poses = new Dictionary<int, string>();

                for(var ix = 0; ix < particles.Count; ix++)
                {
                    var pos = GetPos(particles[ix]);
                    poses.Add(ix, pos);
                }

                var todelete = new List<int>();

                foreach(var st in poses.Values.Distinct())
                {
                    if(poses.Values.Where(e => e == st).Count() > 1)
                    {
                        todelete.AddRange(poses.Where(e => e.Value == st).Select(e => e.Key));
                    }
                }

                foreach(var ix in todelete.OrderByDescending(e => e))
                {
                    particles.RemoveAt(ix);
                    Trace.WriteLine(particles.Count());
                }
                
                for (var ix1 = particles.Count - 1; ix1 > 0; ix1--)
                {
                    for (var ix2 = 0; ix2 < ix1; ix2++)
                    {
                        if (IsClosingIn(particles[ix1], particles[ix2]))
                        {
                            goto a;
                        }
                    }
                }

                return particles.Count();

                a:
                continue;

            }
            
        }

        private static void GetXYX(string val, out long x, out long y, out long z)
        {
            val = val.Split('>')[0];
            var ps = val.Split(',');
            x = long.Parse(ps[0].Trim());
            y = long.Parse(ps[1].Trim());
            z = long.Parse(ps[2].Trim());
        }

        private static bool IsClosingIn(long[,] p1, long[,] p2)
        {
            if (
                p1[1, 0] == p2[1, 0] &&
                p1[1, 1] == p2[1, 1] &&
                p1[1, 2] == p2[1, 2] &&
                p1[2, 0] == p2[2, 0] &&
                p1[2, 1] == p2[2, 1] &&
                p1[2, 2] == p2[2, 2]
                ) return false;
            var c = 0;
            for(var axis = 0; axis < 3; axis++)
            {
                var cd = p1[0, axis] < p2[0, axis] ? -1 : 1;
                var sd = p1[1, axis] > p2[1, axis] ? -1 : 1;
                var ad = p1[2, axis] > p2[2, axis] ? -1 : 1;
                if (sd == cd || ad == cd) c++;
            }
            return c == 3;
        }

        private static string GetPos(long[,] particle) => $"{particle[0, 0]},{particle[0, 1]},{particle[0, 2]}";

    }

}
