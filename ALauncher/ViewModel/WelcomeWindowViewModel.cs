using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALauncher.ViewModel
{
    internal class WelcomeWindowViewModel : BaseViewModel
    {
        private readonly Settings _settings;

        public string ModAPIPath
        {
            get => _settings.ModAPIPath;
            set
            {
                _settings.ModAPIPath = value;
                OnPropertyChanged();
            }
        }

        public WelcomeWindowViewModel()
        {
            _settings = Settings.Instance;
        }
    }
}
