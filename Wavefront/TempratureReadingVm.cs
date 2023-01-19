using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class TempratureReadingVm : SensorReadingVm<eTemperature>
    {
        public TempratureReadingVm(IAUVSensor sensor) : base(sensor.GetTemperature, sensor.TemperatureUnit)
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
