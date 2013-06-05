using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
