using System;
using System.Windows;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class WelcomeWindowViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand HyperlinkCommand { get; private set; }
        public ICommand BrowseBtnCommand { get; private set; }
        public ICommand SaveBtnCommand { get; private set; }

        private string _modAPIPath;
        public string ModAPIPath
        {
            get => _modAPIPath;
            set
            {
                _modAPIPath = value;
                OnPropertyChanged();
            }
        }
        private bool _isSteamVersion;
        public bool? IsSteamVersion
        {
            get => _isSteamVersion;
            set
            {
                _isSteamVersion = value ?? false;
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
            _modAPIPath = _settings.ModAPIPath;
            _isSteamVersion = _settings.IsSteamVersion;

            HyperlinkCommand = new RelayCommand((uri) =>
                System.Diagnostics.Process.Start("explorer.exe",
                uri?.ToString() ?? "https://youtu.be/dQw4w9WgXcQ"));
            BrowseBtnCommand = new RelayCommand(BrowseFolder);
            SaveBtnCommand = new RelayCommand(SaveSettings);
        }

        private void SaveSettings(object? obj)
        {
            if (string.IsNullOrWhiteSpace(_modAPIPath) && !_noModAPI)
            {
                MessageBox.Show("Введите путь до ModAPI", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _settings.IsFirstStart = false;
            _settings.ModAPIPath = _modAPIPath;
            _settings.IsSteamVersion = _isSteamVersion;
            Settings.Serialize();

            // Закрываем окно
            if (obj is Window w)
            {
                w.DialogResult = true;
                w.Close();
            }
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
