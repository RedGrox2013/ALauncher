using System.Windows.Controls;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для SavesPage.xaml
    /// </summary>
    public partial class SavesPage : Page
    {
        public SavesPage()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, System.Windows.RoutedEventArgs e) =>
            NavigationService.GoBack();
    }
}
