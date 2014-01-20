using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.WebDataProvider
{
    public class ComicVineJSONReader : IWebDataReader
    {
        public Issue GetIssue(string ComicVineIssueRequestJSONResponse)
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssues(string ComicVineVolumeRequestJSONResponse)
        {
            throw new NotImplementedException();
        }

        public Publisher GetPublisher(string ComicVinePublisherRequestJSONResponse)
        {
            throw new NotImplementedException();
        }

        public List<Publisher> GetPublishers(string ComicVinePublisherQueryJSONResult)
        {
            throw new NotImplementedException();
        }

        public Volume GetVolume(string ComicVineVolumeRequestJSONResponse)
        {
            throw new NotImplementedException();
        }

        public List<Volume> GetVolumes(string ComicVinePublisherRequestJSONResponse)
        {
            throw new NotImplementedException();
        }
    }
}
