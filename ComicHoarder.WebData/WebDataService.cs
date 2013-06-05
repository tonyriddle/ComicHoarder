using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.WebData
{
    public class WebDataService : IWebDataService
    {
        IConnection connection;
        IWebDataConverter converter;
        IURLBuilder urlBuilder;

        public WebDataService(IConnection connection, IWebDataConverter converter, IURLBuilder urlBuilder)
        {
            this.connection = connection;
            this.converter = converter;
            this.urlBuilder = urlBuilder;
        }

        public WebDataService(string key)
        {
            this.connection = new ComicVineConnection();
            this.converter = new ComicVineConverter();
            this.urlBuilder = new ComicVineURLBuilder(key);
        }

        public List<Publisher> GetAllPublishers()
        {
            throw new NotImplementedException();
        }

        public Publisher GetPublisher(int publisherId)
        {
            string url = urlBuilder.PublisherById(publisherId);
            string result = connection.Query(url);
            return converter.ConvertToPublisher(result);
        }

        public List<Volume> GetVolumesFromPublisher(int publisherId)
        {
            string url = urlBuilder.VolumesFromPublisher(publisherId);
            string result = connection.Query(url);
            return converter.ConvertToVolumes(result);
        }

        public List<Volume> GetNewVolumes(int publisherId, List<int> existingVolumeIds)
        {
            throw new NotImplementedException();
        }

        public Volume GetVolume(int volumeId)
        {
            string url = urlBuilder.VolumeById(volumeId);
            string result = connection.Query(url);
            return converter.ConvertToVolume(result);
        }

        public List<Issue> GetIssuesFromVolume(int volumeId)
        {
            string url = urlBuilder.IssuesFromVolume(volumeId);
            string result = connection.Query(url);
            return converter.ConvertToIssues(result);
        }

        public List<Issue> GetNewIssues(int volumeId, List<int> existingIssueIds)
        {
            throw new NotImplementedException();
        }

        public Issue GetIssue(int issueId)
        {
            string url = urlBuilder.IssueById(issueId);
            string result = connection.Query(url);
            return converter.ConvertToIssue(result);
        }

        public List<Publisher> SearchPublishers(string PartialPublisherName)
        {
            string url = urlBuilder.SearchPublishers(PartialPublisherName);
            string result = connection.Query(url);
            return converter.ConvertToPublishers(result);
        }

        public List<Volume> SearchVolumes(string PartialVolumeName)
        {
            throw new NotImplementedException();
        }

        public List<Issue> SearchIssues(string PartialIssueName)
        {
            throw new NotImplementedException();
        }
    }
}
