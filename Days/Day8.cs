namespace Advent_Of_Code_2023.Days
{
    internal class Day8 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day8").Split("\r\n\r\n");
            var direction = input[0];
            var nodes = input[1].Split("\r\n").Select(n => new Node(n.Split(" = ")[0])).ToArray();
            var allNodes = input[1].Split("\r\n");
            for (int i = 0; i < nodes.Length; i++)
            {
                var ids = allNodes[i].Split(" = ")[1].Split('(', ')')[1];
                nodes[i].Left = nodes.First(n => n.ID == ids.Split(", ")[0]);
                nodes[i].Right = nodes.First(n => n.ID == ids.Split(", ")[1]);
            }
            var node = nodes.First(n => n.ID == "AAA");
            var count = FirstZ(node, direction, "ZZZ");
            Console.WriteLine(count);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day8").Split("\r\n\r\n");
            var direction = input[0];
            var nodes = input[1].Split("\r\n").Select(n => new Node(n.Split(" = ")[0])).ToArray();
            var allNodes = input[1].Split("\r\n");
            for (int i = 0; i < nodes.Length; i++)
            {
                var ids = allNodes[i].Split(" = ")[1].Split('(', ')')[1];
                nodes[i].Left = nodes.First(n => n.ID == ids.Split(", ")[0]);
                nodes[i].Right = nodes.First(n => n.ID == ids.Split(", ")[1]);
            }
            var node = nodes.Where(n => n.ID.EndsWith("A")).ToArray();
            List<long> z = new();
            foreach (var n in node)
            {
                z.Add(FirstZ(n, direction, "Z"));
            }
            Console.WriteLine(lcm(z.ToArray(), 0));
        }

        private class Node
        {
            public string ID;
            public Node Left;
            public Node Right;
            public Node(string ID) { this.ID = ID; }
        }

        private long FirstZ(Node node, string direction, string ending)
        {
            long count = 0;
            while (node.ID.EndsWith(ending) == false)
            {
                var dir = direction[(int)count % direction.Length];
                if (dir == 'L')
                    node = node.Left;
                else
                    node = node.Right;
                count++;
            }
            return count;
        }

        private long gcd(long a, long b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }

        private long lcm(long[] arr, int idx)
        {
            if (idx ==  arr.Length - 1)
                return arr[idx];
            long a = arr[idx];
            long b = lcm(arr, idx + 1);
            return (a*b / gcd(a, b));
        }
    }
}
