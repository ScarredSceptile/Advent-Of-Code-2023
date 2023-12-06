using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day6 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day6");
            var times = input[0].Split(":")[1].Replace("  ", " ").Split(' ').Where(n => n != "").Select(int.Parse).ToArray();
            var distance = input[1].Split(":")[1].Replace("  ", " ").Split(" ").Where(n => n != "").Select(int.Parse).ToArray();
            int total = 1;

            for (int i = 0; i < times.Length; i++)
            {
                int wins = 0;
                for (int j = 1; j < times[i]; j++)
                {
                    if (j * (times[i] - j) > distance[i])
                        wins++;
                }
                total *= wins;
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.Get("Day6");
            var time = long.Parse(input[0].Split(":")[1].Replace(" ", ""));
            var distance = long.Parse(input[1].Split(":")[1].Replace(" ", ""));
            long wins = 0;
            for (int i = 1; i < time; i++)
            {
                if (i * (time - i) > distance)
                    wins++;
            }
            Console.WriteLine(wins);
        }
    }
}
