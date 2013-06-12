using System.Drawing;

namespace ComicHoarder.Common
{
    public interface ICompressionController
    {
        string ExtractTextFile(string filename);
        string UpdateTextFile(string filename);
        Image ExtractImage(string filename);
        void SetFileName(string filename);
    }
}
