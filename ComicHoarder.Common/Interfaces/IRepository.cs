using ComicHoarder.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace ComicHoarder.Common
{
    public interface IRepository
    {
        Issue GetIssue(int id);
        Publisher GetPublisher(int id);
        Volume GetVolume(int id);
        bool DeleteIssue(int id);
        bool DeletePublisher(int id);
        bool DeleteVolume(int id);
        bool Save(Issue issue);
        bool Save(Publisher publisher);
        bool Save(Volume volume);
        bool Save(List<Volume> volumes);
        bool Save(List<Issue> issues);
        bool Save(List<Publisher> publishers);
        List<Publisher> GetPublishers();
        List<Volume> GetVolumes(int id);
        List<Issue> GetIssues(int id);
        List<Issue> GetIssuesByPublisher(int id);
        List<MissingIssue> GetMissingIssues(int id);
        bool UpdateVolumeToUncollectable(int id);
        bool PublisherExists(int id);
        bool VolumeExists(int id);
        bool IssueExists(int id);
        bool UpdateIssueToCollected(int id);
    }
}
