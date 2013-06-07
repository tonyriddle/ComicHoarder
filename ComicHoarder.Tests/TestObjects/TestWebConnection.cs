using ComicHoarder.Common;
using System.IO;

namespace ComicHoarder.Tests
{
    public class TestWebConnection : IWebConnection
    {

        string TestXMLComicVinePublisherFilteredFileName = @"../../../Documentation/Publisherfiltered.xml";
        string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
        string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";
        string TestXMLComicVinePublisherSearchFileName = @"../../../Documentation/publishersearch.xml";

        public string Query(string Url)
        {
            string FileName = "";
            string contents = "";

            if (Url.Contains("http://www.comicvine.com/api/publisher/4010-125/"))
            {
                FileName = TestXMLComicVinePublisherFilteredFileName;
            }

            if (Url.Contains("http://www.comicvine.com/api/volume/4050-2189/"))
            {
                FileName = TestXMLComicVineVolumeUnFilteredFileName;
            }

            if (Url.Contains("http://www.comicvine.com/api/issue/4000-187507/"))
            {
                FileName = TestXMLComicVineIssueUnFilteredFileName;
            }

            if (Url.Contains("http://www.comicvine.com/api/search/") && Url.Contains("publisher"))
            {
                FileName = TestXMLComicVinePublisherSearchFileName;
            }
        
            using (StreamReader sr = new StreamReader(FileName))
            {
                contents = sr.ReadToEnd();
            }
            return contents;
        }
    }
}
