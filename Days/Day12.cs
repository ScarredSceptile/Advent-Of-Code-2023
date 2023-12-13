using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day12 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day12");
            int total = 0;
            foreach (var item in input)
            {
                var springs = item.Split(" ")[0].ToArray();
                var brokenCounts = item.Split(" ")[1].Split(",").Select(int.Parse).ToArray();

                var count = Math.Pow(2, springs.Count(n => n == '?'));
                var len = Convert.ToString((int)count, 2).Length - 1;
                Console.WriteLine(count);
                for (int i = 0; i < count; i++)
                {
                    string rpl = Convert.ToString(i, 2);
                    while (rpl.Length < len)
                        rpl = '0' + rpl;
                    var idx = (char[])springs.Clone();
                    for (int j = 0; j < rpl.Length; j++)
                        for (int k = 0; k < idx.Length; k++)
                            if (idx[k] == '?')
                            {
                                idx[k] = rpl[j] == '0' ? '.' : '#';
                                break;
                            }
                    var broken = new string(idx).Split('.').Where(n => n.Length > 0).Select(n => n.Length).ToArray();

                    if (broken.Length == brokenCounts.Length)
                    {
                        bool equal = true;
                        for (int j = 0; j < broken.Length; j++)
                            if (broken[j] != brokenCounts[j])
                            {
                                equal = false;
                                break;
                            }
                        if (equal)
                            total++;
                    }
                }
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            throw new NotImplementedException();
        }
    }
}
