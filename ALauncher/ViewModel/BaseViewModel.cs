using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ALauncher.ViewModel
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string paramName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    }
}
