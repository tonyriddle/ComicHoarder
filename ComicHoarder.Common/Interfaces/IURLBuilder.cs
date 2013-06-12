using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public interface IURLBuilder
    {
        string PublisherById(int id);
        string VolumesFromPublisher(int id);
        string VolumeById(int id);
        string IssuesFromVolume(int id);
        string IssueById(int id);
        string SearchPublishers(string name);
        string SearchVolumes(string name);
        string SearchIssues(string name);
    }
}
