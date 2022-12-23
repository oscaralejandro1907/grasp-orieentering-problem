using System;
using System.Collections.Generic;

namespace OrienteeringProblem
{
    public class Solution
    {
        public List<Node> ListVisitedNodes { get; set; }
        public int Fitness { get; set; }

        public Solution()
        {
            ListVisitedNodes = new List<Node>();
        }
        
        public void PrintSolution()
        {
            Console.Write("Tour: ");
            foreach (var n in ListVisitedNodes)
            {
                Console.Write(n.Id + " ");
            }
            
            Console.WriteLine("\nFitness: " + Fitness);
            Console.WriteLine("Total travel time used: " + CalculateTotalTime());
        }
        
        public int CalculateFitness()
        {
            int fitness = 0;
            foreach (var n in ListVisitedNodes)
            {
                fitness += n.Score;
            }
            return fitness;
        }
        
        public double CalculateTotalTime()
        {
            double totalTravelTime = 0.0;
            for (int i = 0; i < ListVisitedNodes.Count - 1; i++)
            {
                Node org = ListVisitedNodes[i];
                Node dest = ListVisitedNodes[i + 1];

                totalTravelTime += org.DistanceTo(dest);
            }
            
            return totalTravelTime;
        }
        
        
    }
}