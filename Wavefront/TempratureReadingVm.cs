using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class TempratureReadingVm : SensorReadingVm<eTemperature>
    {
        public TempratureReadingVm(IAUVSensor sensor, UnitSelection<eTemperature> selectedUnits) : base(sensor.GetTemperature, sensor.TemperatureUnit)
        {
        }

        protected override string Symbol => Units switch
        {
            eTemperature.Celsius => "°C",
            eTemperature.Fahrenheit => "°F",
            _ => "??"
        };
    }
}
