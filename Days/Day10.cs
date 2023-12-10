using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day10 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day10");
            var loop = GetLoop(input);
            Console.WriteLine(loop.Count / 2 + 1);
        }

        public void Star2()
        {
            var input = Input.Get("Test");
            var loop = GetLoop(input);
            Console.WriteLine("Loop Found");
            List<Vector2> tiles = new();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (loop.Any(n => n.pos.X == j && n.pos.Y == i) == false)
                        tiles.Add(new Vector2(j, i));
                }
            }
            Console.WriteLine("All tiles not in Loop found!");
            List<List<Vector2>> tileGroups = new();
            while (tiles.Count > 0)
            {
                Vector2 tile = tiles[0];
                List<Vector2> Group = new() { tile };
                tiles.Remove(tile);
                bool GroupNotFound = true;
                while (GroupNotFound)
                {
                    GroupNotFound = false;
                    var close = tiles.Where(a => Group.Any(b => NextToEachOther(a, b))).ToList();
                    if (close.Count > 0)
                    {
                        Group.AddRange(close);
                        foreach (var a in close)
                            tiles.Remove(a);
                        GroupNotFound = true;
                    }
                }
                tileGroups.Add(Group);
            }
            Console.WriteLine("All groups of tiles found!");

            Console.WriteLine(tileGroups.Count);
            tileGroups = tileGroups.Where(n => n.Any(c => c.X == 0 || c.Y == 0 || c.Y == input.Length - 1 || c.X == input[0].Length - 1) == false).ToList();

            Console.WriteLine("Removed Edge groups");

            //Implement method to parse which tiles are in the loop, and which arent

            Console.WriteLine(tileGroups.Count);
        }

        private List<Pipe> GetLoop(string[] input)
        {
            List<Pipe> pipes = new();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] != '.')
                        pipes.Add(new Pipe(input[i][j].ToString(), j, i));
                }
            }

            var start = pipes.First(n => n.type == "S");
            Vector2[] nextPipe = new Vector2[2];
            int k = 0;
            Vector2[] connect = new[] { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
            foreach (var con in connect)
            {
                var pipe = pipes.FirstOrDefault(n => n.pos == start.pos + con);
                if (pipe != null)
                    if (pipe.CanConnect(start.pos))
                        nextPipe[k++] = pipe.pos;
            }
            List<Pipe> loop = new() { start };
            var previous = new[] { start.pos, start.pos };
            for (; ; )
            {
                for (int i = 0; i<nextPipe.Length; i++)
                {
                    var pipe = pipes.First(n => n.pos == nextPipe[i]);
                    nextPipe[i] = pipe.MoveNext(previous[i]);
                    previous[i] = pipe.pos;
                    loop.Add(pipe);
                }

                if (nextPipe[0] == nextPipe[1])
                    break;
            }
            loop.RemoveAt(loop.Count - 1);
            return loop;
        }

        private bool NextToEachOther(Vector2 a, Vector2 b)
        {
            var diff = a - b;
            if (Math.Abs(diff.X) == 1 && diff.Y == 0)
                return true;
            else if (Math.Abs(diff.Y) == 1 && diff.X == 0)
                return true;
            return false;
        }

        private class Pipe
        {
            public string type;
            public Vector2 pos;

            public Pipe(string type, int x, int y)
            {
                this.type = type;
                pos = new Vector2(x, y);
            }

            public bool CanConnect(Vector2 previous)
            {
                var prev = previous - pos;
                if (prev.X == 1 && (type == "-" || type == "F" || type == "L"))
                    return true;
                else if (prev.X == -1 && (type == "-" || type == "J" || type == "7"))
                    return true;
                else if (prev.Y == 1 && (type == "|" || type == "F" || type == "7"))
                    return true;
                else if (prev.Y == -1 && (type == "|" || type == "J" || type == "L"))
                    return true;
                return false;
            }

            public Vector2 MoveNext(Vector2 previous)
            {
                var prev = previous - pos;
                if (type == "|" || type == "-")
                    return pos - prev;
                else if (type == "L")
                    return pos + NextBend(prev, new Vector2(1, 0), new Vector2(0, -1));
                else if (type == "J")
                    return pos + NextBend(prev, new Vector2(-1, 0), new Vector2(0, -1));
                else if (type == "F")
                    return pos + NextBend(prev, new Vector2(1, 0), new Vector2(0, 1));
                else //type == "7"
                    return pos + NextBend(prev, new Vector2(-1, 0), new Vector2(0, 1));

            }

            private Vector2 NextBend(Vector2 prev, Vector2 UpDown, Vector2 LeftRight)
            {
                if (prev.X == UpDown.X)
                    return LeftRight;
                else
                    return UpDown;
            }
        }
    }
}
