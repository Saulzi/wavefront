namespace Wavefront
{
    public class SensorsVM 
    {
        // this could be required in c# 11
        public IList<SensorVM> Sensors { get; private set; } = Array.Empty<SensorVM>();

        public SensorsVM(Func<IList<IAUVSensor>> sensors) {
            if (sensors == null) throw new ArgumentNullException(nameof(sensors));

            Sensors = sensors().Select(s => new SensorVM(s)).ToArray();
        }

        public void UpdateSensors()
        {
            foreach (var sensor in Sensors)
            {
                sensor.UpdateValues();
            }
        }
    }
}
