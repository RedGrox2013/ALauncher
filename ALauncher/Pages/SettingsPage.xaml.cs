using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            LineArgumentsBox.Text = _settings.LineArgumetns;
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            _parentWindow.OpenMainPage();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo? dir = new(ModAPIPathBox.Text);
            if (dir.Name == Settings.MODAPI_NAME)
                dir = dir.Parent;

            _settings.ModAPIPath = dir?.FullName ?? string.Empty;
            _settings.LineArgumetns = LineArgumentsBox.Text;

            Settings.Serialize();
            _parentWindow.OpenMainPage();
        }
    }
}
