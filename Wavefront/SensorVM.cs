namespace Wavefront
{
    public class SensorVM 
    {
        private IAUVSensor _sensor;

        public SensorVM(IAUVSensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));

            ReadValues();
        }

        private void ReadValues()
        {
            Error = false;
            Temprature = ReadValue(_sensor.GetTemperature);
            Pressure = ReadValue(_sensor.GetPressure);
        }

        double ReadValue(Func<double> readFn)
        {
            try
            {
                return readFn();
            }
            catch
            {
                Error = true;
            }
            return 0;
        }

        public double Temprature { get; private set; }

        public double Pressure { get; private set; }

        public bool Error { get; private set; }
    }
}
