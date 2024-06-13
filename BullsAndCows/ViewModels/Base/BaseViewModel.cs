using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace BullsAndCows.ViewModels.Base
{
    public class BaseViewModel : MarkupExtension, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public bool Set<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if(Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}