using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ALauncher.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private readonly Settings _settings;

        public SettingsPage()
        {
            InitializeComponent();

            _settings = Settings.Instance;
            ModAPIPathBox.Text = _settings.ModAPIPath;
            LineArgumentsBox.Text = _settings.LineArgumetns;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) =>
            NavigationService.GoBack();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo? dir = new(ModAPIPathBox.Text);
            if (dir.Name == Settings.MODAPI_NAME)
                dir = dir.Parent;

            _settings.ModAPIPath = dir?.FullName ?? string.Empty;
            _settings.LineArgumetns = LineArgumentsBox.Text;

            Settings.Serialize();
            BackBtn_Click(sender, e);
        }
    }
}
