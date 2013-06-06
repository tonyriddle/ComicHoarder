using System;
namespace ComicHoarder.Common
{
    public interface IComicVineReprintDetector
    {
        bool DetectReprint(ComicHoarder.Common.Volume volume);
    }
}
