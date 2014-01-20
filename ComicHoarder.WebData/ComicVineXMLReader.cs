using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using System.Xml.Linq;

namespace ComicHoarder.WebDataProvider
{
    public class ComicVineXMLReader
    {
        IComicVineReprintDetector reprintDetector;

        public ComicVineXMLReader()
        {
            reprintDetector = new ComicVineReprintDetector();
        }

        public ComicVineXMLReader(IComicVineReprintDetector reprintDetector)
        {
            this.reprintDetector = reprintDetector;
        }

        public Issue GetIssue(string xml)
        {
            var issue = new Issue();
            var xDoc = XDocument.Parse(xml);
            issue.id = ParseHelper.ParseInt(xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            issue.issueNumber = ParseHelper.ParseFloat(xDoc.Descendants("issue_number").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("issue_number").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            issue.name = xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            issue.volumeId = ParseHelper.ParseInt(xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "volume").FirstOrDefault() == null ? "" : xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "volume").FirstOrDefault().Value);
            issue.summary = xDoc.Descendants("deck").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("deck").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            DateTime PublishDate = ParseHelper.ParseDateTime(xDoc.Descendants("cover_date").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("cover_date").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            issue.publishMonth = PublishDate.Month;
            issue.publishYear = PublishDate.Year;
            if (issue.publishYear == 1)
            {
                issue.publishYear = 0;
                if (issue.publishMonth == 1)
                {
                    issue.publishMonth = 0;
                }
            }
            issue.collected = false;
            issue.enabled = true;
            return issue;
        }

        public Volume GetVolume(string xml)
        {
            var volume = new Volume();
            var xDoc = XDocument.Parse(xml);
            volume.id = ParseHelper.ParseInt(xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            volume.name = xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            volume.description = xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            volume.dateAdded = ParseHelper.ParseDateTime(xDoc.Descendants("date_added").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("date_added").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            volume.dateLastUpdated = ParseHelper.ParseDateTime(xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            volume.countOfIssues = ParseHelper.ParseInt(xDoc.Descendants("count_of_issues").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("count_of_issues").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            volume.startYear = ParseHelper.ParseInt(xDoc.Descendants("start_year").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("start_year").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            volume.publisherId = ParseHelper.ParseInt(xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "publisher").FirstOrDefault() == null ? "0" : xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "publisher").FirstOrDefault().Value);
            volume.collectable = true;

            if (volume.dateLastUpdated > DateTime.Now.AddMonths(-13))
            {
                volume.complete = false;
            }
            else
            {
                volume.complete = true;
            }

            reprintDetector.DetectReprint(volume);
            return volume;
        }

        public Publisher GetPublisher(string xml)
        {
            var publisher = new Publisher();
            var xDoc = XDocument.Parse(xml);
            publisher.id = ParseHelper.ParseInt(xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "0" : xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            publisher.name = xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            publisher.description = xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value;
            publisher.dateLastUpdated = ParseHelper.ParseDateTime(xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() == null ? "" : xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault().Value);
            publisher.enabled = true;
            return publisher;
        }

        public List<Issue> GetIssues(string xml)
        {
            var issues = new List<Issue>();

            return issues;
        }
        
        //List<Volume> ConvertToVolumes(string xml);
        //List<Issue> ConvertToIssues(string xml);
        //List<Publisher> ConvertToPublishers(string xml);

    }
}
