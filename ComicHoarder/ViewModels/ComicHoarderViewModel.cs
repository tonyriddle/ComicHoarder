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
            set 
            { 
                selectedPublisher = Publishers[value].id;
                Task<ObservableCollection<Volume>> t = Task<ObservableCollection<Volume>>.Factory.StartNew(() => UpdateVolumesAsync(Publishers[value].id));
                Volumes.Clear();
                Volumes = t.Result;
                NotifyPropertyChanged("Publisher", value);
                NotifyPropertyChanged("Volumes", value);
            } 
        }

        public int selectedVolume = 0;
        public ObservableCollection<Volume> Volumes { get; set; }
        public int SelectedVolume
        {
            get { return selectedVolume; }
            set 
            {
                selectedVolume = value == -1 ? 0 : Volumes[value].id;
                if (selectedVolume != 0)
                {
                    Task<ObservableCollection<Issue>> t = Task<ObservableCollection<Issue>>.Factory.StartNew(() => UpdateIssuesAsync(Volumes[value].id));
                    Issues.Clear();
                    Issues = t.Result;
                    NotifyPropertyChanged("Issues", value);
                }
                NotifyPropertyChanged("Volumes", value);
            }
        }

        public int selectedIssue = 0;
        public ObservableCollection<Issue> Issues { get; set; }
        public int SelectedIssue
        {
            get { return selectedIssue; }
            set
            {
                selectedIssue = value == -1 ? 0 : Issues[value].id;
                NotifyPropertyChanged("Issues", value);
            }
        }

        public int selectedMissingIssue = 0;
        public ObservableCollection<MissingIssue> MissingIssues { get; set; }
        public int SelectedMissingIssue
        {
            get { return selectedMissingIssue; }
            set
            {
                selectedMissingIssue = value == -1 ? 0 : MissingIssues[value].id;
                NotifyPropertyChanged("MissingIssues", value);
            }
        }

        public PieChartMissingIssueRatio selectedPieChartRatio { get; set; }
        public ObservableCollection<KeyValuePair<string,int>> PieChartRatios { get; set; }
        public PieChartMissingIssueRatio SelectedPieChartRatio
        {
            get { return selectedPieChartRatio; }
            set
            {
                selectedPieChartRatio = value;
                NotifyPropertyChanged("PieChartRatios", 0);
            }
        }

        public BarChartMissingIssueRatio selectedBarChartRatio { get; set; }
        public ObservableCollection<KeyValuePair<string, int>> BarChartRatios { get; set; }
        public BarChartMissingIssueRatio SelectedBarChartRatio
        {
            get { return selectedBarChartRatio; }
            set
            {
                selectedBarChartRatio = value;
                NotifyPropertyChanged("BarChartRatios", 0);
            }
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
            Volumes.Add(new Volume { name = "Spiderman v2", id = 1, publisherId = 31 });
            Volumes.Add(new Volume { name = "Blue Beetle", id = 3, publisherId = 125 });
            Volumes.Add(new Volume { name = "Blue Beetle v2", id = 3, publisherId = 125 });
            selectedVolume = Volumes[0].id;
            //TODO Replace with db call
            Issues = new ObservableCollection<Issue>();
            Issues.Add(new Issue { id = 1, volumeId = 1, name = "Spider-Man 1" });
            Issues.Add(new Issue { id = 2, volumeId = 1, name = "Spider-Man 2" });
            Issues.Add(new Issue { id = 3, volumeId = 2, name = "Spider-Man v2 1" });
            Issues.Add(new Issue { id = 4, volumeId = 2, name = "Spider-Man v2 2" });
            Issues.Add(new Issue { id = 5, volumeId = 3, name = "Blue Beetle 1" });
            Issues.Add(new Issue { id = 6, volumeId = 3, name = "Blue Beetle 2" });
            Issues.Add(new Issue { id = 7, volumeId = 4, name = "Blue Beetle v2 1" });
            Issues.Add(new Issue { id = 8, volumeId = 4, name = "Blue Beetle v2 2" });

            //TODO Replace with db call
            MissingIssues = new ObservableCollection<MissingIssue>();
            MissingIssues.Add(new MissingIssue { id = 1, name = "Wolverine 1", volume_id = 5 });

            //TODO Replace with db call
            ObservableCollection<KeyValuePair<string,int>> pieChartRatio = new ObservableCollection<KeyValuePair<string, int>>();
            PieChartMissingIssueRatio ratios = new PieChartMissingIssueRatio();
            ratios.MissingIssueRatioList.Add(new KeyValuePair<string, int>("Collected", 40638));
            ratios.MissingIssueRatioList.Add(new KeyValuePair<string, int>("Missing", 2235));
            foreach (KeyValuePair<string, int> ratio in ratios.MissingIssueRatioList)
            {
                pieChartRatio.Add(ratio);
            }
            PieChartRatios = pieChartRatio;

            ObservableCollection<KeyValuePair<string, int>> barChartRatio = new ObservableCollection<KeyValuePair<string, int>>();
            BarChartMissingIssueRatio barratios = new BarChartMissingIssueRatio();
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("Marvel", 5638));
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("Atlas", 2235));
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("Timely", 1244));
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("Icon", 1508));
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("Amalgam", 1007));
            barratios.MissingIssueCountList.Add(new KeyValuePair<string, int>("MAX", 500));
            foreach (KeyValuePair<string, int> barratio in barratios.MissingIssueCountList)
            {
                barChartRatio.Add(barratio);
            }
            BarChartRatios = barChartRatio;

        }

        public ObservableCollection<Volume> UpdateVolumesAsync(int publisherId)
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
            return newVolumes;
        }

        public ObservableCollection<Issue> UpdateIssuesAsync(int volumeId)
        {
            ObservableCollection<Issue> newIssues = new ObservableCollection<Issue>();
            if (volumeId == 1)
            {
                newIssues.Add(new Issue { id = 1, volumeId = 1, name = "Spider-Man 1" });
                newIssues.Add(new Issue { id = 2, volumeId = 1, name = "Spider-Man 2" });
            }
            else if(volumeId == 2)
            {
                newIssues.Add(new Issue { id = 3, volumeId = 2, name = "Spider-Man v2 1" });
                newIssues.Add(new Issue { id = 4, volumeId = 2, name = "Spider-Man v2 2" });
            }
            else if (volumeId == 3)
            {
                newIssues.Add(new Issue { id = 5, volumeId = 3, name = "Blue Beetle 1" });
                newIssues.Add(new Issue { id = 6, volumeId = 3, name = "Blue Beetle 2" });
            }
            else if (volumeId == 4)
            {
                newIssues.Add(new Issue { id = 7, volumeId = 4, name = "Blue Beetle v2 1" });
                newIssues.Add(new Issue { id = 8, volumeId = 4, name = "Blue Beetle v2 2" });
            }
            return newIssues;
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
