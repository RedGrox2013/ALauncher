using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal class SettingsViewModel : BaseSettingsViewModel
    {
        public ICommand SaveBtnCommand { get; private set; }

        public SettingsViewModel() : base()
        {
            SaveBtnCommand = new RelayCommand(SaveSettings);
        }

        protected override void SaveSettings(object? obj)
        {
            base.SaveSettings(obj);
            GoBack(obj);
        }
    }
}
