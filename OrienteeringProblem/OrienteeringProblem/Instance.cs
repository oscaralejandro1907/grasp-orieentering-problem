using System;
using System.Collections.Generic;

namespace OrienteeringProblem
{
    public class Instance
    {
        public int TimeBudget { get; set; }
        public List<Node> ListNodes { get; set; }

        public Instance()
        {
            TimeBudget = 5;

            Node n0 = new Node(0, 10.5, 14.4, 0);   //Initial node
            Node n1 = new Node(1, 11.2, 14.1, 0);   //Final node
            Node n2 = new Node(2, 18, 15.9, 10);
            Node n3 = new Node(3, 18.3, 13.3, 10);
            Node n4 = new Node(4, 16.5, 9.3, 10);
            Node n5 = new Node(5, 15.4, 11, 10);
            Node n6 = new Node(6, 14.9, 13.2, 5);
            Node n7 = new Node(7, 16.3, 13.3, 5);
            Node n8 = new Node(8, 16.4, 17.8, 5);
            Node n9 = new Node(9, 15, 17.9, 5);

            ListNodes = new();
            
            ListNodes.Add(n0);
            ListNodes.Add(n1);
            ListNodes.Add(n2);
            ListNodes.Add(n3);
            ListNodes.Add(n4);
            ListNodes.Add(n5);
            ListNodes.Add(n6);
            ListNodes.Add(n7);
            ListNodes.Add(n8);
            ListNodes.Add(n9);
        }

        public void PrintInstance()
        {
            Console.WriteLine("Time budget: " + TimeBudget);
            foreach (var n in ListNodes)
            {
                Console.WriteLine(n.Id + " " + n.X + " " + n.Y + " " + n.Score);
            }
        }
    }
}