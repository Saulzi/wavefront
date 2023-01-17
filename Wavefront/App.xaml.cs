using System.Linq;
using System.Windows;

namespace Wavefront
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            SensorVM[] sensorVms = GetSensorVms();

            MainWindow mainWindow = new MainWindow(sensorVms);
            mainWindow.Show();
        }

        private static SensorVM[] GetSensorVms()
        {
            var sensors = AUV.API.AUVSensorsFactory.Build();
            var sensorVms = sensors.Select(sensor => new SensorVM(sensor))
                                   .ToArray();
            return sensorVms;
        }
    }
}
