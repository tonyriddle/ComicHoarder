using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ComicHoarder.Common;

namespace ComicHoarder.EComic
{
    public class EComicService : IEComicService
    {
        IEComicConverter converter;
        ICompressionController compressionController;

        public EComicService()
        {
            converter = new ComicInfoConverter();
            compressionController = new ZipController();
        }

        public EComicService(IEComicConverter converter, ICompressionController compressionController)
        {
            this.converter = converter;
            this.compressionController = compressionController;
        }

        public Issue GetComicInfo(string filename)
        {
            compressionController.SetFileName(filename);
            string data = compressionController.ExtractTextFile("ComicInfo.xml");
            return converter.ConvertToIssue(data);
        }

        public List<string> FindIssuesInPath(string pathName, bool searchSubDirectory)
        {
            string[] filePaths;
            if (searchSubDirectory)
            {
                filePaths = Directory.GetFiles(pathName, "*.cbz", SearchOption.AllDirectories);
            }
            else
            {
                filePaths = Directory.GetFiles(pathName, "*.cbz");
            }
            return filePaths.ToList<string>();
        }
    }
}
