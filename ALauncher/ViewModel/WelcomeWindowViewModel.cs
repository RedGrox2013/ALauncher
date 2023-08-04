using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class WelcomeWindowViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand HyperlinkCommand { get; set; }
        public ICommand BrowseBtnCommand { get; set; }

        public string ModAPIPath
        {
            get => _settings.ModAPIPath;
            set
            {
                _settings.ModAPIPath = value;
                OnPropertyChanged();
            }
        }
        public bool? IsSteamVersion
        {
            get => _settings.IsSteamVersion;
            set
            {
                _settings.IsSteamVersion = value ?? false;
                OnPropertyChanged();
            }
        }
        private bool _noModAPI = false;
        public bool? NoModAPI
        {
            get => _noModAPI;
            set
            {
                _noModAPI = value ?? false;
                OnPropertyChanged();
                ModAPIPathVisibility = _noModAPI ? Visibility.Collapsed :
                    Visibility.Visible;
            }
        }
        private Visibility _modAPIVisibility = Visibility.Visible;
        public Visibility ModAPIPathVisibility
        {
            get => _modAPIVisibility;
            private set
            {
                _modAPIVisibility = value;
                OnPropertyChanged();
            }
        }

        public WelcomeWindowViewModel()
        {
            _settings = Settings.Instance;

            HyperlinkCommand = new RelayCommand((uri) =>
                System.Diagnostics.Process.Start("explorer.exe",
                uri?.ToString() ?? "https://youtu.be/dQw4w9WgXcQ"));
            BrowseBtnCommand = new RelayCommand(BrowseFolder);
        }

        private void BrowseFolder(object? obj)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new();
            var result = folderBrowser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel ||
                string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                return;

            ModAPIPath = folderBrowser.SelectedPath;
        }
    }
}
