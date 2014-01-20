using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.WebDataProvider
{
    public class WebDataService : IWebDataService
    {
        IWebConnection connection;
        IWebDataReader reader;
        IURLBuilder urlBuilder;
        string format = "xml";

        public WebDataService(IWebConnection connection, IWebDataReader reader, IURLBuilder urlBuilder)
        {
            this.connection = connection;
            this.reader = reader;
            this.urlBuilder = urlBuilder;
        }

        public WebDataService(string key)
        {
            //default to ComicVine
            this.connection = new ComicVineWebConnection();
            this.reader = new ComicVineXMLReader();
            this.urlBuilder = new ComicVineURLBuilder(key, format);
        }

        public Publisher GetPublisher(int publisherId)
        {
            string url = urlBuilder.PublisherById(publisherId);
            string PublisherRequestResponse = connection.Query(url);
            return reader.GetPublisher(PublisherRequestResponse);
        }

        public List<Volume> GetVolumesFromPublisher(int publisherId)
        {
            string url = urlBuilder.VolumesFromPublisher(publisherId);
            string PublisherRequestResponse = connection.Query(url);
            return reader.GetVolumes(PublisherRequestResponse);
        }

        public Volume GetVolume(int volumeId)
        {
            string url = urlBuilder.VolumeById(volumeId);
            string VolumeRequestResponse = connection.Query(url);
            return reader.GetVolume(VolumeRequestResponse);
        }

        public List<Issue> GetIssuesFromVolume(int volumeId)
        {
            string url = urlBuilder.IssuesFromVolume(volumeId);
            string IssuesByVolumeRequestResponse = connection.Query(url);
            return reader.GetIssues(IssuesByVolumeRequestResponse);
        }

        public Issue GetIssue(int issueId)
        {
            string url = urlBuilder.IssueById(issueId);
            string IssueRequestResponse = connection.Query(url);
            return reader.GetIssue(IssueRequestResponse);
        }

        public List<Publisher> SearchPublishers(string PartialPublisherName)
        {
            string url = urlBuilder.SearchPublishers(PartialPublisherName);
            string PublisherQueryResponse = connection.Query(url);
            return reader.GetPublishers(PublisherQueryResponse);
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
