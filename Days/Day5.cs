using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day5 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Test").Split("\r\n\r\n");
            var seeds = input[0].Split(": ")[1].Split(' ').Select(long.Parse).ToArray();
            for (int i = 1; i < seeds.Length; i++)
            {
                seeds = MapSeeds(seeds, input[i].Split("\r\n"));
            }
            Console.WriteLine(seeds.Min());
        }

        public void Star2()
        {
            throw new NotImplementedException();
        }

        private long[] MapSeeds(long[] seeds, string[] map)
        {
            long[] output = new long[seeds.Length];
            for (int i = 0; i < output.Length; i++)
                output[i] = seeds[i];

            for (int i = 1; i < map.Length;i++)
            {
                var parts = map[i].Split(' ').Select(long.Parse).ToArray();
                for (int j = 0; j < seeds.Length; j++)
                {
                    if (seeds[j] >= parts[1] && seeds[j] <= parts[1] + parts[2] - 1)
                    {
                        output[j] = parts[0] + (seeds[j] - parts[1]);
                        Console.WriteLine($"Seeds: {seeds[j]} to Output: {output[j]}");
                    }
                }
            }
            return output;
        }
    }
}
