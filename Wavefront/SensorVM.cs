namespace Wavefront
{
    public class SensorVM 
    {
        private IAUVSensor _sensor;

        public double Temprature { get; private set; }

        public double Pressure { get; private set; }

        public bool Error { get; private set; }

        public int SensorId { get; }

        public SensorVM(IAUVSensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
            SensorId = sensor.SensorId; 
            UpdateValues();
        }

        public void UpdateValues()
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

    }
}
