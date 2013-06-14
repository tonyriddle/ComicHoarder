using ComicHoarder.Common;
using System;
using System.Collections.Generic;
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
            //TODO Search Publishers and Fill Publishers with results
            MessageBox.Show("Search Publishers " + SearchText);
            Publishers.Add(new Publisher { name = "found in search", id = 4 });
        }

        public ICommand AddPublisherCommand
        {
            get { return new DelegateCommand(AddPublisher); }
        }

        private void AddPublisher()
        {
            //TODO save selected publisher
            MessageBox.Show("hi " + SelectedPublisher);
        }

    }
}
