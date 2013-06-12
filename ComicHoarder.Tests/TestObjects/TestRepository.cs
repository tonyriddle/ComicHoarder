using ComicHoarder.Common;
using System.Collections.Generic;
using System.Linq;

namespace ComicHoarder.Tests
{
    public class TestRepository : IRepository
    {
        List<Publisher> publishers;
        List<Volume> volumes;
        List<Issue> issues;
        List<MissingIssue> missingIssues;

        public TestRepository()
        {
            publishers = new List<Publisher>
            {
                new Publisher 
                {
                    id = -1,
                    name = "Marvel"
                },
                new Publisher 
                {
                    id = -2,
                    name = "DC"
                },
                new Publisher 
                {
                    id = -3,
                    name = "Charlton"
                }
            };

            volumes = new List<Volume>
            {
                new Volume 
                {
                    id = -1,
                    name = "Blue Beetle"
                },
                new Volume
                {
                    id = -2,
                    name = "Captain Marvel"
                },
                new Volume
                {
                    id = -3,
                    name = "The Phantom"
                }
            };

            issues = new List<Issue>
            {
                new Issue
                {
                    id = -1,
                    name = "Bugs the Squids"
                },
                new Issue
                {
                    id = -2,
                    name = "To the Moon"
                },
                new Issue
                {
                    id = -3,
                    name = "Horses, Horses, Horses"
                }
            };

            missingIssues = new List<MissingIssue>
            {
                new MissingIssue
                {
                    id = -1,
                    name = "Bugs the Squids",
                    volume_name = "Blue Beetle"
                },
                new MissingIssue
                {
                    id = -2,
                    name = "To the Moon",
                    volume_name = "Captain Marvel"
                },
                new MissingIssue
                {
                    id = -3,
                    name = "Horses, Horses, Horses",
                    volume_name = "The Phantom"
                }
            };
        }

        public Issue GetIssue(int id)
        {
            return (from Issue i in issues where i.id == id select i).Take(1).SingleOrDefault();
        }

        public Publisher GetPublisher(int id)
        {
            return (from Publisher p in publishers where p.id == id select p).Take(1).SingleOrDefault(); ;
        }

        public Volume GetVolume(int id)
        {
            return (from Volume v in volumes where v.id == id select v).Take(1).SingleOrDefault();
        }

        public bool DeleteIssue(int id)
        {
            Issue issue = (from Issue i in issues where i.id == id select i).Take(1).SingleOrDefault();
            issues.Remove(issue);
            return !(issues.Contains(issue));
        }

        public bool DeletePublisher(int id)
        {
            Publisher publisher = (from Publisher i in publishers where i.id == id select i).Take(1).SingleOrDefault();
            publishers.Remove(publisher);
            return !(publishers.Contains(publisher));
        }

        public bool DeleteVolume(int id)
        {
            Volume volume = (from Volume i in volumes where i.id == id select i).Take(1).SingleOrDefault();
            volumes.Remove(volume);
            return !(volumes.Contains(volume));
        }

        public bool Save(Issue issue)
        {
            issues.Add(issue);
            return true;
        }

        public bool Save(Publisher publisher)
        {
            publishers.Add(publisher);
            return true;
        }

        public bool Save(Volume volume)
        {
            volumes.Add(volume);
            return true;
        }

        public bool Save(List<Volume> volumes)
        {
            foreach (Volume volume in volumes)
            {
                volumes.Add(volume);
            }
            return true;
        }

        public bool Save(List<Issue> issues)
        {
            foreach (Issue issue in issues)
            {
                issues.Add(issue);
            }
            return true;
        }

        public bool Save(List<Publisher> publishers)
        {
            foreach (Publisher publisher in publishers)
            {
                publishers.Add(publisher);
            }
            return true;
        }

        public List<Publisher> GetPublishers()
        {
            return publishers;
        }

        public List<Volume> GetVolumes(int id)
        {
            return volumes;
        }

        public List<Issue> GetIssues(int id)
        {
            return issues;
        }

        public List<Issue> GetIssuesByPublisher(int id)
        {
            return issues;
        }

        public List<MissingIssue> GetMissingIssues(int id)
        {
            return missingIssues;
        }

        public bool UpdateVolumeToUncollectable(int id)
        {
            return true;
        }

        public bool PublisherExists(int id)
        {
            return true;
        }

        public bool VolumeExists(int id)
        {
            return true;
        }

        public bool IssueExists(int id)
        {
            return true;
        }

        public bool UpdateIssueToCollected(int id)
        {
            return true;
        }


        public string GetWebServiceKey(string name)
        {
            return "12345678";
        }


        public PieChartMissingIssueRatio GetPieChartData(int publisherId)
        {
            PieChartMissingIssueRatio ratios = new PieChartMissingIssueRatio();
            ratios.MissingIssueRatioList.Add(new KeyValuePair<string, int>("Collected", 2));
            ratios.MissingIssueRatioList.Add(new KeyValuePair<string, int>("Missing", 4));
            return ratios;
        }
    }
}
