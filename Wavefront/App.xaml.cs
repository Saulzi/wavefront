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
            var sensors = AUV.API.AUVSensorsFactory.Build();

            MainWindow mainWindow = new MainWindow(sensors);
            mainWindow.Show();
        }
    }
}
