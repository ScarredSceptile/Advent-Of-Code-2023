using System.Numerics;

namespace Advent_Of_Code_2023.Days
{
    internal class Day16 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day16");
            List<Vector2> energized = new();
            Dictionary<Vector2, Vector2> searchStart = new();
            energized = FindEnergized(new Vector2(-1, 0), new Vector2(1, 0), input, searchStart);
            Console.WriteLine(energized.Distinct().Count());
        }

        public void Star2()
        {
            var input = Input.Get("Day16");
            int max = 0;
            for (int i = 0; i < input[0].Length; i++)
            {
                List<Vector2> energized = new();
                Dictionary<Vector2, Vector2> searchStart = new();
                energized = FindEnergized(new Vector2(-1, i), new Vector2(1, 0), input, searchStart);
                max = Math.Max(max, energized.Distinct().Count());

                searchStart.Clear();
                energized = FindEnergized(new Vector2(input[0].Length, i), new Vector2(-1, 0), input, searchStart);
                max = Math.Max(max, energized.Distinct().Count());
            }
            for (int j = 0; j < input.Length; j++)
            {
                List<Vector2> energized = new();
                Dictionary<Vector2, Vector2> searchStart = new();
                energized = FindEnergized(new Vector2(j, -1), new Vector2(0, 1), input, searchStart);
                max = Math.Max(max, energized.Distinct().Count());

                searchStart.Clear();
                energized = FindEnergized(new Vector2(j, input.Length), new Vector2(-0, -1), input, searchStart);
                max = Math.Max(max, energized.Distinct().Count());
            }

            Console.WriteLine(max);
        }

        private List<Vector2> FindEnergized(Vector2 startPos, Vector2 direction, string[] map, Dictionary<Vector2, Vector2> searchStart)
        {
            if (searchStart.ContainsKey(startPos))
            {
                if (searchStart[startPos] == direction)
                    return new List<Vector2>();
                searchStart[startPos] = direction;
            }
            else
                searchStart.Add(startPos, direction);

            List<Vector2> energized = new();
            bool cont = true;
            var move = startPos;
            while (cont)
            {
                move += direction;
                if (move.X >= map[0].Length || move.Y >= map.Length || move.X < 0 || move.Y < 0)
                    break;

                energized.Add(move);
                switch (map[(int)move.Y][(int)move.X])
                {
                    case '.': continue;
                    case '/':
                        energized.AddRange(FindEnergized(move, new Vector2(direction.Y * -1, direction.X * -1), map, searchStart));
                        cont = false;
                        break;
                    case '\\':
                        energized.AddRange(FindEnergized(move, new Vector2(direction.Y, direction.X), map, searchStart));
                        cont = false;
                        break;
                    case '|':
                        if (Math.Abs(direction.X) == 1)
                        {
                            energized.AddRange(FindEnergized(move, new Vector2(0, 1), map, searchStart));
                            energized.AddRange(FindEnergized(move, new Vector2(0, -1), map, searchStart));
                            cont = false;
                        }
                        break;
                    case '-':
                        if (Math.Abs(direction.Y) == 1)
                        {
                            energized.AddRange(FindEnergized(move, new Vector2(1, 0), map, searchStart));
                            energized.AddRange(FindEnergized(move, new Vector2(-1, 0), map, searchStart));
                            cont = false;
                        }
                        break;
                }
            }
            return energized;
        }
    }
}
