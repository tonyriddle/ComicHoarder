using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string publisher, int id)
        {
            if (PropertyChanged != null)
            {
                string selectedPublisher = publisher;
                PropertyChanged(this, new PropertyChangedEventArgs(publisher));
            }
        }
    }
}
