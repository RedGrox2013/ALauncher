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

        private void Hyperlink_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start("explorer.exe",
                ((Hyperlink)sender).NavigateUri.ToString());

        private void NoModAPICheck_Checked(object sender, RoutedEventArgs e) =>
            ModAPIPathDock.Visibility = Visibility.Collapsed;

        private void NoModAPICheck_Unchecked(object sender, RoutedEventArgs e) =>
            ModAPIPathDock.Visibility= Visibility.Visible;

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            bool noModAPI = NoModAPICheck.IsChecked ?? false;
            if (string.IsNullOrWhiteSpace(ModAPIPathBox.Text) && !noModAPI)
            {
                MessageBox.Show("Введите путь до ModAPI", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _settings.ModAPIPath = ModAPIPathBox.Text;
            _settings.IsFirstStart = false;
            _settings.IsSteamVersion = IsSteamVersionCheck.IsChecked ?? false;
            Settings.Serialize();
            Close();
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new();
            var result = folderBrowser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel ||
                string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                return;

            ModAPIPathBox.Text = folderBrowser.SelectedPath;
        }
    }
}
