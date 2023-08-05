using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ALauncher.View
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
            LineArgumentsBox.Text = _settings.LineArguments;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) =>
            NavigationService.GoBack();

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
            BackBtn_Click(sender, e);
        }
    }
}
