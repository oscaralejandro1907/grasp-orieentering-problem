using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringProblem
{
    public class LocalSearch
    {
        public Instance Data { get; set; }
        public List<Node> InitialSolution { get; set; }
        public List<Node> ListUnvisited { get; set; }

        public LocalSearch(Instance data, List<Node> initialSolution)
        {
            Data = data;
            InitialSolution = initialSolution;
            ListUnvisited = new();
            ListUnvisited = Data.ListNodes.Except(InitialSolution).ToList();
        }

        public void Solve()
        {
            List<Node> listVisitedNodes = new List<Node>(InitialSolution);
            List<Node> listUnvisitedNodes = new List<Node>(ListUnvisited);

            listUnvisitedNodes = listUnvisitedNodes.OrderByDescending(n => n.Score).ToList();
            listVisitedNodes.Remove(listVisitedNodes.Last());
            
            foreach (var n in listUnvisitedNodes)
            {
                
            }
            
            
        }

        public Solution ForceInsertNode(Node n, List<Node> listVisited)
        {
            Solution sNew = new Solution();
            int position = listVisited.Count - 1; //last position
            while (position != 0)
            {
                listVisited.Insert(position,n);
                double tReturn = listVisited.Last().DistanceTo(Data.ListNodes[1]);
                if (CalculateCurrentDistance(listVisited) + tReturn > Data.TimeBudget )
                {
                    listVisited.Remove(n);
                    position--;
                }
                else
                {
                    listVisited.Add(Data.ListNodes[1]);
                    sNew.ListVisitedNodes = listVisited;
                    sNew.Fitness = sNew.CalculateFitness();
                    break;
                }
            }

            return sNew;
        }

        public double CalculateCurrentDistance(List<Node> listVisited)
        {
            double cumulTravelTime = 0.0;
            for (int i = 0; i < listVisited.Count - 1; i++)
            {
                Node org = listVisited[i];
                Node dest = listVisited[i + 1];

                cumulTravelTime += org.DistanceTo(dest);
            }
            
            return cumulTravelTime;
        }

    }
}