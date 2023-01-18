using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wavefront.AUV.API.Interface;
using System.Linq;

namespace Wavefront
{
    public class SensorsVM
    {
        // this could be required in c# 11
        public IList<SensorVM> Sensors { get; }

        public SensorsVM(Func<IList<IAUVSensor>> sensors) {
            if (sensors == null) throw new ArgumentNullException(nameof(sensors));
            Sensors = sensors().Select(s => new SensorVM(s)).ToArray();

        }
    }
}
