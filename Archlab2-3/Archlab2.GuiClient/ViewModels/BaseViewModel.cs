using System.ComponentModel;

namespace Archlab2.GuiClient
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
