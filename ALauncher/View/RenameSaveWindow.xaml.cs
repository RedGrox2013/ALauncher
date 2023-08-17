using System.Windows;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для RenameSaveWindow.xaml
    /// </summary>
    public partial class RenameSaveWindow : Window
    {
        public string? SaveName { get; set; }

        public RenameSaveWindow(string? saveName = null)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(saveName))
                NameTextBox.Text = saveName;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveName = NameTextBox.Text;
            Close();
        }
    }
}
