using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace ConsoleApp1
{
    public class Ecosystem
    {
        private List<Plant> plants= new List<Plant>();
        private Radiation currentRadiation = Radiation.None;
        private Radiation lastRadiation;
        public Radiation CurrentRadiation
        {
            get { return currentRadiation; }
        }
        public void LoadPlants(string filename)
        {
            TextFileReader reader = new TextFileReader(filename);

            reader.ReadInt(out int plantCount);
            for(int i = 0; i < plantCount; i++)
            {
                string name = reader.ReadString();
                string type = reader.ReadString();
                reader.ReadInt(out int nutrientLevel);
                plants.Add(Plant.Create(type, name, nutrientLevel));
                
            }
        }

        public void SimulateDay()
        {
            int alphaDemand = 0;
            int deltaDemand = 0;

            foreach (var plant in plants)
            {
                plant.ReactToRadiation(currentRadiation);

                if (plant is Wombleroot)
                {
                    alphaDemand += plant.InfluenceNextRadiation();
                }
                else if (plant is Wittentoot)
                {
                    deltaDemand += plant.InfluenceNextRadiation();
                }
            }

            lastRadiation = currentRadiation;
            if (alphaDemand - deltaDemand >= 3)
            {
                currentRadiation = Radiation.Alpha;
            }
            else if (deltaDemand - alphaDemand >= 3)
            {
                currentRadiation = Radiation.Delta;
            }
            else
            {
                currentRadiation = Radiation.None;
            }

        }

        public void RunSimulation()
        {
            bool simulationContinues = true;
            while (simulationContinues)
            {
                SimulateDay();
                DisplayStatus();
                simulationContinues = !(currentRadiation== Radiation.None && lastRadiation == Radiation.None);
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Today's radiation: {currentRadiation}");
            foreach (var plant in plants)
            {
                Console.WriteLine(plant);
            }
            Console.WriteLine("\n");
        }


    }
}
