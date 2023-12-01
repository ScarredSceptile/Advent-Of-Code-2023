namespace Advent_Of_Code_2023.Days
{
    internal class Day1 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day1");
            int value = 0;
            foreach (var item in input)
            {
                string num = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (char.IsDigit(item[i]))
                    {
                        num += item[i];
                        break;
                    }
                }
                for (int i = item.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(item[i]))
                    {
                        num += item[i];
                        break;
                    }
                }

                value += int.Parse(num);
            }
            Console.WriteLine(value);
        }

        private Dictionary<string, string> Digits = new Dictionary<string, string>
        {
            {"one", "1" },
            {"two", "2" },
            {"three", "3" },
            {"four", "4" },
            {"five", "5" },
            {"six", "6" },
            {"seven", "7" },
            {"eight", "8" },
            {"nine", "9" },
        };

        public void Star2()
        {
            var input = Input.Get("Day1");
            int value = 0;
            foreach (var item in input)
            {
                string num = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (char.IsDigit(item[i]))
                    {
                        num += item[i];
                        break;
                    }
                    var start = GetStart(item[i..]);
                    if (start != "")
                    {
                        num += start;
                        break;
                    }
                }
                for (int i = item.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(item[i]))
                    {
                        num += item[i];
                        break;
                    }
                    var start = GetEnd(item[..(i + 1)]);
                    if (start != "")
                    {
                        num += start;
                        break;
                    }
                }

                value += int.Parse(num);
            }
            Console.WriteLine(value);
        }

        private string GetStart(string input)
        {
            foreach (var item in Digits)
            {
                if (input.StartsWith(item.Key))
                    return item.Value;
            }
            return "";
        }

        private string GetEnd(string input)
        {
            foreach (var item in Digits)
            {
                if (input.EndsWith(item.Key))
                    return item.Value;
            }
            return "";
        }
    }
}
