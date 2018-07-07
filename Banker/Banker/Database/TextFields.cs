using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Banker.Database
{
    public class TextFields : INotifyPropertyChanged
    {
        private string username;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        public TextFields()
        {
        }

        public TextFields(string value)
        {
            this.username = value;
        }

        public string Text
        {
            get => username;
            set
            {
                username = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Text");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(username));
        }
    }
}
