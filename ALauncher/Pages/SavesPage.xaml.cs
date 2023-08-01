using System.Windows.Controls;

namespace ALauncher.Pages
{
    /// <summary>
    /// Логика взаимодействия для SavesPage.xaml
    /// </summary>
    public partial class SavesPage : Page
    {
        private readonly MainWindow _parentWindow;

        public SavesPage(MainWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }

        private void OkBtn_Click(object sender, System.Windows.RoutedEventArgs e) =>
            _parentWindow.OpenMainPage();
    }
}
