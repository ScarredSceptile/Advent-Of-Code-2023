using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day9 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day9");
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var arr = input[i].Split(" ").Select(int.Parse).ToArray();
                var value = NextValue(arr);
                sum += value + arr.Last();
            }
            Console.WriteLine(sum);
        }

        public void Star2()
        {
            var input = Input.Get("Day9");
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var arr = input[i].Split(" ").Select(int.Parse).ToArray();
                var value = FirstValue(arr);
                sum += arr.First() - value;
            }
            Console.WriteLine(sum);
        }

        private int NextValue(int[] values)
        {
            int[] additions = new int[values.Length - 1];
            for (int i = 0; i < values.Length - 1; i++)
            {
                additions[i] = values[i+1] - values[i];
            }
            if (additions.Last() == 0)
                return 0;
            else
            {
                return additions.Last() + NextValue(additions);
            }
        }

        private int FirstValue(int[] values)
        {

            int[] additions = new int[values.Length - 1];
            for (int i = 0; i < values.Length - 1; i++)
            {
                additions[i] = values[i+1] - values[i];
            }
            if (additions.Last() == 0)
                return 0;
            else
            {
                return additions.First() - FirstValue(additions);
            }
        }
    }
}
