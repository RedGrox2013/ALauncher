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
        }

        private void LaunchGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_settings.SporePath + "\n" + _settings.SporeEP1Path + "\n" +
                _settings.MySporeCreationsPath + "\n" + _settings.ModAPIPath + "\n" +
                _settings.MainSporePath);
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
    }
}
