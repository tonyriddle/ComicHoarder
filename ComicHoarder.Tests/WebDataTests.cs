using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using ComicHoarder.Common;
using ComicHoarder.WebData;

namespace ComicHoarder.Tests
{
    [TestClass]
    public class WebDataTests
    {
        string TestXMLComicVinePublisherUnFilteredFileName = @"../../../Documentation/PublisherUnfiltered.xml";
        string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
        string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";
        string key = "12345678";

        [TestMethod]
        public void CanConvertComicVineXmlToComicVinePublisher()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            WebData.CVQueryPublisher.response response = converter.ConvertToPublisherResponse(result);
            Assert.IsTrue(response.results[0].id == "125");
        }

        [TestMethod]
        public void CanConvertComicVineXmlToPublisher()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            Publisher publisher = converter.ConvertToPublisher(result);
            Assert.IsTrue(publisher.id == 125);
            Assert.IsTrue(publisher.name == "Charlton");
        }

        [TestMethod]
        public void CanConvertComicVineXmlToListofVolumes()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            List<Volume> volumes = converter.ConvertToVolumes(result);
            Assert.IsTrue(volumes[0].id == 1533);
            Assert.IsTrue(volumes.Count == 335);
        }

        [TestMethod]
        public void CanBuildPublisherByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.PublisherById(31);
            Assert.IsTrue(query == "http://www.comicvine.com/api/publisher/4010-31/?api_key=12345678&format=xml");
        }

        [TestMethod]
        public void CanBuildVolumesFromPublisherQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.VolumesFromPublisher(31);
            Assert.IsTrue(query == "http://www.comicvine.com/api/publisher/4010-31/?api_key=12345678&format=xml");
        }
        [TestMethod]
        public void CanBuildVolumeByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.VolumeById(1234);
            Assert.IsTrue(query == "http://www.comicvine.com/api/volume/4050-1234/?api_key=12345678&format=xml&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year");
        }
        [TestMethod]
        public void CanBuildIssuesFromVolumeQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.IssuesFromVolume(1234);
            Assert.IsTrue(query == "http://www.comicvine.com/api/volume/4050-1234/?api_key=12345678&format=xml&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,issues");
        }

        [TestMethod]
        public void CanBuildIssueByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.IssueById(1234);
            Assert.IsTrue(query == "http://www.comicvine.com/api/issue/4000-1234/?api_key=12345678&format=xml&field_list=id,volumes,name,issue_number,cover_date");
        }

        [TestMethod]
        public void CanGetPublisherFromWebDataService()
        {
            IConnection connection = new PublisherConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            Publisher publisher = service.GetPublisher(31);
            Assert.IsTrue(publisher.id == 125); //remember, we are using the dummy publisher xml file from above
        }

        [TestMethod]
        public void CanGetVolumesFromPublisherFromWebDataService()
        {
            IConnection connection = new PublisherConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            List<Volume> volumes = service.GetVolumesFromPublisher(31);
            Assert.IsTrue(volumes[0].id == 1533);
            Assert.IsTrue(volumes.Count == 335);
        }

        [TestMethod]
        public void CanConvertComicVineXmlToComicVineVolume()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVineVolumeUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            WebData.CVQueryVolume.response response = converter.ConvertToVolumeResponse(result);
            Assert.IsTrue(response.results[0].id == "2189");
        }

        [TestMethod]
        public void CanConvertComicVineXmlToVolume()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVineVolumeUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            Volume volume = converter.ConvertToVolume(result);
            Assert.IsTrue(volume.id == 2189);
            Assert.IsTrue(volume.name == "The Amazing Spider-Man Annual");
            Assert.IsTrue(volume.countOfIssues == 39);
            Assert.IsFalse(volume.complete);
        }

        [TestMethod]
        public void CanGetVolumeFromWebDataService()
        {
            IConnection connection = new VolumeConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            Volume volume = service.GetVolume(31);
            Assert.IsTrue(volume.id == 2189); //remember, we are using the dummy publisher xml file from above
        }

        [TestMethod]
        public void CanConvertComicVineXmlToListofIssues()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVineVolumeUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            List<Issue> issues = converter.ConvertToIssues(result);
            Assert.IsTrue(issues[0].id == 337485);
            Assert.IsTrue(issues.Count == 39);
            Assert.IsTrue(issues[0].name == "Spider Who?");
            Assert.IsTrue(issues[0].issueNumber == 39.0);
        }

        [TestMethod]
        public void CanGetIssuesFromVolumeFromWebDataService()
        {
            IConnection connection = new VolumeConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            List<Issue> issues = service.GetIssuesFromVolume(31);
            Assert.IsTrue(issues[0].id == 337485);
            Assert.IsTrue(issues.Count == 39);
            Assert.IsTrue(issues[0].name == "Spider Who?");
            Assert.IsTrue(issues[0].issueNumber == 39.0);
        }

        [TestMethod]
        public void CanConvertComicVineXmlToComicVineIssue()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVineIssueUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            WebData.CVQueryIssue.response response = converter.ConvertToIssueResponse(result);
            Assert.IsTrue(response.results[0].id == "187507");
        }

        [TestMethod]
        public void CanConvertComicVineXmlToIssue()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVineIssueUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            Issue issue = converter.ConvertToIssue(result);
            Assert.IsTrue(issue.id == 187507);
            Assert.IsTrue(issue.name == " ");
            Assert.IsTrue(issue.publishYear == 0);
            Assert.IsTrue(issue.publishMonth == 0);
            Assert.IsTrue(issue.issueNumber == 4.00);

        }

        [TestMethod]
        public void CanGetIssueFromWebDataService()
        {
            IConnection connection = new IssueConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            Issue issue = service.GetIssue(31);
            Assert.IsTrue(issue.id == 187507);
            Assert.IsTrue(issue.name == " ");
            Assert.IsTrue(issue.publishYear == 0);
            Assert.IsTrue(issue.publishMonth == 0);
            Assert.IsTrue(issue.issueNumber == 4.00);
        }

        [TestMethod]
        public void CanConvertSearchPublisherToPublisher()
        {
            IConnection connection = new PublisherSearchConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("1234");
            WebDataService service = new WebDataService(connection, converter, builder);
            List<Publisher> publishers = service.SearchPublishers("Marvel");
            Assert.IsTrue(publishers[0].id == 31); //remember, we are using the dummy publisher xml file from above
        }
    }

    public class PublisherConnection : IConnection
    {
        public string Query(string Url)
        {
            string TestXMLComicVinePublisherUnFilteredFileName = @"../../../Documentation/PublisherUnfiltered.xml";
            string result = "";

            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }

    public class PublisherSearchConnection : IConnection
    {
        public string Query(string Url)
        {
            string TestXMLComicVinePublisherUnFilteredFileName = @"../../../Documentation/publishersearch.xml";
            string result = "";

            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }

    public class VolumeConnection : IConnection
    {
        public string Query(string Url)
        {
            string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
            string result = "";

            using (StreamReader sr = new StreamReader(TestXMLComicVineVolumeUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }

    public class IssueConnection : IConnection
    {
        public string Query(string Url)
        {
            string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";
            string result = "";

            using (StreamReader sr = new StreamReader(TestXMLComicVineIssueUnFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }

}
