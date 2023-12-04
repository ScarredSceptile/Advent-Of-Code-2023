namespace Advent_Of_Code_2023.Days
{
    internal class Day4 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day4");
            int total = 0;

            foreach (var card in input)
            {
                var numbers = card.Split(": ")[1];
                var winning = numbers.Split(" | ")[0];
                var matches = numbers.Split(" | ")[1];
                var count = winning.Split(' ').Where(x => matches.Split(' ').Any(c => c == x) && x != "").Count();
                if (count > 0)
                    total += (int)Math.Pow(2, count - 1);
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.Get("Day4");
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < input.Length; i++)
                cards.Add(new Cards() { ID = i+1, Amount = 1 });

            foreach (var item in input)
            {
                var id = int.Parse(item.Split(':')[0].Split(' ').Where(n => n != "").ToArray()[1]);
                var numbers = item.Split(": ")[1];
                var winning = numbers.Split(" | ")[0];
                var matches = numbers.Split(" | ")[1];
                var count = winning.Split(' ').Where(x => matches.Split(' ').Any(c => c == x) && x != "").Count();
                var card = cards.First(x => x.ID == id);
                for (int i = id+1; i < id+1+ count; i++)
                    cards.Where(n => n.ID == i).ToList().ForEach(x => x.Amount += card.Amount);
            }
            Console.WriteLine(cards.Sum(c => c.Amount));
        }

        private class Cards
        {
            public int ID;
            public int Amount;
        }
    }
}
