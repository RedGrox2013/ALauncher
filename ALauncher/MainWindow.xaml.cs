using System.Windows;
using System.Diagnostics;
using ALauncher.Pages;
using System.IO;
using System;

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

            if (Settings.Instance.IsFirstStart)
                new WelcomeWindow().ShowDialog();
        }
    }
}
