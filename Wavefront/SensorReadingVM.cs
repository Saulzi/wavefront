namespace Wavefront
{

    public abstract class SensorReadingVm<UnitEnum> : INotifyPropertyChanged
    {
        protected Func<double> _readValue;
        private double _value;
        public UnitEnum Units { get; init; }

        public string Value => $"{_value:#,0.000} {Symbol}";

        public SensorReadingVm(Func<double> readValue, UnitEnum units)
        {
            _readValue = readValue;
            Units = units;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ReadValue()
        {
            _value = _readValue();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        protected abstract string Symbol { get; }

        public static implicit operator double(SensorReadingVm<UnitEnum> o) => o._value;
    }
}
