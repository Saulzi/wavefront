using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Wavefront
{
    public class SensorsVM : INotifyPropertyChanged
    {
        private IList<IAUVSensor> _sensors;

        // this could be required in c# 11
        public IList<SensorVM> Sensors { get; private set; } = Array.Empty<SensorVM>();

        public SensorsVM(Func<IList<IAUVSensor>> sensors) {
            if (sensors == null) throw new ArgumentNullException(nameof(sensors));
            _sensors = sensors();

            Sensors = _sensors.Select(s => new SensorVM(s)).ToArray();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateSensors()
        {
            foreach (var sensor in Sensors)
            {
                sensor.UpdateValues();
            }
            Sensors = _sensors.Select(s => new SensorVM(s)).ToArray();                  // Note this is a cludge as WPF will not update if the same reference.

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sensors)));
        }
    }
}
