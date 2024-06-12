using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum Radiation { Alpha, Delta, None }
    public abstract class Plant
    {
        public string Name { get; protected set; }
        public int NutrientLevel { get; protected set; }
       
        protected Plant(string name, int nutrientLevel)
        {
            Name = name;
            NutrientLevel = Math.Max(0, nutrientLevel);
        }
        public virtual bool IsAlive()
        {
            return NutrientLevel > 0;
        }

        public abstract void ReactToRadiation(Radiation radiation);
        public abstract int InfluenceNextRadiation();
        public override string ToString()
        {
            return $"{Name} - Nutrient Level: {NutrientLevel}, Alive: {IsAlive()}";
        }

        protected virtual void AdjustNutrientLevel(int change)
        {
            NutrientLevel += change;
            if (NutrientLevel < 0)
                NutrientLevel = 0;
           
        }

        // factory pattern
        public static Plant Create(string type, string name, int nutrientLevel)
        {
            switch (type)
            {
                case "wom":
                    return new Wombleroot(name, nutrientLevel);
                case "wit":
                    return new Wittentoot(name, nutrientLevel);
                case "wor":
                    return new Woreroot(name, nutrientLevel);
                default:
                    throw new ArgumentException("Unknown plant type");
            }
        }
    }

    class Wombleroot: Plant
    {
        public Wombleroot(string name, int nutrientLevel): base(name, nutrientLevel) { }

        public override void ReactToRadiation(Radiation radiation)
        {
            if (!IsAlive()) return;

            switch (radiation)
            {
                case Radiation.Alpha:
                    AdjustNutrientLevel(2);
                    break;
                case Radiation.Delta:
                    AdjustNutrientLevel(-2);
                    break;
                case Radiation.None:
                    AdjustNutrientLevel(-1);
                    break;
            }

        }

        protected override void AdjustNutrientLevel(int change)
        {
            base.AdjustNutrientLevel(change);
            if (NutrientLevel > 10) NutrientLevel = 0; 
        }

        public override int InfluenceNextRadiation()
        {
            return IsAlive() ? 10 : 0;
        }
    }

    class Wittentoot : Plant
    {
        public Wittentoot(string name, int nutrientLevel) : base(name, nutrientLevel) { }

        public override void ReactToRadiation(Radiation radiation)
        {
            if(!IsAlive()) return;

            switch(radiation)
            {
                case Radiation.Alpha:
                    AdjustNutrientLevel(-3);
                    break;
                case Radiation.Delta:
                    AdjustNutrientLevel(4);
                    break;
                case Radiation.None:
                    AdjustNutrientLevel(-1);
                    break;
            }
        }

        public override int InfluenceNextRadiation()
        {
            if (!IsAlive()) return 0;

            if (NutrientLevel < 5) return 4;
            if (NutrientLevel <= 10 ) return 1;
            return 0;
        }
    }

    class Woreroot : Plant
    {
        public Woreroot(string name, int nutrientLevel) : base(name, nutrientLevel) { }

        public override void ReactToRadiation(Radiation radiation)
        {
            if (!IsAlive()) return;

            switch (radiation)
            {
                case Radiation.Alpha:
                    AdjustNutrientLevel(1);
                    break;
                case Radiation.Delta:
                    AdjustNutrientLevel(1);
                    break;
                case Radiation.None:
                    AdjustNutrientLevel(-1);
                    break;

            }
        }
        public override int InfluenceNextRadiation()
        {
            return 0;
        }
    }
}
