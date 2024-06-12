namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the filename:");
            string filename = Console.ReadLine();

            Ecosystem ecosystem = new Ecosystem();
            ecosystem.LoadPlants(filename);

            Console.WriteLine("Initial: ");
            ecosystem.DisplayStatus();

            ecosystem.RunSimulation();
        }
    }
}