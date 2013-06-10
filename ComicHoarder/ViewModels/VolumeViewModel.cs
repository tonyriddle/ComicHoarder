using ComicHoarder.Common;
using System;
using System.Collections.ObjectModel;

namespace ComicHoarder.ViewModels
{
    class VolumeViewModel : ViewModelBase
    {
        int selectedVolume = 0;
        public ObservableCollection<Volume> Volumes { get; set; }
        public int SelectedVolume { get { return selectedVolume; } set { selectedVolume = value; NotifyPropertyChanged("Volume", value); } }

        public VolumeViewModel()
        {
            Volumes = new ObservableCollection<Volume>();
            //TODO Replace with db call
            Volumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
            Volumes.Add(new Volume { name = "Spiderman", id = 1, publisherId = 31 });
            Volumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            Volumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            selectedVolume = Volumes[0].id;

        }
    }
}
