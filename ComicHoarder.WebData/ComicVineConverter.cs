using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using ComicHoarder.Common;

namespace ComicHoarder.WebData
{
    public class ComicVineConverter : IWebDataConverter
    {
        IComicVineReprintDetector reprintDetector;

        public ComicVineConverter()
        {
            reprintDetector = new ComicVineReprintDetector();
        }

        public ComicVineConverter(IComicVineReprintDetector reprintDetector)
        {
            this.reprintDetector = reprintDetector;
        }

        public Issue ConvertToIssue(string xml)
        {
            Issue issue = new Issue();
            CVQueryIssue.response response = ConvertToIssueResponse(xml);
            issue.id = ParseHelper.ParseInt(response.results[0].id);
            issue.name = response.results[0].name;
            issue.volumeId = ParseHelper.ParseInt(response.results[0].volume[0].id);
            if (response.results[0].issue_number.Contains("au"))
            {
                int n = 0;
                int.TryParse(new string(response.results[0].issue_number.Where(a => Char.IsDigit(a)).ToArray()), out n);
                issue.issueNumber = ParseHelper.ParseFloat(n.ToString());
                issue.issueNumber = issue.issueNumber + .1f;
            }
            DateTime publishDate = new DateTime();
            DateTime.TryParse(response.results[0].cover_date, out publishDate);
            issue.publishMonth = publishDate.Month;
            issue.publishYear = publishDate.Year;
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
            issue.issueNumber = ParseHelper.ParseFloat(response.results[0].issue_number);
            issue.summary = response.results[0].deck;
            return issue;
        }

        public Volume ConvertToVolume(string xml)
        {
            Volume volume = new Volume();
            CVQueryVolume.response response = ConvertToVolumeResponse(xml);
            volume.id = ParseHelper.ParseInt(response.results[0].id);
            volume.publisherId = ParseHelper.ParseInt(response.results[0].publisher[0].id);
            volume.name = response.results[0].name;
            volume.description = response.results[0].deck;
            volume.dateAdded = ParseHelper.ParseDateTime(response.results[0].date_added);
            volume.dateLastUpdated = ParseHelper.ParseDateTime(response.results[0].date_last_updated);
            volume.collectable = true;
            volume.countOfIssues = ParseHelper.ParseInt(response.results[0].count_of_issues);
            volume.startYear = ParseHelper.ParseInt(response.results[0].start_year);
            volume.enabled = true;
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

        public Publisher ConvertToPublisher(string xml)
        {
            Publisher publisher = new Publisher();
            CVQueryPublisher.response response = ConvertToPublisherResponse(xml);
            publisher.id = ParseHelper.ParseInt(response.results[0].id);
            publisher.name = response.results[0].name;
            publisher.description = response.results[0].deck;
            publisher.enabled = true;
            publisher.dateLastUpdated = ParseHelper.ParseDateTime(response.results[0].date_last_updated);
            return publisher;
        }

        public List<Publisher> ConvertToPublishers(string xml)
        {
            List<Publisher> publishers = new List<Publisher>();
            CVSearchPublisher.response response = ConvertToPublisherSearchResponse(xml);
            foreach (ComicHoarder.WebData.CVSearchPublisher.responseResultsPublisher comicvinepublisher in response.results[0].publisher)
            {
                Publisher publisher = new Publisher();
                publisher.id = ParseHelper.ParseInt(comicvinepublisher.id);
                publisher.name = comicvinepublisher.name;
                publisher.description = comicvinepublisher.deck;
                publisher.enabled = true;
                publisher.dateLastUpdated = ParseHelper.ParseDateTime(comicvinepublisher.date_last_updated);
                publishers.Add(publisher);
            }
            return publishers;
        }

        public List<Volume> ConvertToVolumes(string xml)
        {
            CVQueryPublisher.response response = ConvertToPublisherResponse(xml);
            List<Volume> volumes = new List<Volume>();
            foreach (ComicHoarder.WebData.CVQueryPublisher.responseResultsVolumesVolume comicvinevolume in response.results[0].volumes[0].volume)
            {
                Volume volume = new Volume();
                volume.id = ParseHelper.ParseInt(comicvinevolume.id);
                volume.name = comicvinevolume.name;
                volumes.Add(volume);
            }
            return volumes;
        }

        public List<Issue> ConvertToIssues(string xml)
        {
            CVQueryVolume.response response = ConvertToVolumeResponse(xml);
            List<Issue> issues = new List<Issue>();
            foreach (ComicHoarder.WebData.CVQueryVolume.responseResultsIssuesIssue comicvineissue in response.results[0].issues[0].issue)
            {
                Issue issue = new Issue();
                issue.id = ParseHelper.ParseInt(comicvineissue.id);
                issue.name = comicvineissue.name;
                if (comicvineissue.issue_number.Contains("au"))
                {
                    int n = 0;
                    int.TryParse(new string(comicvineissue.issue_number.Where(a => Char.IsDigit(a)).ToArray()), out n);
                    issue.issueNumber = ParseHelper.ParseFloat(n.ToString());
                    issue.issueNumber = issue.issueNumber + .1f;
                }

                issue.issueNumber = ParseHelper.ParseFloat(comicvineissue.issue_number);
                issues.Add(issue);
            }
            return issues;
        }

        public CVQueryPublisher.response ConvertToPublisherResponse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CVQueryPublisher.response));
            TextReader reader = new StringReader(xml);
            return (CVQueryPublisher.response)serializer.Deserialize(reader);
        }

        public CVQueryVolume.response ConvertToVolumeResponse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CVQueryVolume.response));
            TextReader reader = new StringReader(xml);
            return (CVQueryVolume.response)serializer.Deserialize(reader);

        }

        public CVQueryIssue.response ConvertToIssueResponse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CVQueryIssue.response));
            TextReader reader = new StringReader(xml);
            return (CVQueryIssue.response)serializer.Deserialize(reader);
        }

        public CVSearchPublisher.response ConvertToPublisherSearchResponse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CVSearchPublisher.response));
            TextReader reader = new StringReader(xml);
            return (CVSearchPublisher.response)serializer.Deserialize(reader);
        }
    }
}
