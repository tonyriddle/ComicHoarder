using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ComicHoarder.Common;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public class ComicHoarderViewModel : ViewModelBase
    {
        private int selectedPublisher = 0;
        public ObservableCollection<Publisher> Publishers { get; set; }
        public int SelectedPublisher 
        { 
            get { return selectedPublisher; } 
            set { selectedPublisher = Publishers[value].id;
                Task t = Task.Factory.StartNew(() => UpdateVolumes(Publishers[value].id));
                  NotifyPropertyChanged("Publisher", value); } 
        }

        public int selectedVolume = 0;
        public ObservableCollection<Volume> Volumes { get; set; }
        public int SelectedVolume
        {
            get { return selectedVolume; }
            set {
                selectedVolume = Volumes[value].id;
                NotifyPropertyChanged("Volume", value); }
        }



        public ComicHoarderViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
            //TODO Replace with db call
            Publishers.Add(new Publisher { name = "Marvel", id = 31 });
            Publishers.Add(new Publisher { name = "Charlton", id = 125 });
            selectedPublisher = Publishers[0].id;
            Volumes = new ObservableCollection<Volume>();
            //TODO Replace with db call
            Volumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
            Volumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
            Volumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            Volumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            selectedVolume = Volumes[0].id;
        }

        public void UpdateVolumes(int publisherId)
        {
            ObservableCollection<Volume> newVolumes = new ObservableCollection<Volume>();
            if (publisherId == 31)
            {
                newVolumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
                newVolumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
            }
            else
            {
                newVolumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
                newVolumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            }


        }

        public ICommand AddPublisherCommand
        {
            get { return new DelegateCommand(AddPublisher); }
        }

        private void AddPublisher()
        {
            int p = selectedPublisher;
            int myCount = Publishers.Count();
            string SomeText = String.Empty;
        }

        public ICommand AddVolumesCommand
        {
            get { return new DelegateCommand(AddVolumes); }
        }

        private void AddVolumes()
        {
            int p = selectedVolume;
            int myCount = Volumes.Count();
            string SomeText = String.Empty;
        }

        public ICommand FindVolumesCommand
        {
            get { return new DelegateCommand(FindVolumes); }
        }

        private void FindVolumes()
        {
            int p = selectedVolume;
            int myCount = Volumes.Count();
            string SomeText = String.Empty;
        }
    }
}
