using System.Windows;
using System.Windows.Controls;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

#if DEBUG
        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherMessageBox.Show("Test");
            LauncherMessageBox.Show("Test 2 njkdsanfkjsdngjkdfsngjbfdjh gbhjfdbghj dfjhfgbhjds bvjhsdb ghjdsg usdg jgsdfyug uydfgf uhdsfg yudgfhjfvdg hjdhg", "Title");
        }
#endif
    }
}
