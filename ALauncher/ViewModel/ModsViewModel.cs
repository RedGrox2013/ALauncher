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
        public ICommand InstallModCommand { get; private set; }
        public ICommand UninstallModCommand { get; private set; }

        private const string MODAPI_URL = "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html";

        public ModsViewModel()
        {
            _settings = Settings.Instance;

            StartModsManagerCommand = new RelayCommand(ModAPIKitStart);
            InstallModCommand = new RelayCommand(UninstallMod);
            UninstallModCommand = new RelayCommand(UninstallMod);
        }

        private void UninstallMod(object? obj)
        {
            if (string.IsNullOrWhiteSpace(_settings.ModAPIPath))
            {
                LauncherMessageBox.ShowModAPIError();
                Process.Start("explorer.exe", MODAPI_URL);
                return;
            }
        }

        private void ModAPIKitStart(object? fileName)
        {
            if (fileName == null)
                throw new Exception(Locale.GetLocaleString("NotArgExc"));

            try
            {
                Process.Start(_settings.ModAPIPath + "/" + fileName);
            }
            catch
            {
                LauncherMessageBox.ShowModAPIError();
                Process.Start("explorer.exe", MODAPI_URL);
            }
        }
    }
}
