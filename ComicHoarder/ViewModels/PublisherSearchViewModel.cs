using ComicHoarder.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public partial class PublisherSearchViewModel : ViewModelBase
    {
        int selectedPublisher = 0;
        public ObservableCollection<Publisher> Publishers { get; set; }
        public int SelectedPublisher { 
            get { return selectedPublisher; } 
            set { selectedPublisher = Publishers[value].id; 
                NotifyPropertyChanged("Publishers", value); } 
        }
        public string SearchText { get; set; }


        public PublisherSearchViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            //TODO Remove altogether once web call works
            Publishers.Add(new Publisher { name = "Spiderman", id = 1 });
            Publishers.Add(new Publisher { name = "Spiderman", id = 1});
            Publishers.Add(new Publisher { name = "Blue Beetle", id = 3});
            Publishers.Add(new Publisher { name = "Blue Beetle", id = 3});
            selectedPublisher = Publishers[0].id;
        }

    }
}
