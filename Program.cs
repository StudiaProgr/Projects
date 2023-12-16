using System;
using System.Threading;

namespace CarBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            FuelTank fuelTank = new FuelTank();
            Engine engine = new Engine(fuelTank);
            FuelTankDisplay fuelTankDisplay = new FuelTankDisplay(fuelTank);
            Car car = new Car(engine, fuelTank, fuelTankDisplay, 10); // Set acceleration

            car.EngineStart();

            // Accelerate to 250 km/h
            while (car.CurrentSpeed < 250)
            {
                car.AccelerateTo(250);
                Console.WriteLine($"Speed: {car.CurrentSpeed} km/h, Fuel Level: {car.FuelTankDisplay.FillLevel} liters");
                Thread.Sleep(1000); // Simulate one second passing
            }

            // Continue driving idle
            while (car.CurrentSpeed > 0)
            {
                car.RunningIdle(); // Coasting, decrease speed by 1 km/h
                Console.WriteLine($"Coasting. Current speed: {car.CurrentSpeed} km/h");
                Thread.Sleep(1000); // Simulate one second passing
            }

            car.EngineStop();
            Console.WriteLine("The car has stopped.");
        }
    }
}
