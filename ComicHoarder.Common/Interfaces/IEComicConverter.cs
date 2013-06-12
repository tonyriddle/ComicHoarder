
namespace ComicHoarder.Common
{
    public interface IEComicConverter
    {
        Issue ConvertToIssue(string xml);
    }
}
