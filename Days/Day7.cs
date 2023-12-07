using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023.Days
{
    internal class Day7 : Day
    {
        public void Star1()
        {
            var hands = Input.Get("Day7").Select(n => new Hand(n, false)).ToArray();
            hands = SortHands(hands, 0, hands.Length - 1);
            int i = 1;
            Console.WriteLine(hands.Sum(n => n.Bid * i++));
        }

        public void Star2()
        {
            var hands = Input.Get("Day7").Select(n => new Hand(n, true)).ToArray();
            hands = SortHands(hands, 0, hands.Length - 1);
            int i = 1;
            Console.WriteLine(hands.Sum(n => n.Bid * i++));
        }

        private static Dictionary<char, int> Card = new()
        {
            {'2', 2 },
            {'3', 3 },
            {'4', 4 },
            {'5', 5 },
            {'6', 6 },
            {'7', 7 },
            {'8', 8 },
            {'9', 9 },
            {'T', 10 },
            {'J', 11 },
            {'Q', 12 },
            {'K', 13 },
            {'A', 14 },
        };

        private static Dictionary<char, int> JokerCard = new()
        {
            {'2', 2 },
            {'3', 3 },
            {'4', 4 },
            {'5', 5 },
            {'6', 6 },
            {'7', 7 },
            {'8', 8 },
            {'9', 9 },
            {'T', 10 },
            {'J', 1 },
            {'Q', 12 },
            {'K', 13 },
            {'A', 14 },
        };

        private class Hand
        {
            public string Cards;
            public int Bid;
            IGrouping<char, char>[] CardSets;
            private bool Joker;

            public Hand(string hand, bool joker)
            {
                Cards = hand.Split(' ')[0];
                Bid = int.Parse(hand.Split(' ')[1]);
                CardSets  = Cards.GroupBy(n => n).OrderByDescending(n => n.Count()).ToArray();
                Joker = joker;
                if (joker && Cards.Contains('J') && CardSets[0].Count() != 5)
                {
                    CardSets = CardSets.Where(n => n.Key != 'J').ToArray();
                    CardSets = Cards.Replace('J', CardSets.First().Key).GroupBy(n => n).OrderByDescending(n => n.Count()).ToArray();
                }
            }

            public bool HigherThan(Hand other)
            {
                if (Cards == other.Cards) return false;
                Dictionary<char, int> card = Joker ? JokerCard : Card;

                for (int i = 0; i < CardSets.Length; i++)
                {
                    if (CardSets[i].Count() > other.CardSets[i].Count())
                        return true;
                    else if (other.CardSets[i].Count() > CardSets[i].Count())
                        return false;
                }
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (card[Cards[i]] > card[other.Cards[i]])
                        return true;
                    else if (card[other.Cards[i]] > card[Cards[i]])
                        return false;
                }
                return false;
            }
        }

        private Hand[] SortHands(Hand[] hands, int left, int right)
        {
            var i = left;
            var j = right;
            var pivot = hands[i];

            while (i <= j)
            {
                while (pivot.HigherThan(hands[i]))
                    i++;
                while (hands[j].HigherThan(pivot))
                    j--;
                if (i <= j)
                {
                    var temp  = hands[i];
                    hands[i] = hands[j];
                    hands[j] = temp;
                    i++;
                    j--;
                }
            }
            if (left < j)
                SortHands(hands, left, j);
            if (i < right)
                SortHands(hands, i, right);
            return hands;
        }
    }
}
