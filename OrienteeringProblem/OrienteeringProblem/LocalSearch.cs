using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringProblem
{
    public class LocalSearch
    {
        public Instance Data { get; set; }
        public  Solution Sinit { get; set; }
        public Solution Scurr { get; set; }
        private List<Node> ListVisitedNodes { get; set; }
        private List<Node> ListUnvisitedNodes { get; set; }

        public LocalSearch(Instance data, Solution s)
        {
            Data = data;
            Sinit = s;
            Scurr = new();
            
            ListVisitedNodes = s.ListVisitedNodes;
            ListUnvisitedNodes = Data.ListNodes.Except(ListVisitedNodes).ToList();
        }

        public void Solve()
        {
            List<Node> listVisitedNodes = new List<Node>(ListVisitedNodes);
            List<Node> listUnvisitedNodes = new List<Node>(ListUnvisitedNodes);

            Scurr.ListVisitedNodes = new (listVisitedNodes);
            
            listVisitedNodes.Remove(listVisitedNodes.First());
            listVisitedNodes.Remove(listVisitedNodes.Last());
            
            while (listVisitedNodes.Count!=0)
            {
                Node vnode = listVisitedNodes.Last();
                Scurr.ListVisitedNodes.Remove(vnode);
                Scurr.ListVisitedNodes.Remove(Data.ListNodes[1]);
                listVisitedNodes.Remove(listVisitedNodes.Last());
                
                listUnvisitedNodes = listUnvisitedNodes.OrderByDescending(n => n.Score).ToList();
                foreach (var n in listUnvisitedNodes)
                {
                    bool nodeInserted = ForceInsertNode(n,Scurr);
                    if (nodeInserted && n!=listUnvisitedNodes.Last())
                    {
                        Scurr.ListVisitedNodes.Remove(Data.ListNodes[1]);
                    }
                }

                if (Scurr.Fitness > Sinit.Fitness)
                {
                    Console.WriteLine("Local Search Found a Better Solution");
                    Scurr.PrintSolution();
                    break;  //End at First Improvement
                }
                
            }

        }

        public List<Node> GetListVisitedNodes()
        {
            List<Node> listVisitedNodes = new List<Node>(Sinit.ListVisitedNodes);

            return listVisitedNodes;

        }

        public bool ForceInsertNode(Node n, Solution Scurr)
        {
            int position = Scurr.ListVisitedNodes.Count; //last position
            while (position != 0)
            {
                Scurr.ListVisitedNodes.Insert(position,n);
                double tReturn = Scurr.ListVisitedNodes.Last().DistanceTo(Data.ListNodes[1]);
                if (CalculateCurrentDistance(Scurr.ListVisitedNodes) + tReturn > Data.TimeBudget )
                {
                    Scurr.ListVisitedNodes.Remove(n);
                    position--;
                }
                else
                {
                    Scurr.ListVisitedNodes.Add(Data.ListNodes[1]);
                    Scurr.Fitness = Scurr.CalculateFitness();
                    return true;
                }
            }

            return false;
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