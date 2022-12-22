namespace OrienteeringProblem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Instance data = new Instance();
            data.PrintInstance();

            Algorithm grasp = new Algorithm(data);
            grasp.Solve();
        }
    }
}