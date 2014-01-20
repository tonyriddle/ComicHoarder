using System;
namespace ComicHoarder.Common
{
    public interface IEComicDataReader
    {
        Issue ReadIssueData(string comicInfo);
    }
}
