namespace Wavefront
{

    public abstract class SensorReadingVm<UnitEnum> : INotifyPropertyChanged
    {
        protected Func<(double value, UnitEnum unit)> _readValue;
        private readonly Func<(double value, UnitEnum unit), double> convertUnit;
        private double _value;

        public string Value => $"{_value:#,0.000} {Symbol}";

        public SensorReadingVm(Func<(double value, UnitEnum unit)> readValue, Func<(double value, UnitEnum unit), double> convertUnit)
        {
            _readValue = readValue;
            this.convertUnit = convertUnit;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ReadValue()
        {
            var value = _readValue();
            _value = convertUnit(value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        protected abstract string Symbol { get; }

        public static implicit operator double(SensorReadingVm<UnitEnum> o) => o._value;
    }
}
