using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public interface IWebDataConverter
    {
        Issue ConvertToIssue(string xml);
        Volume ConvertToVolume(string xml);
        Publisher ConvertToPublisher(string xml);
        List<Volume> ConvertToVolumes(string xml);
        List<Issue> ConvertToIssues(string xml);
        List<Publisher> ConvertToPublishers(string xml);
    }
}
