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

        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("UserName");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(username));
            }
        }
    }
}
