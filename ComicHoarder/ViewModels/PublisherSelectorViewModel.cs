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
    public class PublisherSelectorViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Publisher> Publishers { get; set; }
        public ObservableCollection<Publisher> SelectedPublishers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PublisherSelectorViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            SelectedPublishers = new ObservableCollection<Publisher>();
        }
    }
}
