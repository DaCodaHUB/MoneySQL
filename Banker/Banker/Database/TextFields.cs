using System.ComponentModel;

namespace Banker.Database
{
    public class TextFields : INotifyPropertyChanged
    {
        private string _username;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        public TextFields()
        {
        }

        public TextFields(string value)
        {
            _username = value;
        }

        public string Text
        {
            get => _username;
            set
            {
                _username = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Text");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_username));
        }
    }
}
