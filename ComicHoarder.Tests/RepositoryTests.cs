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
        string testMode = "Test";
        //string testMode = "Live";

        IRepository repository;
        RepositoryService service;
        
        [TestInitialize]
        public void RepositoryTestInitialize()
        {
            if (testMode == "Live")
            {
                this.repository = new MSSQLDatabase();
                InitializeDB();
            }
            else
            {
                this.repository = new TestRepository();
            }
            this.service = new RepositoryService(repository);
        }

        [TestCleanup]
        public void RepositoryTestCleanup()
        {
            if (testMode == "Live")
            {
                TearDownDB();
            }
        }

        [TestMethod]
        public void CanGetIssue()
        {
            Issue issue = service.GetIssue(-1);
            Assert.IsTrue(issue.id == -1);
            Assert.IsTrue(issue.name == "Bugs the Squids");
        }

        [TestMethod]
        public void CanGetPublisher()
        {
            Publisher publisher = service.GetPublisher(-1);
            Assert.IsTrue(publisher.id == -1);
            Assert.IsTrue(publisher.name == "Marvel");
        }

        [TestMethod]
        public void CanGetVolume()
        {
            Volume volume = service.GetVolume(-1);
            Assert.IsTrue(volume.id == -1);
            Assert.IsTrue(volume.name == "Blue Beetle");
        }

        [TestMethod]
        public void CanDeleteIssue()
        {
            Assert.IsTrue(repository.DeleteIssue(-3));
        }

        [TestMethod]
        public void CanDeleteVolume()
        {
            Assert.IsTrue(repository.DeleteVolume(-3));
        }

        [TestMethod]
        public void CanDeletePublisher()
        {
            Assert.IsTrue(repository.DeletePublisher(-3));
        }

        [TestMethod]
        public void CanSaveIssue()
        {
            Issue issue = new Issue
            {
                id = -7,
                name = "It's a Wonderful World",
                volumeId = -1
            };
            repository.Save(issue);
            Issue savedIssue = repository.GetIssue(-7);
            Assert.IsTrue(savedIssue.id == -7);
            Assert.IsTrue(savedIssue.name == "It's a Wonderful World");
        }

        [TestMethod]
        public void CanSaveVolume()
        {
            Volume volume = new Volume
            {
                id = -7,
                name = "Wild West Tales",
                publisherId = -1,
                description = "Wild Wild West"
            };
            repository.Save(volume);
            Volume savedVolume = repository.GetVolume(-7);
            Assert.IsTrue(savedVolume.id == -7);
            Assert.IsTrue(savedVolume.name == "Wild West Tales");
        }

        [TestMethod]
        public void CanSavePublisher()
        {
            Publisher publisher = new Publisher
            {
                id = -7,
                name = "Cheetum Comics",
                description = "Steelers"
            };
            repository.Save(publisher);
            Publisher savedPublisher = repository.GetPublisher(-7);
            Assert.IsTrue(savedPublisher.id == -7);
            Assert.IsTrue(savedPublisher.name == "Cheetum Comics");
        }

        //TODO Complete Repository Tests


        private void InitializeDB()
        {
            List<Publisher> publishers;
            List<Volume> volumes;
            List<Issue> issues;

            publishers = new List<Publisher>
            {
                new Publisher 
                {
                    id = -1,
                    name = "Marvel",
                    description = "The finest"
                },
                new Publisher 
                {
                    id = -2,
                    name = "DC",
                    description = "Batman and such"
                },
                new Publisher 
                {
                    id = -3,
                    name = "Charlton",
                    description = "old timey"
                }
            };

            volumes = new List<Volume>
            {
                new Volume 
                {
                    id = -1,
                    name = "Blue Beetle",
                    publisherId = -1,
                    description = "something"
                },
                new Volume
                {
                    id = -2,
                    name = "Captain Marvel",
                    publisherId = -1,
                    description = "something else"
                },
                new Volume
                {
                    id = -3,
                    name = "The Phantom",
                    publisherId = -1,
                    description = "purple suit"
                }
            };

            issues = new List<Issue>
            {
                new Issue
                {
                    id = -1,
                    name = "Bugs the Squids",
                    volumeId = -1,
                    collected = true
                },
                new Issue
                {
                    id = -2,
                    name = "To the Moon",
                    volumeId = -1,
                    collected = true
                },
                new Issue
                {
                    id = -3,
                    name = "Horses, Horses, Horses",
                    volumeId = -1,
                    collected = true
                },
                new Issue
                {
                    id = -4,
                    name = "Bugs the Squids",
                    volumeId = -1,
                    collected = false
                },
                new Issue
                {
                    id = -5,
                    name = "To the Moon",
                    volumeId = -1,
                    collected = false
                },
                new Issue
                {
                    id = -6,
                    name = "Horses, Horses, Horses",
                    volumeId = -1,
                    collected = false
                }
            };

            repository.Save(publishers);
            repository.Save(volumes);
            repository.Save(issues);
        }

        private void TearDownDB()
        {
            repository.DeletePublisher(-1);
            repository.DeletePublisher(-2);
            repository.DeletePublisher(-3);
            repository.DeletePublisher(-7);
            repository.DeleteVolume(-1);
            repository.DeleteVolume(-2);
            repository.DeleteVolume(-3);
            repository.DeleteVolume(-7);
            repository.DeleteIssue(-1);
            repository.DeleteIssue(-2);
            repository.DeleteIssue(-3);
            repository.DeleteIssue(-4);
            repository.DeleteIssue(-5);
            repository.DeleteIssue(-6);
            repository.DeleteIssue(-7);
        }
    }

}
