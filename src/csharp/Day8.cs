using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{

    public class Day8
    {

        public static int Problem1(string input)
        {

            //input = "b inc 5 if a > 1\r\na inc 1 if b < 5\r\nc dec -10 if a >= 1\r\nc inc -20 if c == 10";

            var bank = new bank();

            var lines = input.GetLines();

            foreach (var line in lines)
            {

                var words = line.GetWords();
                var reg = words[0];
                var op = words[1];
                var val = int.Parse(words[2]);
                var checkreg = words[4];
                var check = words[5];
                var checkval = int.Parse(words[6]);

                var c = bank.Get(checkreg);

                var ok = false;

                switch (check)
                {
                    case "<": ok = c < checkval; break;
                    case "<=": ok = c <= checkval; break;
                    case ">": ok = c > checkval; break;
                    case ">=": ok = c >= checkval; break;
                    case "==": ok = c == checkval; break;
                    case "!=": ok = c != checkval; break;
                }

                if (ok)
                {
                    if (op == "dec") val *= -1;
                    bank.Set(reg, bank.Get(reg) + val);
                }

            }

            return bank._mem.Values.Max();

        }

        public static int Problem2(string input)
        {

            //input = "b inc 5 if a > 1\r\na inc 1 if b < 5\r\nc dec -10 if a >= 1\r\nc inc -20 if c == 10";

            var bank = new bank();

            var lines = input.GetLines();

            var max = 0;

            foreach (var line in lines)
            {

                var words = line.GetWords();
                var reg = words[0];
                var op = words[1];
                var val = int.Parse(words[2]);
                var checkreg = words[4];
                var check = words[5];
                var checkval = int.Parse(words[6]);

                var c = bank.Get(checkreg);

                var ok = false;

                switch (check)
                {
                    case "<": ok = c < checkval; break;
                    case "<=": ok = c <= checkval; break;
                    case ">": ok = c > checkval; break;
                    case ">=": ok = c >= checkval; break;
                    case "==": ok = c == checkval; break;
                    case "!=": ok = c != checkval; break;
                }

                if (ok)
                {
                    if (op == "dec") val *= -1;
                    bank.Set(reg, bank.Get(reg) + val);
                    max = Math.Max(max, bank._mem.Values.Max());
                }


            }

            return max;

        }
        
        private class bank
        {
            public Dictionary<string, int> _mem = new Dictionary<string, int>();
            public int Get(string name)
            {
                return _mem.ContainsKey(name) ? _mem[name]: 0;
            }
            public void Set(string name, int value)
            {
                if (_mem.ContainsKey(name))
                {
                    _mem[name] = value;
                }
                else
                {
                    _mem.Add(name, value);
                }
            }
        }
        
    }

}
