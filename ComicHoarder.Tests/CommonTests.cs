using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComicHoarder.Tests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void CanParseInt()
        {
            Assert.AreEqual(ParseHelper.ParseInt("3"), 3);
        }

        [TestMethod]
        public void CanParseFloat()
        {
            Assert.AreEqual(ParseHelper.ParseFloat("3.14"), 3.14f);
        }

        [TestMethod]
        public void CanParseBool()
        {
            Assert.IsTrue(ParseHelper.ParseBool("True"));
            Assert.IsFalse(ParseHelper.ParseBool("False"));
        }

        [TestMethod]
        public void CanGetEnclosedString()
        {
            Assert.AreEqual(ParseHelper.GetEnclosedString("sometextitssupposedtobegoodforyou", "textits", "good"), "supposedtobe");
            Assert.AreEqual(ParseHelper.GetEnclosedString("sometextitssupposedtobegoodforyou", "textits", ""), "supposedtobegoodforyou");
            Assert.AreEqual(ParseHelper.GetEnclosedString("sometextitssupposedtobegoodforyou", "", "good"), "sometextitssupposedtobe");
            Assert.AreEqual(ParseHelper.GetEnclosedString("sometextitssupposedtobegoodforyou", "good", "textits"), "");
            Assert.AreEqual(ParseHelper.GetEnclosedString("sometextitssupposedtobegoodforyou", "notinthere1", "notinthere2"), "");
        }
    }
}
