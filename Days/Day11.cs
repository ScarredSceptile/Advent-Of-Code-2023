using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day11 : Day
    {
        public void Star1()
        {
            Console.WriteLine(GetGalaxyDistance(2));
        }

        public void Star2()
        {
            Console.WriteLine(GetGalaxyDistance(1000000));
        }

        private long GetGalaxyDistance(long extension)
        {
            var input = Input.Get("Day11");
            List<Galaxy> galaxies = new();
            for (int i = 0; i  < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i][j] == '#')
                        galaxies.Add(new Galaxy(j, i));

            List<int> XSpread = new();
            List<int> YSpread = new();

            for (int i = galaxies.Min(n => n.X) + 1; i < galaxies.Max(n => n.X); i++)
                if (galaxies.Any(x => x.X == i) == false)
                    XSpread.Add(i);

            for (int i = galaxies.Min(n => n.Y) + 1; i < galaxies.Max(n => n.Y); i++)
                if (galaxies.Any(x => x.Y == i) == false)
                    YSpread.Add(i);

            long total = 0;
            int c = 0;
            for (int i = 0; i < galaxies.Count - 1; i++)
                for (int j = i+1; j < galaxies.Count; j++)
                {
                    long spread = extension;
                    long xDist = Math.Abs(galaxies[j].X - galaxies[i].X);
                    int[] X = new[] { galaxies[j].X, galaxies[i].X };
                    xDist += XSpread.Where(x => x > X.Min() && x < X.Max()).Count() * (spread - 1);

                    long yDist = Math.Abs(galaxies[j].Y - galaxies[i].Y);
                    int[] Y = new[] { galaxies[j].Y, galaxies[i].Y };
                    yDist += YSpread.Where(y => y > Y.Min() && y < Y.Max()).Count() *(spread - 1);

                    total += xDist + yDist;
                }
            return total;
        }
        private class Galaxy
        {
            public int X;
            public int Y;
            public Galaxy(int x, int y) { X = x; Y = y; }
        }
    }
}
