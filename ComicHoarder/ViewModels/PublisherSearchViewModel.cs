using ComicHoarder.Common;
using ComicHoarder.Repository;
using ComicHoarder.WebDataProvider;
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
            set
            {
                if (value != -1 && value != 0)
                {
                    selectedPublisher = value;
                    NotifyPropertyChanged("Publishers", value);
                }
            } 
        }
        public string SearchText { get; set; }

        public PublisherSearchViewModel()
        {
            repository = new RepositoryService();
            webDataService = new WebDataService(repository.GetSetting("ComicVineKey"));
            Publishers = new ObservableCollection<Publisher>();
        }
    }
}
