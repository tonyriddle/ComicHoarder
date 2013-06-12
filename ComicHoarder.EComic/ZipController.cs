using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;
using ComicHoarder.Common;
using System.Drawing;

namespace ComicHoarder.EComic
{
    public class ZipController : ICompressionController
    {
        public string FileName { get; set; }

        public ZipController(string filename)
        {
            FileName = filename;
        }

        public ZipController()
        {

        }

        public string ExtractTextFile(string filename)
        {
            Stream stream = new MemoryStream();
            using (ZipFile zip = ZipFile.Read(FileName))
            {
                ZipEntry e = zip[filename];
                e.Extract(stream);
            }
            StreamReader sr = new StreamReader(stream);
            sr.BaseStream.Seek(0, SeekOrigin.Begin); // streamreader is set to end, set back to beginning
            return sr.ReadToEnd();
        }

        public string UpdateTextFile(string filename) //Update with other db id's? Will CVScraper overwrite if rescrape?
        { //TODO implement text file updating in cbz file
            throw new NotImplementedException();
        }

        public Image ExtractImage(string filename)
        {
            Stream stream = new MemoryStream();
            using (ZipFile zip = ZipFile.Read(FileName))
            {
                ZipEntry e = zip[filename];
                e.Extract(stream);
            } 
            return Image.FromStream(stream);
        }

        public List<string> GetFileNames(string filter)
        {
            ZipFile zip = new ZipFile(FileName);
            ICollection<ZipEntry> entries = zip.Entries;
            var fileNames = from e in entries where e.FileName.Contains(filter) select e.FileName;
            return fileNames.OrderBy(x => x).ToList();
        }

        public void SetFileName(string filename)
        {
            this.FileName = filename;
        }
    }
}
