using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using csharp.Properties;

namespace csharp
{

    public class Day9
    {

        public static int Problem1()
        {

            var input = Resources.Day9;

            var chars = input.ToCharArray().Select(e => e.ToString()).ToArray();

            var level = 0;
            var ingarbage = false;
            var afterexclamation = false;

            var sum = 0;

            foreach (var c in chars)
            {

                if (afterexclamation)
                {
                    afterexclamation = false;
                    continue;
                }

                if (ingarbage)
                {
                    switch(c)
                    {
                        case "!":
                            afterexclamation = true;
                            break;
                        case ">":
                            ingarbage = false;
                            break;
                    }
                    continue;
                }

                switch (c)
                {
                    case "{":
                        level++;
                        break;
                    case "}":
                        sum += level;
                        level--;
                        break;
                    case "<":
                        ingarbage = true;
                        break;
                }

            }

            return sum;

        }

        public static int Problem2()
        {

            var input = Resources.Day9;

            var chars = input.ToCharArray().Select(e => e.ToString()).ToArray();

            var level = 0;
            var ingarbage = false;
            var afterexclamation = false;

            var sum = 0;

            foreach (var c in chars)
            {

                if (afterexclamation)
                {
                    afterexclamation = false;
                    continue;
                }

                if (ingarbage)
                {
                    switch (c)
                    {
                        case "!":
                            afterexclamation = true;
                            break;
                        case ">":
                            ingarbage = false;
                            break;
                        default:
                            sum++;
                            break;
                    }
                    continue;
                }

                switch (c)
                {
                    case "{":
                        level++;
                        break;
                    case "}":
                        level--;
                        break;
                    case "<":
                        ingarbage = true;
                        break;
                }

            }

            return sum;

        }
        
    }

}
