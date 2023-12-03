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
                string num = $"{item.FirstOrDefault(char.IsDigit)}{item.LastOrDefault(char.IsDigit)}";
                if (num == "\0\0")
                    continue;
                value += int.Parse(num);
            }
            Console.WriteLine(value);
        }

        private Dictionary<string, string> Digits = new Dictionary<string, string>
        {
            {"one", "o1e" },
            {"two", "t2o" },
            {"three", "t3e" },
            {"four", "f4r" },
            {"five", "f5e" },
            {"six", "s6x" },
            {"seven", "s7n" },
            {"eight", "e8t" },
            {"nine", "n9e" },
        };

        public void Star2()
        {
            var input = Input.Get("Day1");
            int value = 0;
            foreach (var item in input)
            {
                string formatted = item;
                foreach(var digit in Digits)
                    formatted = formatted.Replace(digit.Key, digit.Value);

                string num = $"{formatted.FirstOrDefault(char.IsDigit)}{formatted.LastOrDefault(char.IsDigit)}";
                if (num == "\0\0")
                    continue;

                value += int.Parse(num);
            }
            Console.WriteLine(value);
        }
    }
}
