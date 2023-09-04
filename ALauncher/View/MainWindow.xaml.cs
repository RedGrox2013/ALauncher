using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ALauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var rand = new Random();
            if (rand.Next(50) == 0)
                BackgroundImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/secretbg.png"));

            if (Settings.Instance.IsFirstStart)
                new WelcomeWindow().ShowDialog();
        }
    }
}
