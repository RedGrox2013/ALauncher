using ALauncher.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    class SavesPageViewModel : BaseViewModel
    {
        public ICommand RenameCurrentSaveBtnCommand { get; private set; }
        public ICommand RenameSaveBtnCommand { get; private set; }
        public ICommand AddSaveBtnCommand { get; private set; }
        public ICommand DeleteSaveBtnCommand { get; private set; }
        public ICommand SetActiveBtnCommand { get; private set; }

        private ObservableCollection<GameSave>? _saves;
        public ObservableCollection<GameSave>? Saves
        {
            get => _saves;
            set
            {
                _saves = value;
                OnPropertyChanged();
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
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
            _currentSave = GameSave.GetCurrentSaveName("My Galaxy");
            _selectedIndex = -1;

            RenameCurrentSaveBtnCommand = new RelayCommand((o) =>
            {
                CurrentSave = RenameSave(_currentSave);
                GameSave.RenameCurrentSave(_currentSave);
            });
            RenameSaveBtnCommand = new RelayCommand(Rename);
            AddSaveBtnCommand = new RelayCommand(AddSave);
            DeleteSaveBtnCommand = new RelayCommand(DeleteSave);
            SetActiveBtnCommand = new RelayCommand(SetActiveSave);

            var savesDirs = GameSave.CreateSavesDirectory().GetDirectories();
            if (savesDirs.Length == 0)
                return;

            _saves = new ObservableCollection<GameSave>();
            foreach (var save in savesDirs)
                _saves.Add(new GameSave(save));
        }

        private void SetActiveSave(object? obj)
        {
            if (_selectedIndex < 0 || _saves == null)
            {
                ShowIndexError();
                return;
            }

            CurrentSave = _saves[_selectedIndex].Name;
            var oldSave = GameSave.ReplaceCurrentSave(_saves[_selectedIndex]);
            _saves.RemoveAt(_selectedIndex);
            _saves.Add(oldSave);
            OnPropertyChanged(nameof(Saves));
            SelectedIndex = -1;
        }

        private void Rename(object? obj)
        {
            if (_selectedIndex < 0 || _saves == null)
            {
                ShowIndexError();
                return;
            }

            var newName = RenameSave(_saves[_selectedIndex].Name);
            if (newName != _saves[_selectedIndex].Name)
            {
                var index = _selectedIndex;
                var save = _saves[index];
                _saves.Remove(save);
                save.Name = newName;
                _saves.Insert(index, save);
                OnPropertyChanged(nameof(Saves));
            }
        }

        private void DeleteSave(object? obj)
        {
            if (_selectedIndex < 0 || _saves == null)
            {
                ShowIndexError();
                return;
            }

            var rez = LauncherMessageBox.Show(
                "После удаления это сохранение нельзя будет восстановить. Удалить сохранение?",
                "Вы уверены?",
                MessageBoxButton.YesNoCancel, LauncherMessageBoxImage.Warning);
            if (rez != MessageBoxResult.Yes)
                return;

            _saves[_selectedIndex].Delete();
            _saves.RemoveAt(_selectedIndex);
            OnPropertyChanged(nameof(Saves));
            SelectedIndex = -1;
        }

        private void AddSave(object? obj)
        {
            var win = new RenameSaveWindow();
            win.ShowDialog();
            if (string.IsNullOrWhiteSpace(win.SaveName))
                return;

            _saves ??= new ObservableCollection<GameSave>();
            _saves.Add(new GameSave(win.SaveName));
            OnPropertyChanged(nameof(Saves));
        }

        private static string RenameSave(string name)
        {
            var win = new RenameSaveWindow(name);
            win.ShowDialog();

            return string.IsNullOrWhiteSpace(win.SaveName) ? name : win.SaveName;
        }

        private static void ShowIndexError()
            => LauncherMessageBox.Show("Сначала выберите сохранение", "Ошибка!",
                    image: LauncherMessageBoxImage.Error);
    }
}
