using System;

namespace CarBuilder
{
    public class Car : ICar
    {
        private const int MaxSpeed = 250;
        private const int MaxDeceleration = 10;

        private readonly IEngine _engine;
        private readonly IFuelTank _fuelTank;
        private readonly IFuelTankDisplay _fuelTankDisplay;
        private int _speed;
        private int _acceleration;

        public Car(IEngine engine, IFuelTank fuelTank, IFuelTankDisplay fuelTankDisplay, int acceleration = 10)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _fuelTank = fuelTank ?? throw new ArgumentNullException(nameof(fuelTank));
            _fuelTankDisplay = fuelTankDisplay ?? throw new ArgumentNullException(nameof(fuelTankDisplay));

            _acceleration = Math.Clamp(acceleration, 5, 20);
            _speed = 0;
        }

        public int CurrentSpeed => _speed;

        public bool EngineIsRunning => _engine.IsRunning;

        public IFuelTankDisplay FuelTankDisplay => _fuelTankDisplay;

        public void EngineStart() => _engine.Start();

        public void EngineStop() => _engine.Stop();

        public void Refuel(double liters) => _fuelTank.Refuel(liters);

        public void RunningIdle()
        {
            if (_engine.IsRunning)
            {
                Move();
            }
        }

        public void AccelerateTo(int targetSpeed)
        {
            if (!_engine.IsRunning || _speed >= targetSpeed) return;

            _speed = Math.Min(_speed + _acceleration, targetSpeed);
            _speed = Math.Min(_speed, MaxSpeed);

            ConsumeFuel();
        }

        public void BrakeBy(int deceleration)
        {
            if (!_engine.IsRunning) return;

            _speed = Math.Max(_speed - Math.Min(deceleration, MaxDeceleration), 0);
            ConsumeFuel();
        }

        private void Move()
        {
            if (_speed > 0)
            {
                _speed = Math.Max(_speed - 1, 0); // Slow down due to air and rolling resistance
            }

            ConsumeFuel();
        }

        private void ConsumeFuel()
        {
            double consumptionRate = CalculateFuelConsumptionRate();
            _fuelTank.Consume(consumptionRate);
        }

        private double CalculateFuelConsumptionRate()
        {
            return _speed switch
            {
                int speed when speed >= 1 && speed <= 60 => 0.0020,
                int speed when speed <= 100 => 0.0014,
                int speed when speed <= 140 => 0.0020,
                int speed when speed <= 200 => 0.0025,
                int speed when speed <= 250 => 0.0030,
                _ => 0.0,
            };
        }
    }
}
