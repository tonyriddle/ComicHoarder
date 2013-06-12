using ComicHoarder.Common;
using System.Collections.ObjectModel;

namespace ComicHoarder.ViewModels
{
    class PublisherSearchViewModel : ViewModelBase
    {
        int selectedPublisher = 0;
        public ObservableCollection<Publisher> Publishers { get; set; }
        public int SelectedPublisher { get { return selectedPublisher; } set { selectedPublisher = value; NotifyPropertyChanged("Publishers", value); } }

        public PublisherSearchViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            //TODO Replace with db call
            Publishers.Add(new Publisher { name = "Spiderman", id = 1 });
            Publishers.Add(new Publisher { name = "Spiderman", id = 1});
            Publishers.Add(new Publisher { name = "Blue Beetle", id = 3});
            Publishers.Add(new Publisher { name = "Blue Beetle", id = 3});
            selectedPublisher = Publishers[0].id;
        }
    }
}
