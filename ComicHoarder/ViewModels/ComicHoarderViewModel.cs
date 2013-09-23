using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ComicHoarder.Common;
//using System.Windows;
using ComicHoarder.Repository;
using ComicHoarder.WebData;
using System.Windows.Forms;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public partial class ComicHoarderViewModel : ViewModelBase
    {
        IRepository repository;
        IWebDataService webDataService;

        private int selectedPublisher = 0;
        public ObservableCollection<Publisher> Publishers { get; set; }
        public int SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = Publishers[value].id;
                Task<ObservableCollection<Volume>> t = Task<ObservableCollection<Volume>>.Factory.StartNew(() => ReloadVolumesAsync(Publishers[value].id));
                Task<ObservableCollection<KeyValuePair<string, int>>> c = Task<ObservableCollection<KeyValuePair<string, int>>>.Factory.StartNew(() => ReloadPieChart(Publishers[value].id));
                Volumes.Clear();
                Volumes = t.Result;
                Issues.Clear();
                MissingIssues.Clear();
                MissingIssues = new ObservableCollection<MissingIssue>(repository.GetMissingIssues(selectedPublisher));
                PieChartRatios = c.Result;
                NotifyPropertyChanged("Publishers", value);
                NotifyPropertyChanged("Volumes", value);
                NotifyPropertyChanged("Issues", value);
                NotifyPropertyChanged("MissingIssues", value);
                NotifyPropertyChanged("PieChartRatios", value);
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
                    Task<ObservableCollection<Issue>> t = Task<ObservableCollection<Issue>>.Factory.StartNew(() => ReloadIssuesAsync(Volumes[value].id));
                    Issues.Clear();
                    Issues = t.Result;
                    NotifyPropertyChanged("Issues", value);
                }
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
        public ObservableCollection<KeyValuePair<string, int>> PieChartRatios { get; set; }
        public PieChartMissingIssueRatio SelectedPieChartRatio
        {
            get { return selectedPieChartRatio; }
            set
            {
                selectedPieChartRatio = value;
                NotifyPropertyChanged("PieChartRatios", 0);
            }
        }

        public BarChartMissingIssueCount selectedBarChartRatio { get; set; }
        public ObservableCollection<KeyValuePair<string, int>> BarChartRatios { get; set; }
        public BarChartMissingIssueCount SelectedBarChartRatio
        {
            get { return selectedBarChartRatio; }
            set
            {
                selectedBarChartRatio = value;
                NotifyPropertyChanged("BarChartRatios", 0);
            }
        }

        public string Messages { get; set; }

        public string Path { get; set; }

        public ComicHoarderViewModel()
        {
            repository = new RepositoryService();
            webDataService = new WebDataService(repository.GetSetting("ComicVineKey"));

            Publishers = new ObservableCollection<Publisher>(repository.GetPublishers());
            if (Publishers.Count() > 0) 
            {
                selectedPublisher = Publishers[0].id; //select first publisher
            }

            if (selectedPublisher != 0) //no publisher selected
            {
                Volumes = new ObservableCollection<Volume>(repository.GetVolumes(selectedPublisher)); //get volumes from selected publisher
                if (Volumes.Count() > 0)
                {
                    selectedVolume = Volumes[0].id; //select first volume
                }
            }
            else
            {
                Volumes = new ObservableCollection<Volume>(); //empty volumes
            }

            if (selectedVolume != 0)
            {
                Issues = new ObservableCollection<Issue>(repository.GetIssues(selectedVolume));
                if (Issues.Count() > 0)
                {
                    selectedIssue = Issues[0].id;
                }
            }
            else
            {
                Issues = new ObservableCollection<Issue>();
            }

            MissingIssues = new ObservableCollection<MissingIssue>(repository.GetMissingIssues(selectedPublisher));

            PieChartRatios = ReloadPieChart(SelectedPublisher);

            BarChartRatios = new ObservableCollection<KeyValuePair<string, int>>();
            List<int> publisherIds = (from p in Publishers
                                        where p.enabled == true
                                        select p.id).ToList();
            BarChartMissingIssueCount barratios = repository.GetBarChartData(publisherIds);
            foreach (KeyValuePair<string, int> barratio in barratios.MissingIssueCountList)
            {
                BarChartRatios.Add(barratio);
            }
            //TODO move default path to config file
            Path = @"D:\Incoming\";
        }

        private ObservableCollection<KeyValuePair<string, int>> ReloadPieChart(int publisherId)
        {
            var pieChartRatios = new ObservableCollection<KeyValuePair<string, int>>();
            PieChartMissingIssueRatio ratios = repository.GetPieChartData(publisherId);
            foreach (KeyValuePair<string, int> ratio in ratios.MissingIssueRatioList)
            {
                pieChartRatios.Add(ratio);
            }
            return pieChartRatios;
        }

    }
}
