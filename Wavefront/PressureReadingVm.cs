using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class PressureReadingVm : SensorReadingVm<ePressure>
    {
        private readonly UnitSelection<ePressure> _selectedUnits;

        public PressureReadingVm(IAUVSensor sensor, UnitSelection<ePressure> selectedUnits) : base(() => (sensor.GetPressure(), sensor.PressureUnit))
        {
            _selectedUnits = selectedUnits;
        }

        protected override string Symbol => _selectedUnits.Value switch
        {
            ePressure.PSI => "psi",                        
            ePressure.kPa => "kPa",
            _ => "??"
        };

        protected override double ConvertUnits((double value, ePressure unit) value) => UnitConversions.ConvertPressure(value, _selectedUnits.Value);
    }
}

