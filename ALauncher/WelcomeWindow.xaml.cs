using System.Windows;
using System.Windows.Documents;

namespace ALauncher
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start("explorer.exe",
                ((Hyperlink)sender).NavigateUri.ToString());
    }
}
