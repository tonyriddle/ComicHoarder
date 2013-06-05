using ComicHoarder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public interface IRepository
    {
        //publishers
        List<Publisher> GetAllPublishers();
        Publisher GetPublisherById(int publisherId);
        bool SavePublisher(Publisher publisher);
        bool PublisherExists(int publisherId);
        //volumes
        List<Volume> GetAllVolumes();
        List<Volume> GetVolumeByPublisherId(int publisherId);
        Volume GetVolumeById(int volumeId);
        bool SaveVolume(Volume volume);
        bool VolumeExists(int volumeId);
        //issues
        List<Issue> GetAllIssues();
        List<Issue> GetIssueByVolumeId(int volumeId);
        Issue GetIssueById(int issueId);
        bool SaveIssue(Issue issue);
        bool IssueExists(int issueId);
    }
}
