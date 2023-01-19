using System.Runtime.CompilerServices;
using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class SensorVM : INotifyPropertyChanged
    {
        public SensorReadingVm<eTemperature> Temprature { get; init; }         // What a shame the new field bit did not make it into c# 10/11
        public SensorReadingVm<ePressure> Pressure { get; init; }

        private bool _error;                

        public event PropertyChangedEventHandler? PropertyChanged;


        public bool Error { get => _error; private set => NotifyPropertyChange(_error = value); }

        public int SensorId { get; }

        public SensorVM(IAUVSensor sensor, ISelectedSensorUnits selectedSensorUnits)
        {
            if (sensor == null)
            {
                throw new ArgumentNullException(nameof(sensor));
            }

            SensorId = sensor.SensorId;

            Temprature = new TempratureReadingVm(sensor, selectedSensorUnits.TempratureUnit);
            Pressure = new PressureReadingVm(sensor, selectedSensorUnits.PressureUnit);
            
            UpdateValues();
        }

        public void UpdateValues()
        {
            Error = false;

            TryUpdate(Temprature.ReadValue);
            TryUpdate(Pressure.ReadValue);
        }

        private void TryUpdate(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                Error = true;
            }
        }

        private T NotifyPropertyChange<T>(T value, [CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return value;
        }

    }
}
