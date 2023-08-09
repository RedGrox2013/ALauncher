using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace ALauncher.ViewModel
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand GoBackCommand { get; private set; }

        public BaseViewModel()
        {
            GoBackCommand = new RelayCommand(GoBack);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string paramName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));

        protected virtual void GoBack(object? obj)
        {
            if (obj is Page page)
                page.NavigationService.GoBack();
        }
    }
}
