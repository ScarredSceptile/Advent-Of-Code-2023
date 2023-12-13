using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day13 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day13").Split("\r\n\r\n");
            int vertical = 0;
            int horizontal = 0;

            foreach (var field in input.Select(n => n.Split("\r\n")))
            {
                vertical += GetVertical(field);
                horizontal += GetHorizontal(field);
            }
            Console.WriteLine(vertical + horizontal * 100);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day13").Split("\r\n\r\n");
            int vertical = 0;
            int horizontal = 0;

            foreach (var field in input.Select(n => n.Split("\r\n")))
            {
                var notVert = GetVertical(field);
                var notHori = GetHorizontal(field);
                vertical += GetNewVertical(field, notVert);
                horizontal += GetNewHorizontal(field, notHori);
            }
            Console.WriteLine(vertical + horizontal * 100);
        }

        private int GetHorizontal(string[] field, int avoid = -1)
        {
            for (int i = 0; i < field.Length - 1; i++)
            {
                bool isMirror = true;
                for (int j = 1; j <= i + 1; j++)
                {
                    if (i - (j-1) == -1 || i+j == field.Length)
                        break;
                    for (int k = 0; k < field[0].Length; k++)
                        if (field[i - (j - 1)][k] != field[i+j][k])
                        {
                            isMirror = false; break;
                        }
                }
                if (isMirror && i != avoid - 1)
                    return i+1;
            }
            return 0;
        }

        private int GetNewHorizontal(string[] field, int avoid)
        {
            for (int i = 0; i < field.Length; i++)
            {
                var changed = (string[])field.Clone();
                for (int j = 0; j < field[0].Length; j++)
                {
                    changed[i] = Replace(field[i], j);
                    var value = GetHorizontal(changed, avoid);
                    if (value != 0)
                        return value;
                }
            }
            return 0;
        }

        private int GetVertical(string[] field, int avoid = -1)
        {
            for (int i = 0; i < field[0].Length - 1; i++)
            {
                bool isMirror = true;
                for (int j = 1; j <= i + 1; j++)
                {
                    if (i - (j-1) == -1 || i+j == field[0].Length)
                        break;
                    for (int k = 0; k < field.Length; k++)
                        if (field[k][i - (j - 1)] != field[k][i + j])
                        {
                            isMirror = false; break;
                        }
                }
                if (isMirror && i != avoid - 1)
                    return i+1;
            }
            return 0;
        }

        private int GetNewVertical(string[] field, int avoid = -1)
        {
            for (int i = 0; i < field.Length; i++)
            {
                var changed = (string[])field.Clone();
                for (int j = 0; j < field[0].Length; j++)
                {
                    changed[i] = Replace(field[i], j);
                    var value = GetVertical(changed, avoid);
                    if (value != 0)
                        return value;
                }
            }
            return 0;
        }

        private string Replace(string inp, int idx)
        {
            StringBuilder sb = new(inp);
            sb[idx] = sb[idx] == '.' ? '#' : '.';
            return sb.ToString();
        }
    }
}
