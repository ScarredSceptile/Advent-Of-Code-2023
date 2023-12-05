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
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var seeds = input[0].Split(": ")[1].Split(' ').Select(long.Parse).ToArray();
            for (int i = 1; i < input.Length; i++)
            {
                seeds = MapSeeds(seeds, input[i].Split("\r\n"));
            }
            Console.WriteLine(seeds.Min());
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var seeds = input[0].Split(": ")[1].Split(' ').Select(long.Parse).ToArray();
            List<SeedRange> seedRanges = new();
            for (int i = 0; i < seeds.Length; i += 2)
                seedRanges.Add(new SeedRange(seeds[i], seeds[i] + seeds[i+1] - 1));
            for (int i = 1; i < input.Length; i++)
            {
                seedRanges = MapSeedRanges(seedRanges, input[i].Split("\r\n"));
            }
            Console.WriteLine(seedRanges.Min(s => s.Min));
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
                    if (seeds[j] >= parts[1] && seeds[j] <= parts[1] + parts[2])
                        output[j] = parts[0] + (seeds[j] - parts[1]);
            }
            return output;
        }

        private List<SeedRange> MapSeedRanges(List<SeedRange> seeds, string[] map)
        {
            List<SeedRange> output = new();

            for (int i = 1; i < map.Length; i++)
            {
                var parts = map[i].Split(' ').Select(long.Parse).ToArray();
                for (int j = 0; j < seeds.Count; j++)
                {
                    if (seeds[j].Min >= parts[1] && seeds[j].Max <= parts[1] + parts[2] - 1)
                    {
                        seeds[j].Min =parts[0] + (seeds[j].Min - parts[1]);
                        seeds[j].Max =parts[0] + (seeds[j].Max - parts[1]);
                        output.Add(seeds[j]);
                        seeds.RemoveAt(j);
                        j--;
                    }
                    else if (seeds[j].Max > parts[1] && seeds[j].Max <= parts[1] + parts[2] - 1)
                    {
                        SeedRange newSeed = new(parts[0] + (parts[1] - seeds[j].Min), parts[0] + (seeds[j].Max - parts[1]));
                        seeds[j].Max =parts[1] - 1;
                        output.Add(newSeed);
                    }
                    else if (seeds[j].Min >= parts[1] && seeds[j].Min < parts[1] + parts[2] - 1)
                    {
                        SeedRange newSeed = new(parts[0] + (seeds[j].Min - parts[1]), parts[0] + parts[2] - 1);
                        seeds[j].Min =parts[1] + parts[2];
                        output.Add(newSeed);
                    }
                    else if (seeds[j].Min < parts[1] && seeds[j].Max > parts[1] + parts[2] - 1)
                    {
                        SeedRange middleSeedRange = new(parts[0], parts[0] + parts[2]);
                        SeedRange lateSeedRange = new(parts[1] + parts[2], seeds[j].Max);
                        output.Add(middleSeedRange);
                        seeds.Add(lateSeedRange);
                        seeds[j].Max =parts[1] - 1;
                    }
                }
            }
            output.AddRange(seeds);

            return output;
        }

        private class SeedRange
        {
            public long Min;
            public long Max;
            public SeedRange(long min, long max) { Min = min; Max = max; }
        }
    }
}
