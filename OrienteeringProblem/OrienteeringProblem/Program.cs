namespace OrienteeringProblem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Instance data = new Instance();
            data.PrintInstance();

            Algorithm constructive = new Algorithm(data);
            constructive.Solve();
            LocalSearch ls = new LocalSearch(data, constructive.ListSequenceOfVisit);
            ls.Solve();
        }
    }
}