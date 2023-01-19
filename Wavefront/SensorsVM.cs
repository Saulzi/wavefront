using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class SensorsVM : IUnitSelections
    {
        public IList<SensorVM> Sensors { get; private set; } = Array.Empty<SensorVM>();

        public UnitSelection<eTemperature> TempratureUnit { get; } = UnitSelection.CreateTemprature();
        public UnitSelection<ePressure> PressureUnit { get; } = UnitSelection.CreatePressure();

        public SensorsVM(Func<IList<IAUVSensor>> sensors)
        {
            if (sensors == null) throw new ArgumentNullException(nameof(sensors));

            Sensors = sensors().Select(s => new SensorVM(s, this)).ToArray();
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
