using System;

namespace CarBuilder
{
    public class FuelTank : IFuelTank
    {
        private const double MaxFillLevel = 60;
        private const double MinFillLevelToTurnOffReserve = 5;

        public double FillLevel { get; private set; }
        public bool IsOnReserve { get; private set; }

        public FuelTank()
        {
            FillLevel = 20; // Initial fuel level
            IsOnReserve = false;
        }

        public void Refuel(double liters)
        {
            FillLevel += liters;
            if (FillLevel > MaxFillLevel)
            {
                FillLevel = MaxFillLevel;
                Console.WriteLine("Bak jest pełen");
            }
            if (FillLevel > MinFillLevelToTurnOffReserve)
            {
                IsOnReserve = false;
            }
        }

        public void Consume(double liters)
        {
            FillLevel -= liters;
            if (FillLevel < MinFillLevelToTurnOffReserve)
            {
                IsOnReserve = true;
                if (FillLevel < 0)
                {
                    FillLevel = 0;
                    Console.WriteLine("Bak jest pusty");
                }
            }
        }

        public double CalculateFuelConsumption(int speed)
        {
            return speed switch
            {
                int s when s >= 1 && s <= 60 => 0.0020,
                int s when s <= 100 => 0.0014,
                int s when s <= 140 => 0.0020,
                int s when s <= 200 => 0.0025,
                int s when s <= 250 => 0.0030,
                _ => 0.0,
            };
        }
    }
}
