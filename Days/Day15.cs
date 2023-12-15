using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day15 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day15").Split(',');
            int total = 0;
            foreach (var item in input)
            {
                int count = 0;
                foreach (var character in item)
                {
                    count += character;
                    count *= 17;
                    count %= 256;
                }
                total += count;
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day15").Split(',');
            Box[] boxes = new Box[256];
            for (int i = 0; i < boxes.Length; i++)
                boxes[i] = new Box();

            foreach (var item in input)
            {
                int count = 0;
                var symbol = item.Contains('-') ? '-' : '=';
                int value = 0;
                string label;
                if (symbol == '-')
                    label = item[..^1];
                else
                {
                    var parts = item.Split('=');
                    label = parts[0];
                    value = int.Parse(parts[1]);
                }
                foreach (var character in label)
                {
                    count += character;
                    count *= 17;
                    count %= 256;
                }
                if (symbol == '-')
                {
                    var lens = boxes[count].Lenses.FirstOrDefault(n => n.Key == label);
                    if (lens != null)
                        boxes[count].Lenses.Remove(lens);
                }
                else
                {
                    var lens = boxes[count].Lenses.FirstOrDefault(n => n.Key == label);
                    if (lens != null)
                        lens.Value = value;
                    else
                        boxes[count].Lenses.Add(new Lens(label, value));
                }
            }
            int total = 0;
            for (int i = 0; i < boxes.Length; i++)
                total += boxes[i].GetLensValues(i+1);

            Console.WriteLine(total);
        }

        private class Box
        {
            public List<Lens> Lenses;
            public Box() { Lenses = new(); }
            public int GetLensValues(int box)
            {
                int slot = 1;
                int count = 0;
                foreach (var lens in Lenses)
                {
                    count += box * lens.Value * slot;
                    slot++;
                }
                return count;
            }
        }

        private class Lens
        {
            public string Key;
            public int Value;
            public Lens(string key, int value)
            {
                Key=key;
                Value=value;
            }
        }
    }
}
