using ComicHoarder.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public partial class PublisherSearchViewModel : ViewModelBase
    {

        public ICommand SearchPublishersCommand
        {
            get { return new DelegateCommand(SearchPublishers); }
        }

        private void SearchPublishers()
        {
            if (SearchText != "") //SearchText Needs something to search
            {
                List<Publisher> publishers = webDataService.SearchPublishers(SearchText);
                Publishers = new ObservableCollection<Publisher>(publishers);
                if (Publishers.Count() > 0)
                {
                    NotifyPropertyChanged("Publishers", Publishers[0].id);
                }
            }
        }

        public ICommand AddPublisherCommand
        {
            get { return new DelegateCommand(AddPublisher); }
        }

        private void AddPublisher()
        {
            if (SelectedPublisher != 0) //no publisher selected
            {
                Publisher selectedPublisher = (from p in Publishers
                                               where p.id == SelectedPublisher
                                               select p).FirstOrDefault();
                repository.Save(selectedPublisher);
                NotifyPropertyChanged("Publishers", selectedPublisher.id); 
            }
        }

    }
}
