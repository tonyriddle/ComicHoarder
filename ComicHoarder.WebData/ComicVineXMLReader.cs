using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ComicHoarder.WebDataProvider
{
    public class ComicVineXMLReader : IWebDataReader
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

        public Issue GetIssue(string ComicVineIssueRequestXMLResponse)
        {
            var issue = new Issue();
            var xDoc = XDocument.Parse(ComicVineIssueRequestXMLResponse);
            issue.id = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");
            var strIssueNumber = (string)xDoc.Descendants("issue_number").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault();
            issue.issueNumber = ParseHelper.ParseFloat(new String(strIssueNumber.Where(Char.IsNumber).ToArray()) ?? "");
            issue.issueNumberSuffix = new String(strIssueNumber.Where(Char.IsLetter).ToArray()) ?? "";
            issue.name = (string)xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "";
            issue.volumeId = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "volume").FirstOrDefault() ?? "0");
            issue.summary = (string)xDoc.Descendants("deck").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0";
            var PublishDate = ParseHelper.ParseDateTime((string)xDoc.Descendants("cover_date").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "");
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

        public Volume GetVolume(string ComicVineVolumeRequestXMLResponse)
        {
            var volume = new Volume();
            var xDoc = XDocument.Parse(ComicVineVolumeRequestXMLResponse);
            volume.id = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");
            volume.name = (string)xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "";
            volume.description = (string)xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "";
            volume.dateAdded = ParseHelper.ParseDateTime((string)xDoc.Descendants("date_added").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "");
            volume.dateLastUpdated = ParseHelper.ParseDateTime((string)xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "");
            volume.countOfIssues = ParseHelper.ParseInt((string)xDoc.Descendants("count_of_issues").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");
            volume.startYear = ParseHelper.ParseInt((string)xDoc.Descendants("start_year").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");
            volume.publisherId = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "publisher").FirstOrDefault() ?? "0");
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

        public Publisher GetPublisher(string ComicVinePublisherRequestXMLResponse)
        {
            var publisher = new Publisher();
            var xDoc = XDocument.Parse(ComicVinePublisherRequestXMLResponse);
            publisher.id = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");
            publisher.name = (string)xDoc.Descendants("name").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "";
            publisher.description = (string)xDoc.Descendants("description").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "";
            publisher.dateLastUpdated = ParseHelper.ParseDateTime((string)xDoc.Descendants("date_last_updated").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "");
            publisher.enabled = true;
            return publisher;
        }

        public List<Issue> GetIssues(string ComicVineVolumeRequestXMLResponse)
        {
            var xDoc = XDocument.Parse(ComicVineVolumeRequestXMLResponse);
            var xVolumeId = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");

            var issues = (from issue in xDoc.Descendants("issue").Where(p => p.Parent.Name.LocalName == "issues")
                select new Issue
                {
                    name = (string)issue.Descendants("name").FirstOrDefault() ?? "",
                    id = ParseHelper.ParseInt((string)issue.Descendants("id").FirstOrDefault() ?? "0"),
                    issueNumber = ParseHelper.ParseFloat((new String(issue.Descendants("issue_number").FirstOrDefault().ToString().Where(Char.IsNumber).ToArray())) ?? ""),
                    issueNumberSuffix = new String(((string)issue.Descendants("issue_number").FirstOrDefault()).Where(Char.IsLetter).ToArray()) ?? "",
                    volumeId = xVolumeId
                }).ToList();

            return issues;
        }

        public List<Volume> GetVolumes(string ComicVinePublisherRequestXMLResponse)
        {
            var xDoc = XDocument.Parse(ComicVinePublisherRequestXMLResponse);
            var xPublisherId = ParseHelper.ParseInt((string)xDoc.Descendants("id").Where(p => p.Parent.Name.LocalName == "results").FirstOrDefault() ?? "0");

            var volumes = (from volume in xDoc.Descendants("volume").Where(p => p.Parent.Name.LocalName == "volumes")
                          select new Volume
                          {
                              name = (string)volume.Descendants("name").FirstOrDefault() ?? "",
                              id = ParseHelper.ParseInt((string)volume.Descendants("id").FirstOrDefault() ?? "0"),
                              publisherId = xPublisherId
                          }).ToList();
            return volumes;
        }

        public List<Publisher> GetPublishers(string ComicVinePublisherQueryXMLResponse)
        {
            var xDoc = XDocument.Parse(ComicVinePublisherQueryXMLResponse);

            var publishers = (from publisher in xDoc.Descendants("publisher").Where(p => p.Parent.Name.LocalName == "results")
                              select new Publisher
                              {
                                  name = (string)publisher.Descendants("name").FirstOrDefault() ?? "",
                                  id = ParseHelper.ParseInt((string)publisher.Descendants("id").FirstOrDefault() ?? "0")
                              }).ToList();
            return publishers;
        }
    }
}
