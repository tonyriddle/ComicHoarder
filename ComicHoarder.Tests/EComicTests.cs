using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using ComicHoarder.EComic;
using ComicHoarder.Common;

namespace ComicHoarder.Tests
{
    [TestClass]
    public class EComicTests
    {
        string TestCBZComicFileName = @"../../../Documentation/Blue Beetle 001 (1967) June-1967.cbz";
        string TestXMLCRComicInfoFileName = @"../../../Documentation/ComicInfoBlueBeetle1.xml";
        string TestPath = @"../../../Documentation/";
        string TestComicInfoFileName = @"ComicInfo.xml";
        string TestImageFileName = @"P00001.jpg";

        [TestMethod]
        public void CanReadXMLIssueDataFromEComic()
        {
            ComicVineEComicXMLDataReader reader = new ComicVineEComicXMLDataReader();
            string comicInfo = File.ReadAllText(TestXMLCRComicInfoFileName);
            Issue issue = reader.ReadIssueData(comicInfo);
            Assert.IsTrue(issue.id == 9383);
            Assert.IsTrue(issue.name == "Bugs the Squids");
            Assert.IsTrue(issue.summary.StartsWith("The Squid Gang crash the party"));
            Assert.IsTrue(issue.issueNumber == 1);
            Assert.IsTrue(issue.publishMonth == 6);
            Assert.IsTrue(issue.publishYear == 1967);
        }

        [TestMethod]
        public void CanReadXMLFromCbz()
        {
            ZipController reader = new ZipController(TestCBZComicFileName);
            string result = reader.ExtractTextFile(TestComicInfoFileName);
            Assert.IsTrue(result.Contains("Bugs the Squids"));
        }

        [TestMethod]
        public void CanGetListOfImagesFromCbz()
        {
            ZipController reader = new ZipController(TestCBZComicFileName);
            List<string> filenames = reader.GetFileNames(".jpg");
            Assert.IsTrue(filenames.Contains(TestImageFileName));
        }

        [TestMethod]
        public void CanExtractImageFromCbz()
        {
            ZipController reader = new ZipController(TestCBZComicFileName);
            Image image = reader.ExtractImage(TestImageFileName);
            Assert.IsTrue(image.Height == 1291);
        }


        [TestMethod]
        public void CanExtractCVIDFromComicInfoNotes()
        {
            string notes = "Scraped metadata from ComicVine [CVDB9383] on 2013.06.03 23:14:42.";
            ComicVineEComicXMLDataReader dataReader = new ComicVineEComicXMLDataReader();
            string id = dataReader.GetCVIDFromNotes(notes);
            Assert.IsTrue(id == "9383");
        }

        [TestMethod]
        public void CanHandleNoCVIDFromComicInfoNotes()
        {
            string notes = "";
            ComicVineEComicXMLDataReader dataReader = new ComicVineEComicXMLDataReader();
            string id = dataReader.GetCVIDFromNotes(notes);
            Assert.IsTrue(id == "");

        }

        [TestMethod]
        public void ServiceCanReturnIssue()
        {
            EComicService eComicService = new EComicService();
            Issue issue = eComicService.GetComicInfo(TestCBZComicFileName);
            Assert.IsTrue(issue.id == 9383);
            Assert.IsTrue(issue.name == "Bugs the Squids");
            Assert.IsTrue(issue.summary.StartsWith("The Squid Gang crash the party"));
            Assert.IsTrue(issue.issueNumber == 1);
            Assert.IsTrue(issue.publishMonth == 6);
            Assert.IsTrue(issue.publishYear == 1967);
        }

        [TestMethod]
        public void ServiceCanFindIssuesInPath()
        {
            var eComicService = new EComicService();
            List<string> filenames = eComicService.FindIssuesInPath(TestPath, false);
            Assert.IsTrue(filenames.Exists(ContainsBlueBeetle003));
        }

        [TestMethod]
        public void ServiceCanFindIssuesInPathSubDirectory()
        {
            var eComicService = new EComicService();
            List<string> filenames = eComicService.FindIssuesInPath(TestPath, true);
            Assert.IsTrue(filenames.Exists(ContainsBlueBeetle006));
        }

        [TestMethod]
        public void ServiceCanFindIssuesInPathAndReturnIssues()
        {
            var eComicService = new EComicService();
            List<Issue> issues = eComicService.GetIssues(TestPath, true);
            Assert.IsTrue(issues.Count == 6); 
            Assert.IsTrue(issues[0].id == 9383);
            Assert.IsTrue(issues[5].id == 226638);
        }

        private static bool ContainsBlueBeetle003(String s)
        {
            return s.Contains("Blue Beetle 003 (1967) October-1967.cbz");
        }
        private static bool ContainsBlueBeetle006(String s)
        {
            return s.Contains("Blue Beetle 006 (1967) April-1967.cbz");
        }
    }
}
