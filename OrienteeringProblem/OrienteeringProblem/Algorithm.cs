using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace OrienteeringProblem
{
    public class Algorithm
    {
        public Instance Data { get; set; }
        private List<Node> ListSequenceOfVisit { get; set; }

        public Algorithm(Instance I)
        {
            Data = new();
            Data = I;
            ListSequenceOfVisit = new();    //List of results
        }

        public void solve()
        {
            //Constructive phase (by an alpha value)
            double alpha = 0.4;
            
            //Initialize first node of the sequence
            List<Node> listUnvisited = new(Data.ListNodes);
           
            ListSequenceOfVisit.Add(Data.ListNodes[0]);     //Start from the origin
            listUnvisited.Remove(Data.ListNodes[0]);        //Remove from unvisited origin node
            listUnvisited.Remove(Data.ListNodes[1]);        //Remove from unvisited final node

            List<Node> listUnvisitedAux = new(listUnvisited);
            double cumulTime = 0;
            while (listUnvisitedAux.Count!=0 && cumulTime<=Data.TimeBudget)
            {
                //listUnvisited = listUnvisited.Except(ListSequenceOfVisit).ToList(); //Disjoint elements
                
                //Assign ratios based on score / distance from origin to the node
                calculateRatiosfromNode(ListSequenceOfVisit.Last(), listUnvisited);
                
                double minvalue = Double.PositiveInfinity;
                double maxvalue = 0;
                foreach (var n in listUnvisited)
                {
                    minvalue = Math.Min(minvalue, n.Ratio);
                    maxvalue = Math.Max(maxvalue, n.Ratio);
                }
                
                List<Node> listRCL = new();
                
                foreach (var n in listUnvisitedAux)
                {
                    if (n.Ratio >= maxvalue - alpha*(maxvalue-minvalue))
                    {
                        listRCL.Add(n);
                    }
                }

                var random = new Random();
                int indexselected = random.Next(listRCL.Count);
                Node candidate = listRCL[indexselected];
                
                Node lastVisitedNode = ListSequenceOfVisit.Last();
                cumulTime += lastVisitedNode.DistanceTo(candidate);
                double tReturn = candidate.DistanceTo(Data.ListNodes[1]);
                
                if (cumulTime+tReturn<=Data.TimeBudget)
                {
                    ListSequenceOfVisit.Add(candidate);
                    listUnvisited.Remove(candidate);
                }
                else
                {
                    cumulTime -= lastVisitedNode.DistanceTo(candidate);
                }
                
            }
            
            
        }

        public void calculateRatiosfromNode(Node origin, List<Node> aListNode)
        {
            foreach (var n in aListNode)
            {
                n.Ratio = n.Score / n.DistanceTo(origin);
            }
        }
    }
}