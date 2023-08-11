using System;
using System.Diagnostics;
using System.Windows;
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
                throw new Exception("Не указан аргумент");

            try
            {
                if (_settings.ModAPIPath[^1] != '\\' || _settings.ModAPIPath[^1] != '/')
                    _settings.ModAPIPath += "\\";
                Process.Start(_settings.ModAPIPath + fileName?.ToString());
            }
            catch
            {
                MessageBox.Show(
                            "Пожалуйста, укажите путь до SporeModAPI. " +
                            "Если у вас не установлен Spore ModAPI Launcher, " +
                            "вы можете сделать это в открывшемся окне", "Проверьте настройки",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                Process.Start("explorer.exe",
                    "http://davoonline.com/sporemodder/rob55rod/ModAPI/Public/index.html");
            }
        }
    }
}
