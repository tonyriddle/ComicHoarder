using System;
using ComicHoarder.Common;
using ComicHoarder.WebData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComicHoarder.LiveTests
{
    [TestClass]
    public class WebDataTests
    {
        [TestMethod]
        public void CanGetVolumeFromComicVine()
        {
            IWebConnection connection = new ComicVineWebConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("c562cec823cc6cf7b2513420a05de943479c68f4");
            WebDataService service = new WebDataService(connection, converter, builder);
            Volume volume = service.GetVolume(23402);
            Assert.IsTrue(volume.id == 23402);
        }
        
        [TestMethod]
        public void CanGetVolumeFromComicVineAndMarkAsNotCollectable()
        {
            IWebConnection connection = new ComicVineWebConnection();
            IWebDataConverter converter = new ComicVineConverter();
            IURLBuilder builder = new ComicVineURLBuilder("c562cec823cc6cf7b2513420a05de943479c68f4");
            WebDataService service = new WebDataService(connection, converter, builder);
            Volume volume = service.GetVolume(167594);
            Assert.IsTrue(volume.id == 167594);
            Assert.IsFalse(volume.collectable);
        }
    }
}
