using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALauncher.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        private readonly Settings _settings;

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

        public SettingsViewModel()
        {
            _settings = Settings.Instance;
            _modAPIPath = _settings.ModAPIPath;
        }
    }
}
