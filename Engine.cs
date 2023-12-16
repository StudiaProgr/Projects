namespace CarBuilder
{
    public class Engine : IEngine
    {
        public bool IsRunning => _isRunning;
        private bool _isRunning;

        public readonly IFuelTank _fuelTank;

        public Engine(IFuelTank fuelTank)
        {
            _fuelTank = fuelTank ?? throw new ArgumentNullException(nameof(fuelTank));
        }

        public void Consume(double liters)
        {
            _fuelTank.Consume(liters);
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
