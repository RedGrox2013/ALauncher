using ALauncher.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand ShowExplorerCommand { get; private set; }
        public ICommand SavesBtnCommand { get; private set; }
        public ICommand SettingsBtnCommand { get; private set; }
        public ICommand LaunchGameCommand { get; private set; }

        public string MainSporePath => _settings.MainSporePath;
        public string MySporeCreationsPath => _settings.MySporeCreationsPath;
        public int SelectedGameIndex
        {
            get => _settings.SelectedGameIndex;
            set
            {
                _settings.SelectedGameIndex = value;
                OnPropertyChanged();
            }
        }

        private SavesPage? _savesPage;
        private SettingsPage? _settingsPage;
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private const string EXPLORER = "explorer.exe";

        public MainWindowViewModel()
        {
            _settings = Settings.Instance;
            _currentPage = new MainPage();

            ShowExplorerCommand = new RelayCommand((param) =>
                Process.Start(EXPLORER, param?.ToString() ?? string.Empty));
            SavesBtnCommand = new RelayCommand((o) =>
                CurrentPage = _savesPage ??= new SavesPage());
            SettingsBtnCommand = new RelayCommand((o) =>
                CurrentPage = _settingsPage ??= new SettingsPage());
            LaunchGameCommand = new RelayCommand(LaunchGame);
        }

        private void LaunchGame(object? arg)
        {
            string processName = _settings.MainSporePath;
            string arguments = _settings.LineArguments + " locale:";
            arguments += _settings.Language switch
            {
                SporeLanguages.Russian => "ru-ru",
                SporeLanguages.Czech => "cs-cz",
                SporeLanguages.Danish => "da-dk",
                _ => "en-us",
            };
            switch (SelectedGameIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(_settings.ModAPIPath))
                    {
                        MessageBox.Show("Пожалуйста, укажите путь до SporeModAPI. " +
                            "Если у вас не установлен Spore ModAPI Launcher, " +
                            "вы можете сделать это в открывшемся окне", "Проверьте настройки",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        //SettingsBtn_Click(sender, e);
                        processName = EXPLORER;
                        arguments = "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html";
                    }
                    else
                        processName = _settings.ModAPIPath + "\\" + Settings.MODAPI_NAME;
                    break;
                case 1:
                    if (_settings.IsSteamVersion)
                    {
                        processName = EXPLORER;
                        arguments = "steam://rungameid/24720";
                    }
                    else
                        processName += "\\SporebinEP1\\SporeApp.exe";
                    break;
                case 2:
                    if (_settings.IsSteamVersion)
                    {
                        processName = EXPLORER;
                        arguments = "steam://rungameid/17390";
                    }
                    else
                        processName += "\\SporeBin\\SporeApp.exe";
                    break;
                default:
                    return;
            }
            try
            {
                // Сделать чтобы аргументы работали и через стим
                Process.Start(processName, arguments);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
