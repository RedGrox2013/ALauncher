using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ALauncher.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private readonly MainWindow _parentWindow;
        private readonly Settings _settings;

        public SettingsPage(MainWindow parentWindow)
        {
            InitializeComponent();

            _parentWindow = parentWindow;
            _settings = Settings.Instance;

            ModAPIPathBox.Text = _settings.ModAPIPath;
            LineArgumentsBox.Text = _settings.LineArguments;
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            _parentWindow.OpenMainPage();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ModAPIPathBox.Text))
            {
                DirectoryInfo? dir = new(ModAPIPathBox.Text);
                if (dir.Name == Settings.MODAPI_NAME)
                    dir = dir.Parent;
                _settings.ModAPIPath = dir?.FullName ?? string.Empty;
            }
            _settings.LineArguments = LineArgumentsBox.Text;

            Settings.Serialize();
            _parentWindow.OpenMainPage();
        }
    }
}
