using System.Windows;
using System.Windows.Documents;

namespace ALauncher
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private readonly Settings _settings;

        public WelcomeWindow()
        {
            InitializeComponent();
            _settings = Settings.Instance;
        }

        // Перенести в vm
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //bool noModAPI = NoModAPICheck.IsChecked ?? false;
            //if (string.IsNullOrWhiteSpace(ModAPIPathBox.Text) && !noModAPI)
            //{
            //    MessageBox.Show("Введите путь до ModAPI", "Ошибка!",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //_settings.ModAPIPath = ModAPIPathBox.Text;
            _settings.IsFirstStart = false;
            //_settings.IsSteamVersion = IsSteamVersionCheck.IsChecked ?? false;
            Settings.Serialize();
            Close();
        }
    }
}
