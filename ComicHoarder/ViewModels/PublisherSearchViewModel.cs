using ComicHoarder.Common;
using ComicHoarder.Repository;
using ComicHoarder.WebData;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public partial class PublisherSearchViewModel : ViewModelBase
    {
        IRepository repository;
        IWebDataService webDataService;

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
            webDataService = new WebDataService("UseComicVineScraperKey");
            repository = new RepositoryService();
            Publishers = new ObservableCollection<Publisher>();
        }
    }
}
