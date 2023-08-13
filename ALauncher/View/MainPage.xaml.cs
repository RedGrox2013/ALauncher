using System.Windows;
using System.Windows.Controls;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

#if DEBUG
        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            var rez = LauncherMessageBox.Show("Test", image:LauncherMessageBoxImage.Question);
            MessageBox.Show(rez.ToString(), "Test", MessageBoxButton.OK, MessageBoxImage.Question);
        }
#endif
    }
}
