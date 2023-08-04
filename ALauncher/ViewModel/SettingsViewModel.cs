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

        public string MainSporePath => _settings.MainSporePath;

        public SettingsViewModel()
        {
            _settings = Settings.Instance;
        }
    }
}
