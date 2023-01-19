using System.Runtime.CompilerServices;
using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public sealed class SensorVM : INotifyPropertyChanged
    {
        public SensorReadingVm<eTemperature> Temprature { get; init; }         
        public SensorReadingVm<ePressure> Pressure { get; init; }             

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private bool _error;
        public bool Error { get => _error; private set => NotifyPropertyChange(_error = value); }

        public int SensorId { get; }

        public SensorVM(IAUVSensor sensor, IUnitSelections selectedSensorUnits)
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

        private void TryUpdate(Action action)       // The Error property should perhaps be pushed down
        {                                           // So that we display it on the reading which is bad
            try                                     // This is good enough for now though
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
