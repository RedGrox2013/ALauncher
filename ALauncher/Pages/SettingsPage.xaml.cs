using System;
using System.Collections.Generic;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            _parentWindow.OpenMainPage();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var pathParts = ModAPIPathBox.Text.Split('/', '\\');
            if (pathParts[^1].ToLower() == "spore modapi launcher.exe")
            {
                StringBuilder sb = new(pathParts[0]);
                for (int i = 1; i < pathParts.Length - 1; i++)
                {
                    sb.Append(pathParts[i]);
                    sb.Append('\\');
                }
                _settings.ModAPIPath = sb.ToString();
            }
            else
                _settings.ModAPIPath = ModAPIPathBox.Text;
            Settings.Serialize();
        }
    }
}
