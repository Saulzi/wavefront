
using Wavefront.AUV.API.Enums;

namespace Wavefront
{

    public class SensorReadingVm<UnitEnum> : INotifyPropertyChanged
    {
        private Func<double> _readValue;
        private double _value;
        private object Units;

        public string Value => $"{_value:#,0.000} {Symbol()}";

        public SensorReadingVm(IAUVSensor sensor)
        {
            if (typeof(UnitEnum) == typeof(eTemperature))           // Not very open closed but good enough for our use case
            {
                _readValue = sensor.GetTemperature;
                Units = sensor.TemperatureUnit;
            } 

            else if (typeof(UnitEnum) == typeof(ePressure))
            {
                _readValue = sensor.GetPressure;
                Units = sensor.PressureUnit;
            }

            // Should be a build time error
            else { throw new Exception(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ReadValue()
        {
            _value = _readValue();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        private string Symbol()
        {
            return Units switch
            {
                ePressure.PSI => "psi",                         // Not very open closed but good enough for our use case
                ePressure.kPa => "kPa",
                eTemperature.Celsius => "°C",
                eTemperature.Fahrenheit => "°F",
                _ => "??"
            };
        }

        public static implicit operator double(SensorReadingVm<UnitEnum> o) => o._value;
    }
}
