using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int count = 0;
            while (node.ID != "ZZZ")
            {
                var dir = direction[count % direction.Length];
                if (dir == 'L')
                    node = node.Left;
                else
                    node = node.Right;
                count++;
            }
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
            int count = 0;
            while (node.All(n => n.ID.EndsWith("Z") == true) == false)
            {
                for (int i = 0; i < node.Length; i++)
                {
                    var dir = direction[count % direction.Length];
                    if (dir == 'L')
                        node[i] = node[i].Left;
                    else
                        node[i] = node[i].Right;
                }
                count++;
            }
            Console.WriteLine(count);
        }

        private class Node
        {
            public string ID;
            public Node Left;
            public Node Right;
            public Node(string ID) { this.ID = ID; }
        }
    }
}
