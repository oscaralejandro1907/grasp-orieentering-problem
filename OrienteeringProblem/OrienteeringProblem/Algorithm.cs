using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringProblem
{
    public class Algorithm
    {
        public Instance Data { get; set; }
        public List<Node> ListSequenceOfVisit { get; set; }

        public Algorithm(Instance I)
        {
            Data = new();
            Data = I;
            ListSequenceOfVisit = new();    //List of results
        }

        public void Solve()
        {
            //Constructive phase (by an alpha value)
            double alpha = 0.4;
            
            //Initialize first node of the sequence
            List<Node> listUnvisited = new(Data.ListNodes);
           
            ListSequenceOfVisit.Add(Data.ListNodes[0]);     //Start from the origin
            listUnvisited.Remove(Data.ListNodes[0]);        //Remove from unvisited origin node
            listUnvisited.Remove(Data.ListNodes[1]);        //Remove from unvisited final node
            double cumulTime = 0;
            while (listUnvisited.Count!=0)
            {
                //Assign ratios based on score / distance from origin to the node
                CalculateRatiosFromNode(ListSequenceOfVisit.Last(), listUnvisited);
                
                
                double minvalue = Double.PositiveInfinity;
                double maxvalue = 0;
                foreach (var n in listUnvisited)
                {
                    minvalue = Math.Min(minvalue, n.Ratio);
                    maxvalue = Math.Max(maxvalue, n.Ratio);
                }
                
                List<Node> listRCL = new();
                foreach (var n in listUnvisited)
                {
                    if (n.Ratio >= maxvalue - alpha*(maxvalue-minvalue))
                    {
                        listRCL.Add(n);
                    }
                }

                var random = new Random(1);
                int indexSelected = random.Next(listRCL.Count);
                Node candidate = listRCL[indexSelected];
                
                Node lastVisitedNode = ListSequenceOfVisit.Last();
                cumulTime += lastVisitedNode.DistanceTo(candidate);
                double tReturn = candidate.DistanceTo(Data.ListNodes[1]);
                
                if (cumulTime+tReturn<=Data.TimeBudget)
                {
                    ListSequenceOfVisit.Add(candidate);
                }
                else
                {
                    cumulTime -= lastVisitedNode.DistanceTo(candidate);
                }
                
                listUnvisited.Remove(candidate);
                
            }
            
            ListSequenceOfVisit.Add(Data.ListNodes[1]);

            //Create and Print Solution
            Solution S = new Solution();
            S.ListVisitedNodes = ListSequenceOfVisit;
            S.Fitness = S.CalculateFitness();
            S.PrintSolution();

        }

        public void CalculateRatiosFromNode(Node origin, List<Node> aListNode)
        {
            foreach (var n in aListNode)
            {
                n.Ratio = n.Score / n.DistanceTo(origin);
            }
        }
        
    }
}