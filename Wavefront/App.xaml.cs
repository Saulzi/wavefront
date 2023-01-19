using System.Timers;

namespace Wavefront
{
    /// <summary>
    /// The Main Entry point to the application, Note that this has been hooked up to use the OnStartup
    /// Callback to Inject the SensorsViewMode, Really the little bit around refreshing every second should not be here
    /// But this is very small
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
