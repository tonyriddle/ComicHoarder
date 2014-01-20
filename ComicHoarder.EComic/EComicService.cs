﻿using System;
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
        IEComicDataReader dataReader;
        ICompressionController compressionController;

        public EComicService()
        {
            dataReader = new ComicVineEComicXMLDataReader();
            compressionController = new ZipController();
        }

        public EComicService(ComicVineEComicXMLDataReader dataReader, ICompressionController compressionController)
        {
            this.dataReader = dataReader;
            this.compressionController = compressionController;
        }

        public Issue GetComicInfo(string filename)
        {
            compressionController.SetFileName(filename);
            string data = compressionController.ExtractTextFile("ComicInfo.xml");
            return dataReader.ReadIssueData(data);
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

        public List<Issue> GetIssues(string pathname, bool searchSubDirectory)
        {
            List<Issue> issues = new List<Issue>();
            List<string> fileNames = FindIssuesInPath(pathname, searchSubDirectory);
            foreach (string filename in fileNames)
            {
                issues.Add(GetComicInfo(filename));
            }
            return issues;
        }
    }
}
