using System.Windows;

namespace Wavefront
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(SensorsVM sensors)
        {
            DataContext = sensors ?? throw new ArgumentNullException(nameof (sensors));    

            InitializeComponent();
        }
    }
}
