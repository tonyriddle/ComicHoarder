using System.Collections.Generic;

namespace ComicHoarder.Common
{
    public interface IEComicService
    {
        Issue GetComicInfoFromEComic(string filename);
        List<string> GetIssueNamesInPath(string pathName, bool searchSubDirectory);
        List<Issue> GetIssuesInPath(string pathname, bool searchSubDirectory);
    }
}
