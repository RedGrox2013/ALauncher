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
using System.Windows.Shapes;

namespace ALauncher.View
{
    /// <summary>
    /// Логика взаимодействия для LauncherMessageBox.xaml
    /// </summary>
    public partial class LauncherMessageBox : Window
    {
        public MessageBoxResult Result { get; private set; }

        public LauncherMessageBox(string message, string? title = null)
        {
            InitializeComponent();
            TxtBlck.Text = message;
            Title = title ?? string.Empty;
        }

        public static MessageBoxResult Show(string message, string? title = null)
        {
            var msgBox = new LauncherMessageBox(message, title);
            msgBox.ShowDialog();
            return msgBox.Result;
        }
    }
}
