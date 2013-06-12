using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.Repository
{
    public class RepositoryService : IRepository
    {
        IRepository repository;

        public RepositoryService()
        {
            repository = new MSSQLDatabase();
        }

        public RepositoryService(IRepository repository)
        {
            this.repository = repository;
        }
        
        public Issue GetIssue(int id)
        {
            return repository.GetIssue(id);
        }

        public Publisher GetPublisher(int id)
        {
            return repository.GetPublisher(id);
        }

        public Volume GetVolume(int id)
        {
            return repository.GetVolume(id);
        }

        public bool DeleteIssue(int id)
        {
            return repository.DeleteIssue(id);
        }

        public bool DeletePublisher(int id)
        {
            return repository.DeletePublisher(id);
        }

        public bool DeleteVolume(int id)
        {
            return repository.DeleteVolume(id);
        }

        public bool Save(Issue issue)
        {
            return repository.Save(issue);
        }

        public bool Save(Publisher publisher)
        {
            return repository.Save(publisher);
        }

        public bool Save(Volume volume)
        {
            return repository.Save(volume);
        }

        public bool Save(List<Volume> volumes)
        {
            return repository.Save(volumes);
        }

        public bool Save(List<Issue> issues)
        {
            return repository.Save(issues);
        }

        public bool Save(List<Publisher> publishers)
        {
            return repository.Save(publishers);
        }

        public List<Publisher> GetPublishers()
        {
            return repository.GetPublishers();
        }

        public List<Volume> GetVolumes(int id)
        {
            return repository.GetVolumes(id);
        }

        public List<Issue> GetIssues(int id)
        {
            return repository.GetIssues(id);
        }

        public List<Issue> GetIssuesByPublisher(int id)
        {
            return repository.GetIssuesByPublisher(id);
        }

        public List<MissingIssue> GetMissingIssues(int id)
        {
            return repository.GetMissingIssues(id);
        }

        public bool UpdateVolumeToUncollectable(int id)
        {
            return repository.UpdateVolumeToUncollectable(id);
        }

        public bool PublisherExists(int id)
        {
            return repository.PublisherExists(id);
        }

        public bool VolumeExists(int id)
        {
            return repository.VolumeExists(id);
        }

        public bool IssueExists(int id)
        {
            return repository.IssueExists(id);
        }

        public bool UpdateIssueToCollected(int id)
        {
            return repository.UpdateIssueToCollected(id);
        }

        public string GetWebServiceKey(string name)
        {
            return repository.GetWebServiceKey(name);
        }

        public PieChartMissingIssueRatio GetPieChartData(int publisherId)
        {
            return repository.GetPieChartData(publisherId);
        }
    }
}
