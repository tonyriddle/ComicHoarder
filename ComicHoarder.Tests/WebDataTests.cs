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
        string testMode = "Test";
        //string testMode = "Live";

        IWebDataConverter converter;
        IURLBuilder urlBuilder;
        IWebConnection connection;
        IWebDataService service;

        IRepository repository;

        string key;

        [TestInitialize]
        public void Setup()
        {
            if (testMode == "Live")
            {
                repository = new MSSQLDatabase();
                connection = new ComicVineWebConnection();
            }
            else
            {
                repository = new TestRepository();
                connection = new TestWebConnection();
            }

            key = repository.GetSetting("WebServiceKey");

            converter = new ComicVineConverter();
            urlBuilder = new ComicVineURLBuilder(key);
            service = new WebDataService(connection, converter, urlBuilder);
        }

        string TestXMLComicVinePublisherFilteredFileName = @"../../../Documentation/Publisherfiltered.xml";
        string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
        string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";

        [TestMethod]
        public void CanConvertComicVineXmlToComicVinePublisher()
        {
            string result = "";
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherFilteredFileName))
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
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherFilteredFileName))
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
            using (StreamReader sr = new StreamReader(TestXMLComicVinePublisherFilteredFileName))
            {
                result = sr.ReadToEnd();
            }
            ComicVineConverter converter = new ComicVineConverter();
            List<Volume> volumes = converter.ConvertToVolumes(result);
            Assert.IsTrue(volumes[0].id == 1533);
            Assert.IsTrue(volumes.Count == 336);
        }

        [TestMethod]
        public void CanBuildPublisherByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.PublisherById(31);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/publisher/4010-31/?api_key=") && query.Contains("&field_list=volumes,deck,id,name&format=xml"));
        }

        [TestMethod]
        public void CanBuildVolumesFromPublisherQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.VolumesFromPublisher(31);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/publisher/4010-31/?api_key=") && query.Contains("&field_list=volumes,deck,id,name&format=xml"));
        }
        [TestMethod]
        public void CanBuildVolumeByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.VolumeById(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/volume/4050-1234/?api_key=") && query.Contains("&format=xml&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,description"));
        }
        [TestMethod]
        public void CanBuildIssuesFromVolumeQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.IssuesFromVolume(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/volume/4050-1234/?api_key=") && query.Contains("&format=xml&field_list=id,volume,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,issues"));
        }

        [TestMethod]
        public void CanBuildIssueByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key);
            string query = URLBuilder.IssueById(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/issue/4000-1234/?api_key=") && query.Contains("&format=xml&field_list=id,cover_date,volume,name,issue_number"));
        }

        [TestMethod]
        public void CanGetPublisherFromWebDataService()
        {
            Publisher publisher = service.GetPublisher(125);
            Assert.IsTrue(publisher.id == 125); 
        }

        [TestMethod]
        public void CanGetVolumesFromPublisherFromWebDataService()
        {
            List<Volume> volumes = service.GetVolumesFromPublisher(125);
            Assert.IsTrue(volumes[0].id == 1533);
            Assert.IsTrue(volumes.Count == 336);
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
            Volume volume = service.GetVolume(2189);
            Assert.IsTrue(volume.id == 2189); 
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
            Assert.IsTrue(issues[0].id == 6837);
            Assert.IsTrue(issues.Count == 33);
            Assert.IsTrue(issues[0].name == "The Sinister Six!");
            Assert.IsTrue(issues[0].issueNumber == 1.0);
        }

        [TestMethod]
        public void CanGetIssuesFromVolumeFromWebDataService()
        {
            List<Issue> issues = service.GetIssuesFromVolume(2189);
            Assert.IsTrue(issues[0].id == 6837);
            Assert.IsTrue(issues.Count == 33);
            Assert.IsTrue(issues[0].name == "The Sinister Six!");
            Assert.IsTrue(issues[0].issueNumber == 1.0);
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
            Assert.IsTrue(issue.name == "");
            Assert.IsTrue(issue.publishYear == 2007);
            Assert.IsTrue(issue.publishMonth == 6);
            Assert.IsTrue(issue.issueNumber == 4.00);

        }

        [TestMethod]
        public void CanGetIssueFromWebDataService()
        {
            Issue issue = service.GetIssue(187507);
            Assert.IsTrue(issue.id == 187507);
            Assert.IsTrue(issue.name == "");
            Assert.IsTrue(issue.publishYear == 2007);
            Assert.IsTrue(issue.publishMonth == 6);
            Assert.IsTrue(issue.issueNumber == 4.00);
        }

        [TestMethod]
        public void CanConvertSearchPublisherToPublisher()
        {
            List<Publisher> publishers = service.SearchPublishers("Marvel");
            Assert.IsTrue(publishers[0].id == 31); 
        }

        [TestMethod]
        public void CanDetectReprint()
        {
            IComicVineReprintDetector reprintDetector = new ComicVineReprintDetector();
            Volume volume = new Volume();
            volume.collectable = true;
            volume.description = "reprints spider-man 1 to 650";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
            volume.collectable = true;
            volume.description = "this is a tradepaperback";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
            volume.collectable = true;
            volume.description = "tpb of spider-man 1 to 650";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
            volume.collectable = true;
            volume.description = "a hardcover book which reprints spider-man 1 to 650";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
            volume.collectable = true;
            volume.description = "spider-man trade paperback of 1 to 650";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
            volume.collectable = true;
            volume.description = "reprints spider-man 1 to 650";
            Assert.IsTrue(reprintDetector.DetectReprint(volume));
            Assert.IsFalse(volume.collectable);
        }

        [TestMethod]
        public void CanDetectIfNotReprint()
        {
            IComicVineReprintDetector reprintDetector = new ComicVineReprintDetector();
            Volume volume = new Volume();
            volume.collectable = true;
            volume.description = "this is the amazing spider-man";
            Assert.IsFalse(reprintDetector.DetectReprint(volume));
            Assert.IsTrue(volume.collectable);
        }
    }
}
