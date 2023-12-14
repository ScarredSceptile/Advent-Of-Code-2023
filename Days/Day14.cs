using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day14 : Day
    {
        public void Star1()
        {
            var grid = Input.Get("Day14");
            int count = grid.Length;
            List<Stone> stones = new List<Stone>();
            for (int i = 0; i < grid[0].Length; i++)
                for (int j = 0; j < grid.Length; j++)
                    if (grid[i][j] != '.')
                        stones.Add(new Stone(grid[i][j], i, j));

            FlipNorth(stones);

            Console.WriteLine(stones.Where(n => n.Type == 'O').Sum(n => count - n.X));
        }

        public void Star2()
        {
            var grid = Input.Get("Day14");
            int count = grid.Length;
            List<Stone> stones = new List<Stone>();
            for (int i = 0; i < grid[0].Length; i++)
                for (int j = 0; j < grid.Length; j++)
                    if (grid[i][j] != '.')
                        stones.Add(new Stone(grid[i][j], i, j));

            List<List<Stone>> stoneList = new();
            for (int i = 0; i < 1000000000; i++)
            {
                Console.WriteLine(i);
                FlipNorth(stones);
                FlipWest(stones);
                FlipSouth(stones, grid.Length);
                FlipEast(stones, grid.Length);
                var replica = stoneList.FirstOrDefault(n => CompareStoneSets(n, stones));

                List<Stone> mem = new();
                stones.ForEach(n => mem.Add(new Stone(n)));
                stoneList.Add(new(mem));

                if (replica != null)
                {
                    var start = stoneList.IndexOf(replica);
                    var rem = 1000000000 - start;
                    var guide = i-start;
                    stoneList = stoneList.GetRange(start, guide);
                    stones = stoneList[rem % guide - 1];
                    break;
                }
            }

            Console.WriteLine(stones.Where(n => n.Type == 'O').Sum(n => count - n.X));
        }

        private class Stone
        {
            public char Type;
            public int X;
            public int Y;
            public Stone(char type, int x, int y) { Type = type; X = x; Y = y; }
            public Stone(Stone clone) { Type = clone.Type; X = clone.X; Y = clone.Y; }
        }

        private void FlipNorth(List<Stone> stones)
        {
            stones = stones.OrderBy(n => n.X).ToList();
            foreach (var stone in stones)
                if (stone.Type == 'O')
                    while (stone.X > 0 && stones.Any(n => n.X == stone.X - 1 && n.Y == stone.Y) == false)
                        stone.X--;
        }

        private void FlipSouth(List<Stone> stones, int max)
        {
            stones = stones.OrderByDescending(n => n.X).ToList();
            foreach (var stone in stones)
                if (stone.Type == 'O')
                    while (stone.X < max - 1 && stones.Any(n => n.X == stone.X + 1 && n.Y == stone.Y) == false)
                        stone.X++;
        }
        private void FlipWest(List<Stone> stones)
        {
            stones = stones.OrderBy(n => n.Y).ToList();
            foreach (var stone in stones)
                if (stone.Type == 'O')
                    while (stone.Y > 0 && stones.Any(n => n.Y == stone.Y - 1 && n.X == stone.X) == false)
                        stone.Y--;
        }
        private void FlipEast(List<Stone> stones, int max)
        {
            stones = stones.OrderByDescending(n => n.Y).ToList();
            foreach (var stone in stones)
                if (stone.Type == 'O')
                    while (stone.Y < max - 1 && stones.Any(n => n.Y == stone.Y + 1 && n.X == stone.X) == false)
                        stone.Y++;
        }

        private bool CompareStoneSets(List<Stone> set1, List<Stone> set2)
        {
            foreach(var stone in set1)
            {
                bool foundInOther = false;
                foreach(var stone2 in set2)
                {
                    if (stone2.Y == stone.Y && stone2.X == stone.X && stone2.Type == stone.Type)
                    {
                        foundInOther = true;
                        break;
                    }
                }
                if (!foundInOther)
                    return false;
            }
            return true;
        }

        private void PrintStones(List<Stone> stones, int iMax, int jMax)
        {

            for (int i = 0; i < iMax; i++)
            {
                for (int j = 0; j < jMax; j++)
                {
                    var stone = stones.FirstOrDefault(n => n.X == i && n.Y == j);
                    if (stone != null)
                        Console.Write(stone.Type);
                    else
                        Console.Write('.');
                }
                Console.Write('\n');
            }
            Console.WriteLine("\n-----------------------\n");
        }
    }
}
