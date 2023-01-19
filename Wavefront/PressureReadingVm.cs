using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class PressureReadingVm : SensorReadingVm<ePressure>
    {
        public PressureReadingVm(IAUVSensor sensor) : base(sensor.GetPressure, sensor.PressureUnit)
        {
        }

        protected override string Symbol => Units switch
        {
            ePressure.PSI => "psi",                        
            ePressure.kPa => "kPa",
            _ => "??"
        };
    }
}
