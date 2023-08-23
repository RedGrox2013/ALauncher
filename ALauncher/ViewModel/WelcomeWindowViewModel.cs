using ALauncher.View;
using System.Windows;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class WelcomeWindowViewModel : BaseSettingsViewModel
    {
        public ICommand HyperlinkCommand { get; private set; }
        public ICommand SaveBtnCommand { get; private set; }

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

        public WelcomeWindowViewModel() : base()
        {
            HyperlinkCommand = new RelayCommand((uri) =>
                System.Diagnostics.Process.Start("explorer.exe",
                uri?.ToString() ?? "https://youtu.be/dQw4w9WgXcQ"));
            SaveBtnCommand = new RelayCommand(SaveSettings);
        }

        protected override void SaveSettings(object? obj)
        {
            if (string.IsNullOrWhiteSpace(ModAPIPath) && !_noModAPI)
            {
                LauncherMessageBox.Show(Locale.GetLocaleString("EnterModAPIPath"),
                    Locale.GetLocaleString("ErrorTitle"), image: LauncherMessageBoxImage.Error);
                return;
            }

            settings.IsFirstStart = false;
            base.SaveSettings(obj);

            // Закрываем окно
            if (obj is Window w)
            {
                w.DialogResult = true;
                w.Close();
            }
        }
    }
}
