using ALauncher.View;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class ModsViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public ICommand StartModsManagerCommand { get; private set; }

        public ModsViewModel()
        {
            _settings = Settings.Instance;

            StartModsManagerCommand = new RelayCommand(ModAPIKitStart);
        }

        private void ModAPIKitStart(object? fileName)
        {
            if (fileName == null)
                throw new Exception(Locale.GetLocaleString("NotArgExc"));

            try
            {
                Process.Start(_settings.ModAPIPath + "\\" + fileName);
            }
            catch
            {
                LauncherMessageBox.ShowModAPIError();
                Process.Start("explorer.exe",
                    "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html");
            }
        }
    }
}
