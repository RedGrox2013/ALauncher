using System.Windows;

namespace ALauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Instance.IsFirstStart)
                new WelcomeWindow().ShowDialog();
        }
    }
}
