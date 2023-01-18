using System.Runtime.CompilerServices;

namespace Wavefront
{
    public class SensorVM : INotifyPropertyChanged
    {
        private readonly IAUVSensor _sensor;

        private double _temprature;         // What a shame the new field bit did not make it into c# 10/11
        private double _pressure;
        private bool _error;                

        public event PropertyChangedEventHandler? PropertyChanged;

        public double Temprature { get => _temprature; private set => NotifyPropertyChange(_temprature = value); }

        public double Pressure { get => _pressure; private set => NotifyPropertyChange(_pressure = value); }

        public bool Error { get => _error; private set => NotifyPropertyChange(_error = value); }

        public int SensorId { get; }

        public SensorVM(IAUVSensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
            SensorId = sensor.SensorId; 
            UpdateValues();
        }

        public void UpdateValues()
        {
            Error = false;
            Temprature = ReadValue(_sensor.GetTemperature);
            Pressure = ReadValue(_sensor.GetPressure);
        }

        double ReadValue(Func<double> readFn)
        {
            try
            {
                return readFn();
            }
            catch
            {
                Error = true;
            }
            return 0;
        }

        private T NotifyPropertyChange<T>(T value, [CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return value;
        }

    }
}
