using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.Common
{
    public interface IWebDataService
    {
        //publisher
        Publisher GetPublisher(int publisherId);
        //volume
        List<Volume> GetVolumesFromPublisher(int publisherId);
        Volume GetVolume(int volumeId);
        //issue
        List<Issue> GetIssuesFromVolume(int volumeId);
        Issue GetIssue(int issueId);
        //search
        List<Publisher> SearchPublishers(string PartialPublisherName);
        List<Volume> SearchVolumes(string PartialVolumeName);
        List<Issue> SearchIssues(string PartialIssueName);
    }
}
