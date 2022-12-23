using System;
using System.Collections.Generic;
using System.Linq;

namespace OrienteeringProblem
{
    public class LocalSearch
    {
        public List<Node> InitialSolution { get; set; }
        public List<Node> ListUnvisited { get; set; }

        public LocalSearch(Instance data, List<Node> initialSolution)
        {
            InitialSolution = initialSolution;
            ListUnvisited = new();
            ListUnvisited = data.ListNodes.Except(InitialSolution).ToList();
        }
    }
}