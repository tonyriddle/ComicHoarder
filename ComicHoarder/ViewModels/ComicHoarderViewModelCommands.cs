using ComicHoarder.Common;
using ComicHoarder.EComic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ComicHoarder.ViewModels
{
    public partial class ComicHoarderViewModel : ViewModelBase
    {
        private ComicHoarder.AddPublisher pubWindow;
        private FolderBrowserDialog folderBrowser;

        public ObservableCollection<Volume> ReloadVolumesAsync(int publisherId)
        {
            return new ObservableCollection<Volume>(repository.GetVolumes(publisherId));
        }

        public ObservableCollection<Issue> ReloadIssuesAsync(int volumeId)
        {
            return new ObservableCollection<Issue>(repository.GetIssues(volumeId));
        }

        public ICommand ShowAddPublisherWindowCommand
        {
            get { return new DelegateCommand(ShowAddPublisherWindow); }
        }

        private void ShowAddPublisherWindow()
        {
            if (pubWindow == null)
            {
                pubWindow = new ComicHoarder.AddPublisher();
                pubWindow.Closed += AddPublisherWindowClosed;
                pubWindow.Show();
            }
        }

        private void AddPublisherWindowClosed(object sender, EventArgs e)
        {
            pubWindow = null;
            Publishers = new ObservableCollection<Publisher>(repository.GetPublishers());
            PrintMessage("Added new Publisher...");
            NotifyPropertyChanged("Publishers", 0);
            NotifyPropertyChanged("Messages", 0);
        }

        public ICommand AddVolumesCommand
        {
            get { return new DelegateCommand(AddVolumesAsync); }
        }

        private void AddVolumesAsync()
        {
            Task<bool> t = Task<bool>.Factory.StartNew(() => AddVolumes());
        }

        private bool AddVolumes() //Adds volumes based on selected publisher
        {
            int total = 0;
            int count = 0;
            List<Volume> skeletonVolumes = webDataService.GetVolumesFromPublisher(SelectedPublisher);
            total = skeletonVolumes.Count();
            foreach (Volume vol in skeletonVolumes)
            {
                count++;
                if (!repository.VolumeExists(vol.id))
                {
                    PrintMessage("Getting Volume " + count + " of " + total + " - " + vol.name + "...");
                    NotifyPropertyChanged("Messages", 0);
                    Volume volume = webDataService.GetVolume(vol.id);
                    repository.Save(volume);
                }
            }
            Volumes = new ObservableCollection<Volume>(repository.GetVolumes(SelectedPublisher));
            NotifyPropertyChanged("Volumes", 0);
            return true;
        }

        public ICommand AddIssuesCommand
        {
            get { return new DelegateCommand(AddIssuesAsync); }
        }

        public void AddIssuesAsync()
        {
            Task<bool> t = Task<bool>.Factory.StartNew(() => AddIssues());
        }

        private bool AddIssues() //Add issues by selected volume
        {
            int total = 0;
            int count = 0;
            List<Issue> skeletonIssues = webDataService.GetIssuesFromVolume(SelectedVolume);
            total = skeletonIssues.Count();
            foreach (Issue iss in skeletonIssues)
            {
                count++;
                if (!repository.IssueExists(iss.id))
                {
                    PrintMessage("Getting Issue " + count + " of " + total + " - " + iss.name + "...");
                    NotifyPropertyChanged("Messages", 0);
                    Issue issue = webDataService.GetIssue(iss.id);
                    repository.Save(issue);
                }
            }
            Issues = new ObservableCollection<Issue>(repository.GetIssues(SelectedVolume));
            NotifyPropertyChanged("Issues", 0);
            return true;
        }

        public ICommand FindIssuesCommand
        {
            get { return new DelegateCommand(FindIssuesAsync); }
        }

        private void FindIssuesAsync()
        {
            Task<bool> t = Task<bool>.Factory.StartNew(() => FindIssues());
        }

        private bool FindIssues() //Find any issues by selected publisher
        {
            int total = 0;
            int count = 0;
            int totalv = 0;
            int countv = 0;
            List<Volume> skeletonVolumes = webDataService.GetVolumesFromPublisher(SelectedPublisher);
            totalv = skeletonVolumes.Count();
            foreach (Volume vol in skeletonVolumes)
            {
                countv++;
                if (!repository.VolumeExists(vol.id)) //if volume does not exist, then get and save
                {
                    repository.Save(webDataService.GetVolume(vol.id));
                }
                List<Issue> skeletonIssues = webDataService.GetIssuesFromVolume(vol.id);
                total = skeletonIssues.Count();
                count = 0;
                foreach (Issue iss in skeletonIssues)
                {
                    count++;
                    if (!repository.IssueExists(iss.id))
                    {
                        PrintMessage("Getting Volume " + countv + " of " + totalv + ", Issue " + count + " of " + total + " - " + iss.name + "...");
                        NotifyPropertyChanged("Messages", 0);
                        Issue issue = webDataService.GetIssue(iss.id);
                        repository.Save(issue);
                    }
                }
            }
            Issues = new ObservableCollection<Issue>(repository.GetIssues(SelectedVolume));
            NotifyPropertyChanged("Issues", 0);
            return true;
        }

        public ICommand BrowseMissingIssuesCommand
        {
            get { return new DelegateCommand(BrowseMissingIssues); }
        }

        private void BrowseMissingIssues()
        {
            folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                Path = folderBrowser.SelectedPath;
            }
            NotifyPropertyChanged("Path", 1);
        }

        public ICommand CollectMissingIssuesCommand
        {
            get { return new DelegateCommand(CollectMissingIssuesAsync); }
        }

        private void CollectMissingIssuesAsync()
        {
            Task<bool> t = Task<bool>.Factory.StartNew(() => CollectMissingIssues());
        }

        private bool CollectMissingIssues()
        {
            IEComicService eComicService = new EComicService();
            List<Issue> issues = eComicService.GetIssues(Path, true);
            int count = 0;
            foreach(Issue issue in issues)
            {
                count++;
                repository.UpdateIssueToCollected(issue.id);
                PrintMessage("Updating Issue " + count + " of " + issues.Count() + " - " + issue.name + "...");
                NotifyPropertyChanged("Messages", 0);
            }
            PrintMessage("Updating Issues Complete.");
            NotifyPropertyChanged("Messages", 0);
            return true;
        }

        public ICommand ExportMissingIssuesCommand
        {
            get { return new DelegateCommand(ExportMissingIssues); }
        }

        private void ExportMissingIssues()
        {
            //TODO implement export missing issues
            MessageBox.Show("Export MissingIssues");
            int p = selectedMissingIssue;
            int myCount = MissingIssues.Count();
            string SomeText = String.Empty;
        }

        private void PrintMessage(string message)
        {
            Messages = Messages + "\n" + message;
        }

    }
}
