using System;
using System.Collections.Generic;
namespace ComicHoarder.Common
{
    public interface IWebDataReader
    {
        Issue GetIssue(string IssueRequestResponse);
        List<Issue> GetIssues(string VolumeRequestResponse);
        Publisher GetPublisher(string PublisherRequestResponse);
        List<Publisher> GetPublishers(string PublisherQueryResponse);
        Volume GetVolume(string VolumeRequestResponse);
        List<Volume> GetVolumes(string PublisherRequestResponse);
    }
}
