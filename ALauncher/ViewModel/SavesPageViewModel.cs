using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALauncher.ViewModel
{
    class SavesPageViewModel : BaseViewModel
    {
        private List<GameSave> _saves;
        public List<GameSave> Saves
        {
            get => _saves;
            set
            {
                _saves = value;
                OnPropertyChanged();
            }
        }

        public SavesPageViewModel()
        {
            // Доделать
        }
    }
}
