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
            int count = 0;
            var previous = new[] { start.pos, start.pos };
            for (; ; )
            {
                for (int i = 0; i<nextPipe.Length; i++)
                {
                    var pipe = pipes.First(n => n.pos == nextPipe[i]);
                    nextPipe[i] = pipe.MoveNext(previous[i]);
                    previous[i] = pipe.pos;
                }

                count++;
                if (nextPipe[0] == nextPipe[1])
                    break;
            }
            Console.WriteLine(count + 1);
        }

        public void Star2()
        {
            throw new NotImplementedException();
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
