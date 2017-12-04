using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace csharp
{

    public class Day3
    {

        public static int Problem2()
        {
            var input = 289326;
            var map = new int[200,200];
            var m = 0;
            var x = 100;
            var y = 100;
            map[x, y] = 1;
            while (true)
            {
                m++;
                for (var d = 0; d < 4; d++)
                {
                    if (d == 2) m++;
                    for (var repeat = 0; repeat < m; repeat++)
                    {
                        switch (d)
                        {
                            case 0:
                                x++;
                                break;
                            case 1:
                                y--;
                                break;
                            case 2:
                                x--;
                                break;
                            case 3:
                                y++;
                                break;
                        }
                        var sum =
                            map[x - 1, y - 1] +
                            map[x, y - 1] +
                            map[x + 1, y - 1] +
                            map[x - 1, y] +
                            map[x + 1, y] +
                            map[x - 1, y + 1] +
                            map[x, y + 1] +
                            map[x + 1, y + 1];
                        map[x, y] = sum;
                        if (sum > input) return sum;
                    }
                }
            }
        }

    }

}
