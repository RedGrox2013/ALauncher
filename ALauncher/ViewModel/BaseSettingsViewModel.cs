using System.Windows.Input;

namespace ALauncher.ViewModel
{
    abstract class BaseSettingsViewModel : BaseViewModel
    {
        protected readonly Settings settings;

        public ICommand BrowseBtnCommand { get; private set; }
        public ICommand BrowseSteamBtnCommand { get; private set; }

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
        private string? _steamPath;
        public string? SteamPath
        {
            get => _steamPath;
            set
            {
                _steamPath = value;
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
            _steamPath = settings.SteamPath;
            _isSteamVersion = settings.IsSteamVersion;
            _lineArgs = settings.LineArguments;
            _lang = settings.Language;

            BrowseBtnCommand = new RelayCommand(BrowseModAPIFolder);
            BrowseSteamBtnCommand = new RelayCommand((o) =>
                SteamPath = BrowseFile() ?? SteamPath);
        }

        protected virtual void SaveSettings(object? obj)
        {
            settings.ModAPIPath = _modAPIPath.TrimEnd('\\', '/');
            settings.SteamPath = _steamPath;
            settings.IsSteamVersion = _isSteamVersion;
            settings.LineArguments = _lineArgs;
            settings.Language = _lang;

            Settings.Serialize();
        }

        protected virtual void BrowseModAPIFolder(object? obj)
        {
            using System.Windows.Forms.FolderBrowserDialog folderBrowser = new();
            var result = folderBrowser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel ||
                string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                return;

            ModAPIPath = folderBrowser.SelectedPath;
        }

        protected virtual string? BrowseFile()
        {
            using System.Windows.Forms.OpenFileDialog openFile = new()
            {
                Filter = "Исполняемый файл (*.exe)|*.exe"
            };
            var result = openFile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel ||
                string.IsNullOrWhiteSpace(openFile.FileName))
                return null;

            return openFile.FileName;
        }
    }
}
