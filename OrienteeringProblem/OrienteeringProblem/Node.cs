using System;

namespace OrienteeringProblem
{
    public class Node
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Score { get; set; }
        public double Ratio { get; set; }

        public Node(int id, double x, double y,int s)
        {
            Id = id;
            X = x;
            Y = y;
            Score = s;
        }

        public double DistanceTo(Node d)
        {
            return Math.Sqrt(Math.Pow(this.X - d.X, 2) + Math.Pow(this.Y - d.Y, 2));
        }
    }
}