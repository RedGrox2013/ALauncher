using System.Windows.Input;

namespace ALauncher.ViewModel
{
    abstract class BaseSettingsViewModel : BaseViewModel
    {
        protected readonly Settings settings;

        public ICommand BrowseBtnCommand { get; private set; }

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
        private SporeLanguages _lang;
        public int SelectedLanguageIndex
        {
            get => (int)_lang;
            set
            {
                _lang = (SporeLanguages)value;
                OnPropertyChanged();
            }
        }

        public BaseSettingsViewModel()
        {
            settings = Settings.Instance;
            _modAPIPath = settings.ModAPIPath;
            _isSteamVersion = settings.IsSteamVersion;
            _lineArgs = settings.LineArguments;
            _lang = settings.Language;

            BrowseBtnCommand = new RelayCommand(BrowseFolder);
        }

        protected virtual void SaveSettings(object? obj)
        {
            settings.ModAPIPath = _modAPIPath;
            settings.IsSteamVersion = _isSteamVersion;
            settings.LineArguments = _lineArgs;
            settings.Language = _lang;

            Settings.Serialize();
        }

        protected virtual void BrowseFolder(object? obj)
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
