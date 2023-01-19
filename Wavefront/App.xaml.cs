using System.Timers;

namespace Wavefront
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var sensorsVm = new SensorsVM(AUV.API.AUVSensorsFactory.Build);

            using var timer = new Timer(1000);
            timer.Elapsed += (source, args) =>
            {
                sensorsVm.UpdateSensors();
            };

            timer.Start();

            var mainWindow = new MainWindow(sensorsVm);
            mainWindow.ShowDialog();
        }
    }
}
