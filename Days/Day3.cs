using System.Numerics;

namespace Advent_Of_Code_2023.Days
{
    internal class Day3 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day3");

            int total = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string partValue = "";
                bool countingNumber = false;
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (countingNumber == false && char.IsDigit(input[i][j]) == false)
                        continue;

                    else if (char.IsDigit(input[i][j]) == false || j == input[i].Length - 1)
                    {
                        int start = j - partValue.Length - 1;
                        if (char.IsDigit(input[i][j]))
                            partValue += input[i][j];

                        countingNumber = false;
                        bool isPart = false;
                        for (int x = Math.Max(0, i-1); x <= Math.Min(input.Length - 1, i+1); x++)
                            for (int y = Math.Max(0, start); y <= Math.Min(input[i].Length - 1, j); y++)
                                if (input[x][y] != '.' && char.IsDigit(input[x][y]) == false)
                                    isPart = true;
                        if (isPart)
                            total += int.Parse(partValue);
                        partValue = "";
                    }

                    else
                    {
                        partValue += input[i][j];
                        countingNumber = true;
                    }
                }
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.Get("Day3");

            List<Gear> gears = new List<Gear>();

            for (int i = 0; i < input.Length; i++)
            {
                string partValue = "";
                bool countingNumber = false;
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (countingNumber == false && char.IsDigit(input[i][j]) == false)
                        continue;

                    else if (char.IsDigit(input[i][j]) == false || j == input[i].Length - 1)
                    {
                        int start = j - partValue.Length - 1;
                        if (char.IsDigit(input[i][j]))
                            partValue += input[i][j];
                        Vector2 GearLoc = new Vector2(0, 0);

                        countingNumber = false;
                        bool isNearGear = false;
                        for (int x = Math.Max(0, i-1); x <= Math.Min(input.Length - 1, i+1); x++)
                            for (int y = Math.Max(0, start); y <= Math.Min(input[i].Length - 1, j); y++)
                                if (input[x][y] == '*')
                                {
                                    isNearGear = true;
                                    GearLoc = new Vector2(x, y);
                                }
                        if (isNearGear)
                        {
                            var gear = gears.Where(g => g.Position == GearLoc).FirstOrDefault();
                            if (gear == null)
                                gears.Add(new Gear(GearLoc, int.Parse(partValue)));
                            else
                                gear.AddValue(int.Parse(partValue));
                        }

                        partValue = "";
                    }

                    else
                    {
                        partValue += input[i][j];
                        countingNumber = true;
                    }
                }
            }
            Console.WriteLine(gears.Sum(g => g.GetRatio()));
        }

        private class Gear
        {
            public Vector2 Position;
            public List<int> values;

            public Gear(Vector2 position, int value)
            {
                Position = position;
                values = new List<int> { value };
            }

            public void AddValue(int value)
            {
                values.Add(value);
            }

            public int GetRatio()
            {
                if (values.Count == 2)
                    return values[0] * values[1];
                return 0;
            }
        }
    }
}
