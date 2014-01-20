using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using ComicHoarder.Common;
using ComicHoarder.WebDataProvider;

namespace ComicHoarder.Tests
{
    [TestClass]
    public class WebDataTests
    {
        string testMode = "Test";
        //string testMode = "Live";

        IURLBuilder urlBuilder;
        IWebConnection connection;
        IWebDataService service;
        IWebDataReader reader;

        IRepository repository;

        string key;
        string format;

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
            format = "xml";

            urlBuilder = new ComicVineURLBuilder(key, format);
            reader = new ComicVineXMLReader();
            service = new WebDataService(connection, reader, urlBuilder);
        }

        string TestXMLComicVinePublisherFilteredFileName = @"../../../Documentation/Publisherfiltered.xml";
        string TextXMLComicVinePublisherSearchFileName = @"../../../Documentation/publishersearch.xml";
        string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
        string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";
        string TestXMLComicVineIssueWithSuffixUnFilteredFilename = @"../../../Documentation/issuewithsuffix.xml";

        [TestMethod]
        public void CanBuildComicVineXMLPublisherByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "xml");
            string query = URLBuilder.PublisherById(31);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/publisher/4010-31/?api_key=") && query.Contains("&field_list=volumes,deck,id,name&format=xml"));
        }

        [TestMethod]
        public void CanBuildComicVineJSONPublisherByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "json");
            string query = URLBuilder.PublisherById(31);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/publisher/4010-31/?api_key=") && query.Contains("&field_list=volumes,deck,id,name&format=json"));
        }

        [TestMethod]
        public void CanBuildComicVineXMLVolumesFromPublisherQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "xml");
            string query = URLBuilder.VolumesFromPublisher(31);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/publisher/4010-31/?api_key=") && query.Contains("&field_list=volumes,deck,id,name&format=xml"));
        }

        [TestMethod]
        public void CanBuildComicVineXMLVolumeByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "xml");
            string query = URLBuilder.VolumeById(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/volume/4050-1234/?api_key=") && query.Contains("&format=xml&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,description"));
        }

        [TestMethod]
        public void CanBuildComicVineJSONVolumeByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "json");
            string query = URLBuilder.VolumeById(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/volume/4050-1234/?api_key=") && query.Contains("&format=json&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,description"));
        }

        [TestMethod]
        public void CanBuildComicVineXMLIssuesFromVolumeQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "xml");
            string query = URLBuilder.IssuesFromVolume(1234);
            Assert.IsTrue(query.Contains("http://www.comicvine.com/api/volume/4050-1234/?api_key=") && query.Contains("&format=xml&field_list=id,volume,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,issues"));
        }

        [TestMethod]
        public void CanBuildComicVineXMLIssueByIdQuery()
        {
            ComicVineURLBuilder URLBuilder = new ComicVineURLBuilder(key, "xml");
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
        public void CanGetVolumeFromWebDataService()
        {
            Volume volume = service.GetVolume(2189);
            Assert.IsTrue(volume.id == 2189); 
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
        public void CanReadComicVineXMLToIssue()
        {
            var result = File.ReadAllText(TestXMLComicVineIssueUnFilteredFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var issue = cvXMLReader.GetIssue(result);
            Assert.IsTrue(issue.id == 187507);
            Assert.IsTrue(issue.issueNumber == 4);
            Assert.IsTrue(issue.name == "");
            Assert.IsTrue(issue.volumeId == 30439);
        }

        [TestMethod]
        public void CanReadComicVineXMLToIssueWithSuffix()
        {
            var result = File.ReadAllText(TestXMLComicVineIssueWithSuffixUnFilteredFilename);
            var cvXMLReader = new ComicVineXMLReader();
            var issue = cvXMLReader.GetIssue(result);
            Assert.IsTrue(issue.id == 187507);
            Assert.IsTrue(issue.issueNumber == 4.0f);
            Assert.IsTrue(issue.issueNumberSuffix == "au");
            Assert.IsTrue(issue.name == "");
            Assert.IsTrue(issue.volumeId == 30439);
        }
        
        [TestMethod]
        public void CanReadComicVineXMLToVolume()
        {
            var result = File.ReadAllText(TestXMLComicVineVolumeUnFilteredFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var volume = cvXMLReader.GetVolume(result);
            Assert.AreEqual(volume.id, 2189);
            Assert.AreEqual(volume.name, "The Amazing Spider-Man Annual");
            Assert.AreEqual(volume.countOfIssues, 39);
            Assert.IsFalse(volume.complete);
            Assert.AreEqual(volume.publisherId, 31);
            Assert.AreEqual(volume.startYear, 1964);
        }

        [TestMethod]
        public void CanReadComicVineXMLToPublisher()
        {
            var result = File.ReadAllText(TestXMLComicVinePublisherFilteredFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var publisher = cvXMLReader.GetPublisher(result);
            Assert.AreEqual(publisher.id, 125);
            Assert.AreEqual(publisher.name, "Charlton");
            Assert.IsTrue(publisher.enabled = true);
            Assert.AreEqual(publisher.description, "");
        }

        [TestMethod]
        public void CanReadComicVineXMLToIssues()
        {
            var result = File.ReadAllText(TestXMLComicVineVolumeUnFilteredFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var issues = cvXMLReader.GetIssues(result);
            Assert.IsTrue(issues[0].id == 6837);
            Assert.IsTrue(issues.Count == 33);
            Assert.IsTrue(issues[0].name == "The Sinister Six!");
            Assert.IsTrue(issues[0].issueNumber == 1.0);
            Assert.IsTrue(issues[3].issueNumber == 4.0f);
            Assert.IsTrue(issues[3].issueNumberSuffix == "au");
        }

        [TestMethod]
        public void CanReadComicVineXMLToVolumes()
        {
            var result = File.ReadAllText(TestXMLComicVinePublisherFilteredFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var volumes = cvXMLReader.GetVolumes(result);
            Assert.IsTrue(volumes.Count == 336);
            Assert.IsTrue(volumes[0].id == 1533);
            Assert.IsTrue(volumes[0].name == "Racket Squad in Action");
            Assert.IsTrue(volumes[4].id == 1667);
            Assert.IsTrue(volumes[4].name == "Black Fury");
        }

        [TestMethod]
        public void CanReadComicVineXMLToPublishers()
        {
            var result = File.ReadAllText(TextXMLComicVinePublisherSearchFileName);
            var cvXMLReader = new ComicVineXMLReader();
            var publishers = cvXMLReader.GetPublishers(result);
            Assert.IsTrue(publishers.Count == 8);
            Assert.IsTrue(publishers[0].id == 31);
            Assert.IsTrue(publishers[0].name == "Marvel");
            Assert.IsTrue(publishers[7].id == 3520);
            Assert.IsTrue(publishers[7].name == "Marvelmania International");
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
        public void CanConvertSearchPublisherToPublishers()
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
