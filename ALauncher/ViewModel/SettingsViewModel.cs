using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class SettingsViewModel : BaseSettingsViewModel
    {
        public ICommand SaveBtnCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        public SettingsViewModel() : base()
        {
            SaveBtnCommand = new RelayCommand(SaveSettings);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack(object? obj)
        {
            if (obj is Page page)
                page.NavigationService.GoBack();
        }

        protected override void SaveSettings(object? obj)
        {
            base.SaveSettings(obj);
            GoBack(obj);
        }
    }
}
