using System.Windows;
using System.Diagnostics;
using ALauncher.Pages;
using System.IO;
using System;

namespace ALauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SavesPage? _savesPage;
        private SettingsPage? _settingsPage;

        private readonly Settings _settings;

        private const string EXPLORER = "explorer.exe";

        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Content = new MainPage();

            if (File.Exists(Settings.FILE_NAME))
                Settings.Deserialize();
            _settings = Settings.Instance;
            GamesList.SelectedIndex = _settings.SelectedGameIndex;

            if (_settings.IsFirstStart)
                new WelcomeWindow().ShowDialog();
        }

        private void LaunchGameBtn_Click(object sender, RoutedEventArgs e)
        {
            string processName = _settings.MainSporePath;
            string arguments = _settings.LineArguments;
            switch (GamesList.SelectedIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(_settings.ModAPIPath))
                    {
                        MessageBox.Show("Пожалуйста, укажите путь до SporeModAPI. " +
                            "Если у вас не установлен Spore ModAPI Launcher, " +
                            "вы можете сделать это в открывшемся окне", "Проверьте настройки",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        SettingsBtn_Click(sender, e);
                        processName = EXPLORER;
                        arguments = "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html";
                    }
                    else
                        processName = _settings.ModAPIPath + "\\" + Settings.MODAPI_NAME;
                    break;
                case 1:
                    if (_settings.IsSteamVersion)
                    {
                        processName = EXPLORER;
                        arguments = "steam://rungameid/24720";
                    }
                    else
                        processName += "\\SporebinEP1\\SporeApp.exe";
                    break;
                case 2:
                    if (_settings.IsSteamVersion)
                    {
                        processName = EXPLORER;
                        arguments = "steam://rungameid/17390";
                    }
                    else
                        processName += "\\SporeBin\\SporeApp.exe";
                    break;
                default:
                    return;
            }
            try
            {
                // Сделать чтобы аргументы работали и через стим
                Process.Start(processName, arguments);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilesBtn_Click(object sender, RoutedEventArgs e) =>
            Process.Start(EXPLORER, Settings.Instance.SporeEP1Path);

        private void CreationsBtn_Click(object sender, RoutedEventArgs e) =>
            Process.Start(EXPLORER, Settings.Instance.MySporeCreationsPath);

        private void SavesBtn_Click(object sender, RoutedEventArgs e)
        {
            _savesPage ??= new SavesPage();
            MainFrame.Content = _savesPage;
        }

        //public void OpenMainPage() =>
        //    MainFrame.Content = _mainPage;

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            _settingsPage ??= new SettingsPage();
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
