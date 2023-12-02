using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day2 : Day
    {
        private Dictionary<string, int> MaxCubes = new Dictionary<string, int>
        {
            {"red", 12 },
            {"green", 13 },
            {"blue", 14 },
        };

        public void Star1()
        {
            var input = Input.Get("Day2");
            int total = 0;

            foreach (var game in input)
            {
                var id = int.Parse(game.Split(':')[0].Split(' ')[1]);
                var cubes = game.Substring(game.IndexOf(':') + 2);
                var subsets = cubes.Split("; ");

                bool valid = true;

                foreach (string set in subsets)
                {
                    var pulls = set.Split(", ");
                    foreach (string pull in pulls)
                    {
                        string color = pull.Split(" ")[1];
                        var amount = int.Parse(pull.Split(" ")[0]);
                        if (MaxCubes[color] < amount)
                            valid = false;
                    }
                }

                if (valid)
                    total += id;
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.Get("Day2");
            int total = 0;

            foreach (var game in input)
            {
                var id = int.Parse(game.Split(':')[0].Split(' ')[1]);
                var cubes = game.Substring(game.IndexOf(':') + 2);
                var subsets = cubes.Split("; ");

                int green = 0, red = 0, blue = 0;

                foreach (string set in subsets)
                {
                    var pulls = set.Split(", ");
                    foreach (string pull in pulls)
                    {
                        string color = pull.Split(" ")[1];
                        var amount = int.Parse(pull.Split(" ")[0]);
                        if (color == "green")
                            green = Math.Max(green, amount);
                        if (color == "red")
                            red = Math.Max(red, amount);
                        if (color == "blue")
                            blue = Math.Max(blue, amount);
                    }
                }

                total += green * red * blue;
            }
            Console.WriteLine(total);
        }
    }
}
