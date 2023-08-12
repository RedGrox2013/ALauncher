using System.Windows;
using System.Windows.Controls;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для LauncherMessageBox.xaml
    /// </summary>
    public partial class LauncherMessageBox : Window
    {
        private MessageBoxResult _result;

        public LauncherMessageBox(object message, string? title = null,
            MessageBoxButton button = MessageBoxButton.OK)
        {
            InitializeComponent();

            TxtBlck.Text = message.ToString();
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;

            switch (button)
            {
                case MessageBoxButton.OK:
                    OkBtn.Visibility = Visibility.Visible;
                    _result = MessageBoxResult.OK;
                    break;
                case MessageBoxButton.OKCancel:
                    OkBtn.Visibility = CancelBtn.Visibility = Visibility.Visible;
                    _result = MessageBoxResult.Cancel;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesBtn.Visibility = NoBtn.Visibility =
                        CancelBtn.Visibility = Visibility.Visible;
                    _result = MessageBoxResult.Cancel;
                    break;
                case MessageBoxButton.YesNo:
                    YesBtn.Visibility = NoBtn.Visibility = Visibility.Visible;
                    _result = MessageBoxResult.No;
                    break;
            }
        }

        public static MessageBoxResult Show(object message, string? title = null,
            MessageBoxButton button = MessageBoxButton.OK)
        {
            var msgBox = new LauncherMessageBox(message, title, button);
            msgBox.ShowDialog();
            return msgBox._result;
        }
        public static void ShowModAPIError()
            => Show("Пожалуйста, укажите путь до Spore ModAPI. " +
                    "Если у вас не установлен Spore ModAPI Launcher, " +
                    "вы можете сделать это в открывшемся окне",
                    "Проверьте настройки");

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn == OkBtn)
                    _result = MessageBoxResult.OK;
                else if (btn == CancelBtn)
                    _result = MessageBoxResult.Cancel;
                else if (btn == YesBtn)
                    _result = MessageBoxResult.Yes;
                else if (btn == NoBtn)
                    _result = MessageBoxResult.No;
            }
            Close();
        }
    }
}
