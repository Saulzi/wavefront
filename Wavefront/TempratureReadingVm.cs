using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class TempratureReadingVm : SensorReadingVm<eTemperature>
    {
        private readonly UnitSelection<eTemperature> _selectedUnits;

        public TempratureReadingVm(IAUVSensor sensor, UnitSelection<eTemperature> selectedUnits) : base(() => (sensor.GetTemperature(), sensor.TemperatureUnit), val => UnitConversions.ConvertTemprature(val, selectedUnits.Value))
        {
            _selectedUnits = selectedUnits;
        }

        protected override string Symbol => _selectedUnits.Value switch
        {
            eTemperature.Celsius => "°C",
            eTemperature.Fahrenheit => "°F",
            _ => "??"
        };
    }
}
