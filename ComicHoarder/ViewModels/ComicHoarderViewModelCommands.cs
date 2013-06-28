using ComicHoarder.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            Publishers = new ObservableCollection<Publisher>(repository.GetPublishers());
            PrintMessage("Added new Publisher...");
            NotifyPropertyChanged("Publisher", 0);
        }

        private void AddPublisherWindowClosed(object sender, EventArgs e)
        {
            pubWindow = null;
        }

        public ICommand AddVolumesCommand
        {
            get { return new DelegateCommand(AddVolumes); }
        }

        private void AddVolumes() //Adds volumes based on selected publisher
        {
            List<Volume> skeletonVolumes = webDataService.GetVolumesFromPublisher(SelectedPublisher);
            foreach (Volume vol in skeletonVolumes)
            {
                if (!repository.VolumeExists(vol.id))
                {
                    PrintMessage("Getting Volume - " + vol.name + "...");
                    Volume volume = webDataService.GetVolume(vol.id);
                    repository.Save(volume);
                }
            }
            Volumes = new ObservableCollection<Volume>(repository.GetVolumes(SelectedPublisher));
            NotifyPropertyChanged("Volumes", 0);
        }

        public ICommand AddIssuesCommand
        {
            get { return new DelegateCommand(AddIssues); }
        }

        private void AddIssues() //Add issues by selected volume
        {
            List<Issue> skeletonIssues = webDataService.GetIssuesFromVolume(SelectedVolume);
            foreach (Issue iss in skeletonIssues)
            {
                if (!repository.IssueExists(iss.id))
                {
                    Issue issue = webDataService.GetIssue(iss.id);
                    repository.Save(issue);
                }
            }
            Issues = new ObservableCollection<Issue>(repository.GetIssues(SelectedVolume));
            NotifyPropertyChanged("Issues", 0);
        }

        public ICommand FindIssuesCommand
        {
            get { return new DelegateCommand(FindIssues); }
        }

        private void FindIssues() //Find any issues by selected publisher
        {
            List<Volume> skeletonVolumes = webDataService.GetVolumesFromPublisher(SelectedPublisher);
            foreach (Volume vol in skeletonVolumes)
            {
                if (!repository.VolumeExists(vol.id)) //if volume does not exist, then get and save
                {
                    repository.Save(webDataService.GetVolume(vol.id));
                }
                List<Issue> skeletonIssues = webDataService.GetIssuesFromVolume(vol.id);
                foreach (Issue iss in skeletonIssues)
                {
                    if (!repository.IssueExists(iss.id))
                    {
                        Issue issue = webDataService.GetIssue(iss.id);
                        repository.Save(issue);
                    }
                }
            }
            Issues = new ObservableCollection<Issue>(repository.GetIssues(SelectedVolume));
            NotifyPropertyChanged("Issues", 0);
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
            get { return new DelegateCommand(CollectMissingIssues); }
        }

        private void CollectMissingIssues()
        {
            //implement collect (update do collected) missing issues - including adding issues - volumes - publishers if necessary
            MessageBox.Show("Collecting MissingIssues");
            int p = selectedMissingIssue;
            int myCount = MissingIssues.Count();
            string SomeText = String.Empty;
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
            Messages = Messages + "/n" + message;
        }
    }
}
