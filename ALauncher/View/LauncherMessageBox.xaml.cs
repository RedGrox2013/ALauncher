using System;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для LauncherMessageBox.xaml
    /// </summary>
    public partial class LauncherMessageBox : Window
    {
        private MessageBoxResult _result;

        public LauncherMessageBox(object message, string? title,
            MessageBoxButton button, LauncherMessageBoxImage image)
        {
            InitializeComponent();

            MsgBoxText.Text = message.ToString();
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
            if (image != LauncherMessageBoxImage.None)
            {
                MsgBoxIcon.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/MessageBox/{image}.png"));
                using SoundPlayer soundPlayer = new();
                switch (image)
                {
                    case LauncherMessageBoxImage.Error:
                        soundPlayer.SoundLocation = @"C:\Windows\Media\Windows Foreground.wav";
                        break;
                    case LauncherMessageBoxImage.Question:
                        soundPlayer.SoundLocation = @"C:\Windows\Media\Windows Ding.wav";
                        break;
                    case LauncherMessageBoxImage.Information:
                    case LauncherMessageBoxImage.Warning:
                        soundPlayer.SoundLocation = @"C:\Windows\Media\Windows Background.wav";
                        break;
                }
                soundPlayer.Play();
            }
        }

        public static MessageBoxResult Show(object message, string? title = null,
            MessageBoxButton button = MessageBoxButton.OK, LauncherMessageBoxImage image = LauncherMessageBoxImage.None)
        {
            var msgBox = new LauncherMessageBox(message, title, button, image);
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
            var btn = sender as Button;
            if (btn == OkBtn)
                _result = MessageBoxResult.OK;
            else if (btn == CancelBtn)
                _result = MessageBoxResult.Cancel;
            else if (btn == YesBtn)
                _result = MessageBoxResult.Yes;
            else if (btn == NoBtn)
                _result = MessageBoxResult.No;
            Close();
        }
    }

    public enum LauncherMessageBoxImage
    {
        None,
        Error = 16,
        Question = 32,
        Warning = 48,
        Information = 64
    }
}
