using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand SaveBtnCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

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
        private string _lineArgs;
        public string LineArguments
        {
            get => _lineArgs;
            set
            {
                _lineArgs = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            _settings = Settings.Instance;
            _modAPIPath = _settings.ModAPIPath;
            _isSteamVersion = _settings.IsSteamVersion;
            _lineArgs = _settings.LineArguments;

            SaveBtnCommand = new RelayCommand(SaveSettings);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack(object? obj)
        {
            if (obj is Page page)
                page.NavigationService.GoBack();
        }

        private void SaveSettings(object? obj)
        {
            _settings.ModAPIPath = _modAPIPath;
            _settings.IsSteamVersion = _isSteamVersion;
            _settings.LineArguments = _lineArgs;

            Settings.Serialize();
            GoBack(obj);
        }
    }
}
