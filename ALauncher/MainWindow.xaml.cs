using System.Windows;
using System.Diagnostics;
using ALauncher.Pages;
using System.IO;

namespace ALauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainPage _mainPage;
        private SavesPage? _savesPage;
        private SettingsPage? _settingsPage;

        private readonly Settings _settings;

        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = _mainPage = new MainPage();

            if (File.Exists(Settings.FILE_NAME))
                Settings.Deserialize();
            _settings = Settings.Instance;
            GamesList.SelectedIndex = _settings.SelectedGameIndex;
        }

        private void LaunchGameBtn_Click(object sender, RoutedEventArgs e)
        {
            string processName = _settings.MainSporePath;
            switch (GamesList.SelectedIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(_settings.ModAPIPath))
                    {
                        MessageBox.Show("Пожалуйста, укажите путь до SporeModAPI. " +
                            "Если у вас не установлен Spore ModAPI Launcher," +
                            "вы можете сделать это в открывшемся окне", "Проверьте настройки",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        SettingsBtn_Click(sender, e);
                        Process.Start("explorer.exe",
                            "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html");
                        return;
                    }
                    processName = _settings.ModAPIPath + "\\" + Settings.MODAPI_NAME;
                    break;
                case 1:
                    processName += "\\SporebinEP1\\SporeApp.exe";
                    break;
                case 2:
                    processName += "\\SporeBin\\SporeApp.exe";
                    break;
            }
            Process.Start(processName, _settings.LineArguments);
        }

        private void FilesBtn_Click(object sender, RoutedEventArgs e) =>
            Process.Start("explorer.exe", Settings.Instance.SporeEP1Path);

        private void CreationsBtn_Click(object sender, RoutedEventArgs e) =>
            Process.Start("explorer.exe", Settings.Instance.MySporeCreationsPath);

        private void SavesBtn_Click(object sender, RoutedEventArgs e)
        {
            _savesPage ??= new SavesPage(this);
            MainFrame.Content = _savesPage;
        }

        // Сделать отдельный класс для изменения страницы
        public void OpenMainPage() =>
            MainFrame.Content = _mainPage;

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            _settingsPage ??= new SettingsPage(this);
            MainFrame.Content = _settingsPage;
        }

        private void GamesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!IsInitialized)
                return;

            _settings.SelectedGameIndex = GamesList.SelectedIndex;
            Settings.Serialize();
        }
    }
}
