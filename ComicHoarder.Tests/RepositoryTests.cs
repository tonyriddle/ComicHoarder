using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComicHoarder.Common;
using ComicHoarder.Repository;
using System.Collections.Generic;

namespace ComicHoarder.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        IRepository repository;
        RepositoryService service;
        
        [TestInitialize]
        public void RepositoryTestInitialize()
        {
            this.repository = new TestRepository();
            this.service = new RepositoryService(repository);
        }
   
        [TestMethod]
        public void CanGetIssue()
        {
            Issue issue = service.GetIssue(1);
            Assert.IsTrue(issue.id == 1);
            Assert.IsTrue(issue.name == "Bugs the Squids");
        }

        [TestMethod]
        public void CanGetPublisher()
        {
            Publisher publisher = service.GetPublisher(3);
            Assert.IsTrue(publisher.id == 3);
            Assert.IsTrue(publisher.name == "Charlton");
        }

        [TestMethod]
        public void CanGetVolume()
        {
            Volume volume = service.GetVolume(2);
            Assert.IsTrue(volume.id == 2);
            Assert.IsTrue(volume.name == "Captain Marvel");
        }

        [TestMethod]
        public void CanDeleteIssue()
        {
            Assert.IsTrue(repository.DeleteIssue(3));
        }

        [TestMethod]
        public void CanDeleteVolume()
        {
            Assert.IsTrue(repository.DeleteVolume(1));
        }

        [TestMethod]
        public void CanDeletePublisher()
        {
            Assert.IsTrue(repository.DeletePublisher(1));
        }

        [TestMethod]
        public void CanSaveIssue()
        {
            Issue issue = new Issue
            {
                id = 4,
                name = "It's a Wonderful World"
            };
            repository.Save(issue);
            Issue savedIssue = repository.GetIssue(4);
            Assert.IsTrue(savedIssue.id == 4);
            Assert.IsTrue(savedIssue.name == "It's a Wonderful World");
        }

        [TestMethod]
        public void CanSaveVolume()
        {
            Volume volume = new Volume
            {
                id = 4,
                name = "Wild West Tales"
            };
            repository.Save(volume);
            Volume savedVolume = repository.GetVolume(4);
            Assert.IsTrue(savedVolume.id == 4);
            Assert.IsTrue(savedVolume.name == "Wild West Tales");
        }

        [TestMethod]
        public void CanSavePublisher()
        {
            Publisher publisher = new Publisher
            {
                id = 4,
                name = "Cheetum Comics"
            };
            repository.Save(publisher);
            Publisher savedPublisher = repository.GetPublisher(4);
            Assert.IsTrue(savedPublisher.id == 4);
            Assert.IsTrue(savedPublisher.name == "Cheetum Comics");
        }

        //TODO Complete Repository Tests
    }

}
