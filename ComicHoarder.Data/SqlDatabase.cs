using ComicHoarder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Repository
{
    public class SqlDatabase : IRepository
    {
        public List<Publisher> GetAllPublishers()
        {
            throw new NotImplementedException();
        }

        public Publisher GetPublisherById(int publisherId)
        {
            throw new NotImplementedException();
        }

        public bool SavePublisher(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public List<Volume> GetAllVolumes()
        {
            throw new NotImplementedException();
        }

        public List<Volume> GetVolumeByPublisherId(int publisherId)
        {
            throw new NotImplementedException();
        }

        public Volume GetVolumeById(int volumeId)
        {
            throw new NotImplementedException();
        }

        public bool SaveVolume(Volume volume)
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetAllIssues()
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssueByVolumeId(int volumeId)
        {
            throw new NotImplementedException();
        }

        public Issue GetIssueById(int issueId)
        {
            throw new NotImplementedException();
        }

        public bool SaveIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public bool PublisherExists(int publisherId)
        {
            throw new NotImplementedException();
        }

        public bool VolumeExists(int volumeId)
        {
            throw new NotImplementedException();
        }

        public bool IssueExists(int issueId)
        {
            throw new NotImplementedException();
        }
    }
}
