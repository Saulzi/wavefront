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
            var sensorsVm = new SensorsVM(AUV.API.AUVSensorsFactory.Build); 
            MainWindow mainWindow = new MainWindow(sensorsVm);
            mainWindow.Show();
        }
    }
}
