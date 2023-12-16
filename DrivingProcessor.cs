namespace CarBuilder
{
    public class DrivingProcessor : IDrivingProcessor
    {
        private const int MaxSpeed = 250;
        private const int MaxDeceleration = 10;
        private int _acceleration;

        public DrivingProcessor(int acceleration = 10)
        {
            _acceleration = Math.Clamp(acceleration, 5, 20);
            ActualSpeed = 0;
        }

        public int ActualSpeed { get; private set; }

        public void IncreaseSpeedTo(int speed) => ActualSpeed = Math.Min(speed, MaxSpeed);

        public void ReduceSpeed(int speed) => ActualSpeed = Math.Max(ActualSpeed - speed, 0);

        public void AccelerateTo(int speed)
        {
            var targetSpeed = Math.Min(speed, MaxSpeed);
            ActualSpeed = Math.Min(ActualSpeed + _acceleration, targetSpeed);
        }

        public void BrakeBy(int amount) => ActualSpeed = Math.Max(ActualSpeed - Math.Min(amount, MaxDeceleration), 0);

        public void Move()
        {
            if (ActualSpeed > 0)
            {
                ActualSpeed = Math.Max(ActualSpeed - 1, 0);
            }
        }
    }
}
