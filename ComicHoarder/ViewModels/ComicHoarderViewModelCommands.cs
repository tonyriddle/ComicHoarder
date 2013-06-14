using ComicHoarder.Common;
using System;
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
        
        //TODO at least pull these out to a partial class, new class would be better
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
            //MessageBox.Show("Hello There");
            int p = selectedPublisher;
            int myCount = Publishers.Count();
            string SomeText = String.Empty;
        }

        private void AddPublisherWindowClosed(object sender, EventArgs e)
        {
            pubWindow = null;
        }

        public ICommand AddVolumesCommand
        {
            get { return new DelegateCommand(AddVolumes); }
        }

        private void AddVolumes()
        {
            MessageBox.Show("Add Volumes");
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
            MessageBox.Show("Find Volumes");
            int p = selectedVolume;
            int myCount = Volumes.Count();
            string SomeText = String.Empty;
        }

        public ICommand AddIssuesCommand
        {
            get { return new DelegateCommand(AddIssues); }
        }

        private void AddIssues()
        {
            MessageBox.Show("Add Issues");
            int p = selectedIssue;
            int myCount = Issues.Count();
            string SomeText = String.Empty;
        }

        public ICommand FindIssuesCommand
        {
            get { return new DelegateCommand(FindIssues); }
        }

        private void FindIssues()
        {
            MessageBox.Show("Find Issues");
            int p = selectedIssue;
            int myCount = Issues.Count();
            string SomeText = String.Empty;
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
            MessageBox.Show("Export MissingIssues");
            int p = selectedMissingIssue;
            int myCount = MissingIssues.Count();
            string SomeText = String.Empty;
        }
    }
}
