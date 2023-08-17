using ALauncher.View;
using System.Collections.Generic;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    class SavesPageViewModel : BaseViewModel
    {
        public ICommand RenameCurrentSaveBtnCommand { get; set; }

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
                GameSave.RenameCurrentSave(value);
            }
        }

        public SavesPageViewModel()
        {
            _currentSave = GameSave.GetCurrentSave("My Galaxy");
            RenameCurrentSaveBtnCommand = new RelayCommand((o) => CurrentSave = RenameSave(_currentSave));

            var savesDirs = GameSave.CreateSavesDirectory().GetDirectories();
            if (savesDirs.Length == 0)
                return;

            _saves = new List<GameSave>(savesDirs.Length);
            foreach (var save in savesDirs)
                _saves.Add(new GameSave(save));
        }

        private static string RenameSave(string name)
        {
            var win = new RenameSaveWindow(name);
            win.ShowDialog();
            
            return string.IsNullOrWhiteSpace(win.SaveName) ? name : win.SaveName;
        }
    }
}
