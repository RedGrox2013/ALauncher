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
            var rez = LauncherMessageBox.Show("Test");
            rez = LauncherMessageBox.Show(rez,
                button:MessageBoxButton.OKCancel);
            rez = LauncherMessageBox.Show(rez,
                button: MessageBoxButton.YesNoCancel);
            rez = LauncherMessageBox.Show(rez,
                button: MessageBoxButton.YesNo);
            MessageBox.Show(rez.ToString());
        }
#endif
    }
}
