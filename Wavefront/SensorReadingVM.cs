namespace Wavefront
{

    public abstract class SensorReadingVm<UnitEnum> : INotifyPropertyChanged
    {
        protected Func<(double value, UnitEnum unit)> _readValue;
        private double _value;

        public string Value => $"{_value:#,0.000} {Symbol}";

        public SensorReadingVm(Func<(double value, UnitEnum unit)> readValue)
        {
            _readValue = readValue;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ReadValue()
        {
            _value = _readValue().value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        protected abstract string Symbol { get; }

        public static implicit operator double(SensorReadingVm<UnitEnum> o) => o._value;
    }
}
