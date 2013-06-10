using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ComicHoarder.Common;

namespace ComicHoarder.ViewModels
{
    public class PublisherSelectorViewModel : ViewModelBase
    {
        private int selectedPublisher = 0;
        public ObservableCollection<Publisher> Publishers { get; set; }
        public int SelectedPublisher { get { return selectedPublisher; } set { selectedPublisher = value;  NotifyPropertyChanged("Publisher", value); } }


        public PublisherSelectorViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            //TODO Replace with db call
            Publishers.Add(new Publisher { name = "Marvel", id = 31 });
            Publishers.Add(new Publisher { name = "Charlton", id = 125 });
            selectedPublisher = Publishers[0].id;
        }
    }
}
