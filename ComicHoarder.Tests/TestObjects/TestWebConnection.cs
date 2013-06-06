using ComicHoarder.Common;
using System.IO;

namespace ComicHoarder.Tests
{
    public class TestWebConnection : IWebConnection
    {

        string TestXMLComicVinePublisherUnFilteredFileName = @"../../../Documentation/PublisherUnfiltered.xml";
        string TestXMLComicVineVolumeUnFilteredFileName = @"../../../Documentation/volume.xml";
        string TestXMLComicVineIssueUnFilteredFileName = @"../../../Documentation/issue.xml";
        string TestXMLComicVinePublisherSearchFileName = @"../../../Documentation/publishersearch.xml";

        public string Query(string Url)
        {
            string FileName = "";
            string contents = "";

            if (Url.Contains("http://www.comicvine.com/api/publisher/4010-31/"))
            {
                FileName = TestXMLComicVinePublisherUnFilteredFileName;
            }

            if (Url.Contains("http://www.comicvine.com/api/volume/4050-1234/"))
            {
                FileName = TestXMLComicVineVolumeUnFilteredFileName;
            }

            if (Url.Contains("http://www.comicvine.com/api/issue/4000-1234/"))
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
