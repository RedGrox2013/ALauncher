using System.Collections.Generic;

namespace ALauncher.ViewModel
{
    class SavesPageViewModel : BaseViewModel
    {
        private List<GameSave>? _saves;
        public List<GameSave>? Saves
        {
            get => _saves;
            set
            {
                _saves = value;
                OnPropertyChanged();
            }
        }
        private string _currentSave;
        public string CurrentSave
        {
            get => _currentSave;
            set
            {
                _currentSave = value;
                OnPropertyChanged();
            }
        }

        public SavesPageViewModel()
        {
            _currentSave = GameSave.GetCurrentSave("My Galaxy");

            var savesDirs = GameSave.CreateSavesDirectory().GetDirectories();
            if (savesDirs.Length == 0)
                return;

            _saves = new List<GameSave>(savesDirs.Length);
            foreach (var save in savesDirs)
                _saves.Add(new GameSave(save));
        }
    }
}
