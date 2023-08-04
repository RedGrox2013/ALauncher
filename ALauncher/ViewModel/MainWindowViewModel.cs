using System.Diagnostics;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand ShowExplorerCommand { get; set; }

        public string MainSporePath => _settings.MainSporePath;
        public string MySporeCreationsPath => _settings.MySporeCreationsPath;

        public MainWindowViewModel()
        {
            _settings = Settings.Instance;
            ShowExplorerCommand = new RelayCommand((param)
                => Process.Start("explorer.exe", param?.ToString() ?? string.Empty));
        }
    }
}
