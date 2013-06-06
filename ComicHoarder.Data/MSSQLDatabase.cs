using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ComicHoarder.Common;

namespace ComicHoarder
{
    public class MSSQLDatabase : IRepository
    {
        private string constring;

        public MSSQLDatabase()
        {
            //TODO appconfig
            //AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ComicHoarder\");
            //constring = ConfigurationManager.ConnectionStrings["ComicHoarderConnectionString"].ToString();
        }

        public MSSQLDatabase(string connectionstring)
        {
            constring = connectionstring;
        }

        public bool Save(Publisher publisher)
        {
            if (PublisherExists(publisher.id))
            {
                return Update(publisher);
            }

            return Insert(publisher);
        }

        public bool Insert(Publisher publisher)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "insert into publisher "
                        + "(id, name, description, enabled, date_last_updated) "
                        + "values (@id, @name, @description, @enabled, @date_last_updated)";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", publisher.id);
                command.Parameters.AddWithValue("@name", TruncateString(publisher.name, 100));
                command.Parameters.AddWithValue("@description", TruncateString(publisher.description, 1000));
                command.Parameters.AddWithValue("@enabled", publisher.enabled);
                if (publisher.dateLastUpdated == DateTime.MinValue)
                {
                    publisher.dateLastUpdated = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_last_updated", publisher.dateLastUpdated);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Save(Volume volume)
        {
            if (VolumeExists(volume.id))
            {
                return Update(volume);
            }

            return Insert(volume);
        }

        public bool Insert(Volume volume)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "insert into volume "
                        + "(id, publisher_id, name, description, date_added, date_last_updated, collectable, count_of_issues, start_year, enabled, complete) "
                        + "values (@id, @publisher_id, @name, @description, @date_added, @date_last_updated, @collectable, @count_of_issues, @start_year, @enabled, @complete)";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", volume.id);
                command.Parameters.AddWithValue("@publisher_id", volume.publisherId);
                command.Parameters.AddWithValue("@name", TruncateString(volume.name, 100));
                command.Parameters.AddWithValue("@description", TruncateString(volume.description, 1000));
                if (volume.dateAdded == DateTime.MinValue)
                {
                    volume.dateAdded = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_added", volume.dateAdded);
                if (volume.dateLastUpdated == DateTime.MinValue)
                {
                    volume.dateLastUpdated = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_last_updated", volume.dateLastUpdated);
                command.Parameters.AddWithValue("@collectable", volume.collectable);
                command.Parameters.AddWithValue("@count_of_issues", volume.countOfIssues);
                command.Parameters.AddWithValue("@start_year", volume.startYear);
                command.Parameters.AddWithValue("@enabled", volume.enabled);
                command.Parameters.AddWithValue("@complete", volume.complete);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Save(List<Publisher> publishers)
        {
            int rowsupdated = 0; //TODO make this work right and be threaded and update the interface instead of having this in here to watch while running
            int errors = 0;
            foreach (Publisher publisher in publishers)
            {
                if (Save(publisher))
                {
                    rowsupdated++;
                }
                else
                {
                    errors++;
                }
            }

            if (errors != 0) //TODO put in transaction, this is returning false for any error in the group/or write errors to screen and save others
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Save(List<Volume> volumes)
        {
            int rowsupdated = 0;
            int errors = 0;
            foreach (Volume volume in volumes)
            {
                if (Save(volume))
                {
                    rowsupdated++;
                }
                else
                {
                    errors++;
                }
            }

            if (errors != 0) //TODO put in transaction, this is returning false for any error in the group
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Save(List<Issue> issues)
        {
            int rowsupdated = 0;
            int errors = 0;
            foreach (Issue issue in issues)
            {
                if (Save(issue))
                {
                    rowsupdated++;
                }
                else
                {
                    errors++;
                }
            }

            if (errors != 0) //TODO put in transaction, this is returning false for any error in the group
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Save(Issue issue)
        {
            if (IssueExists(issue.id))
            {
                return Update(issue);
            }

            return Insert(issue);
        }

        public bool Insert(Issue issue)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "insert into issue "
                        + "(id, volume_id, name, issue_number, publish_month, publish_year, collected, enabled) "
                        + "values (@id, @volume_id, @name, @issue_number, @publish_month, @publish_year, @collected, @enabled)";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", issue.id);
                command.Parameters.AddWithValue("@volume_id", issue.volumeId);
                command.Parameters.AddWithValue("@name", TruncateString(issue.name, 100));
                command.Parameters.AddWithValue("@issue_number", issue.issueNumber);
                command.Parameters.AddWithValue("@publish_month", issue.publishMonth);
                command.Parameters.AddWithValue("@publish_year", issue.publishYear);
                command.Parameters.AddWithValue("@collected", issue.collected);
                command.Parameters.AddWithValue("@enabled", issue.enabled);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool PublisherExists(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select 1 from Publisher where id = @id", con);
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("publisher");
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VolumeExists(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select 1 from Volume where id = @id", con);
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("volume");
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IssueExists(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select 1 from Issue where id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Update(Publisher publisher)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "update publisher set name = @name, description = @description, date_last_updated = @date_last_updated, enabled = @enabled where id = @id";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", publisher.id);
                command.Parameters.AddWithValue("@name", TruncateString(publisher.name, 100));
                command.Parameters.AddWithValue("@description", TruncateString(publisher.description, 1000));
                command.Parameters.AddWithValue("@enabled", publisher.enabled);
                if (publisher.dateLastUpdated == DateTime.MinValue)
                {
                    publisher.dateLastUpdated = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_last_updated", publisher.dateLastUpdated);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Update(Volume volume)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "update volume set publisher_id = @publisher_id, name = @name, description = @description, date_added = @date_added, date_last_updated = @date_last_updated, count_of_issues = @count_of_issues, start_year = @start_year, enabled = @enabled, complete = @complete where id = @id";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", volume.id);
                command.Parameters.AddWithValue("@publisher_id", volume.publisherId);
                command.Parameters.AddWithValue("@name", TruncateString(volume.name, 100));
                command.Parameters.AddWithValue("@description", TruncateString(volume.description, 1000));
                if (volume.dateAdded == DateTime.MinValue)
                {
                    volume.dateAdded = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_added", volume.dateAdded);
                if (volume.dateLastUpdated == DateTime.MinValue)
                {
                    volume.dateLastUpdated = new DateTime(1900, 1, 1);
                }
                command.Parameters.AddWithValue("@date_last_updated", volume.dateLastUpdated);
                //command.Parameters.AddWithValue("@collectable", volume.collectable); //Don't want to update collectable unless it's newly set to TPB
                command.Parameters.AddWithValue("@count_of_issues", volume.countOfIssues);
                command.Parameters.AddWithValue("@start_year", volume.startYear);
                command.Parameters.AddWithValue("@enabled", volume.enabled);
                command.Parameters.AddWithValue("@complete", volume.complete);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Update(Issue issue)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                string sql = "update issue set volume_id = @volume_id, name = @name, issue_number = @issue_number, publish_month = @publish_month, publish_year = @publish_year, enabled = @enabled where id = @id";
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@id", issue.id);
                command.Parameters.AddWithValue("@volume_id", issue.volumeId);
                command.Parameters.AddWithValue("@name", TruncateString(issue.name, 100));
                command.Parameters.AddWithValue("@issue_number", issue.issueNumber);
                command.Parameters.AddWithValue("@publish_month", issue.publishMonth);
                command.Parameters.AddWithValue("@publish_year", issue.publishYear);
                //command.Parameters.AddWithValue("@collected", issue.collected); Don't want to update collected values
                command.Parameters.AddWithValue("@enabled", issue.enabled);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Publisher GetPublisher(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, name, description, enabled, date_last_updated from Publisher where id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("publisher");
                ad.Fill(ds);
                return FillPublisher(ds);
            }
        }

        public List<Publisher> GetPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, name, description, enabled, date_last_updated from Publisher order by name", con);
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("publisher");
                ad.Fill(ds);
                return FillPublishers(ds);
            }
        }

        public List<Volume> GetVolumes(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, publisher_id, name, description, date_added, date_last_updated, collectable, count_of_issues, start_year, enabled, complete from volume where publisher_id = @id order by name", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("volume");
                ad.Fill(ds);
                return FillVolumes(ds);
            }

        }

        public Volume GetVolume(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, publisher_id, name, description, date_added, date_last_updated, collectable, count_of_issues, start_year, enabled, complete from volume where id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("volume");
                ad.Fill(ds);
                return FillVolume(ds);
            }
        }

        public List<Issue> GetIssues(int volumeId)
        {
            List<Issue> issues = new List<Issue>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, volume_id, name, issue_number, publish_month, publish_year, collected, enabled from issue where volume_id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = volumeId;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                return FillIssues(ds);
            }
        }

        public List<Issue> GetIssuesByPublisher(int publisherId)
        {
            List<Issue> issues = new List<Issue>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select i.id, i.volume_id, i.name, i.issue_number, i.publish_month, i.publish_year, i.collected, i.enabled, v.name as volume_name, p.name as publisher_name  from volume v join issue i on v.id = i.volume_id join publisher p on p.id = v.publisher_id where p.id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = publisherId;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                return FillIssues(ds);
            }
        }
        
        public List<MissingIssue> GetMissingIssues(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command;
                if (id == 0) // 0 is all publishers
                {
                    command = new SqlCommand("select i.id, i.volume_id, i.name, i.issue_number, i.publish_month, i.publish_year, i.collected, i.enabled, v.name as volume_name, p.name as publisher_name  from volume v join issue i on v.id = i.volume_id join publisher p on p.id = v.publisher_id where i.collected = 0 and v.collectable = 1 order by v.name, i.publish_year, i.issue_number", con);
                }
                else
                {
                    command = new SqlCommand("select i.id, i.volume_id, i.name, i.issue_number, i.publish_month, i.publish_year, i.collected, i.enabled, v.name as volume_name, p.name as publisher_name  from volume v join issue i on v.id = i.volume_id join publisher p on p.id = v.publisher_id where i.collected = 0 and v.collectable = 1 and p.id = @id order by v.name, i.publish_year, i.issue_number", con);
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                }
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                return FillMissingIssues(ds);
            }
        }
        
        public Issue GetIssue(int id)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select id, volume_id, name, issue_number, publish_month, publish_year, collected, enabled from issue where id = @id", con);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                return FillIssue(ds);
            }
        }

        public bool DeletePublisher(int id)
        {
            if (PublisherExists(id))
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "delete from publisher where id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteVolume(int id)
        {
            if (VolumeExists(id))
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "delete from volume where id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteIssue(int id)
        {
            if (IssueExists(id))
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "delete from issue where id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private Publisher FillPublisher(DataSet ds)
        {
            Publisher publisher = new Publisher();
            if (ds.Tables[0].Rows.Count != 0)
            {
                publisher.id = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["id"].ToString());
                publisher.name = ds.Tables[0].Rows[0]["name"].ToString();
                publisher.description = ds.Tables[0].Rows[0]["description"].ToString();
                publisher.enabled = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["enabled"].ToString());
                publisher.dateLastUpdated = ParseHelper.ParseDateTime(ds.Tables[0].Rows[0]["date_last_updated"].ToString());
            }
            return publisher;
        }

        private List<Publisher> FillPublishers(DataSet ds)
        {
            List<Publisher> publishers = new List<Publisher>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Publisher publisher = new Publisher();
                publisher.id = ParseHelper.ParseInt(row["id"].ToString());
                publisher.name = row["name"].ToString();
                publisher.description = row["description"].ToString();
                publisher.enabled = ParseHelper.ParseBool(row["enabled"].ToString());
                publisher.dateLastUpdated = ParseHelper.ParseDateTime(row["date_last_updated"].ToString());
                publishers.Add(publisher);
            }
            return publishers;
        }

        private Volume FillVolume(DataSet ds)
        {
            Volume volume = new Volume();
            if (ds.Tables[0].Rows.Count != 0)
            {
                volume.id = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["id"].ToString());
                volume.publisherId = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["publisher_id"].ToString());
                volume.name = ds.Tables[0].Rows[0]["name"].ToString();
                volume.description = ds.Tables[0].Rows[0]["description"].ToString();
                volume.dateAdded = ParseHelper.ParseDateTime(ds.Tables[0].Rows[0]["date_added"].ToString());
                volume.dateLastUpdated = ParseHelper.ParseDateTime(ds.Tables[0].Rows[0]["date_last_updated"].ToString());
                volume.collectable = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["collectable"].ToString());
                volume.countOfIssues = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["count_of_issues"].ToString());
                volume.startYear = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["start_year"].ToString());
                volume.enabled = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["enabled"].ToString());
                volume.complete = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["complete"].ToString());
            }
            return volume;
        }

        private List<Volume> FillVolumes(DataSet ds)
        {
            List<Volume> volumes = new List<Volume>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Volume volume = new Volume();
                volume.id = ParseHelper.ParseInt(row["id"].ToString());
                volume.publisherId = ParseHelper.ParseInt(row["publisher_id"].ToString());
                volume.name = row["name"].ToString();
                volume.description = row["description"].ToString();
                volume.dateAdded = ParseHelper.ParseDateTime(row["date_added"].ToString());
                volume.dateLastUpdated = ParseHelper.ParseDateTime(row["date_last_updated"].ToString());
                volume.collectable = ParseHelper.ParseBool(row["collectable"].ToString());
                volume.countOfIssues = ParseHelper.ParseInt(row["count_of_issues"].ToString());
                volume.startYear = ParseHelper.ParseInt(row["start_year"].ToString());
                volume.enabled = ParseHelper.ParseBool(row["enabled"].ToString());
                volume.complete = ParseHelper.ParseBool(row["complete"].ToString());
                volumes.Add(volume);
            }
            return volumes;
        }

        private List<Issue> FillIssues(DataSet ds)
        {
            List<Issue> issues = new List<Issue>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Issue issue = new Issue();
                issue.id = ParseHelper.ParseInt(row["id"].ToString());
                issue.volumeId = ParseHelper.ParseInt(row["volume_id"].ToString());
                issue.name = row["name"].ToString();
                issue.issueNumber = ParseHelper.ParseFloat(row["issue_number"].ToString());
                issue.publishMonth = ParseHelper.ParseInt(row["publish_month"].ToString());
                issue.publishYear = ParseHelper.ParseInt(row["publish_year"].ToString());
                issue.collected = ParseHelper.ParseBool(row["collected"].ToString());
                issue.enabled = ParseHelper.ParseBool(row["enabled"].ToString());
                issues.Add(issue);
            }
            return issues;
        }
        
        private List<MissingIssue> FillMissingIssues(DataSet ds)
        {
            List<MissingIssue> issues = new List<MissingIssue>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                MissingIssue issue = new MissingIssue();
                issue.id = ParseHelper.ParseInt(row["id"].ToString());
                issue.volume_id = ParseHelper.ParseInt(row["volume_id"].ToString());
                issue.name = row["name"].ToString();
                issue.issue_number = ParseHelper.ParseFloat(row["issue_number"].ToString());
                issue.publish_month = ParseHelper.ParseInt(row["publish_month"].ToString());
                issue.publish_year = ParseHelper.ParseInt(row["publish_year"].ToString());
                issue.collected = ParseHelper.ParseBool(row["collected"].ToString());
                issue.enabled = ParseHelper.ParseBool(row["enabled"].ToString());
                issue.volume_name = row["volume_name"].ToString();
                issue.publisher_name = row["publisher_name"].ToString();
                issues.Add(issue);
            }
            return issues;
        }
        
        private Issue FillIssue(DataSet ds)
        {
            Issue issue = new Issue();
            if (ds.Tables[0].Rows.Count != 0)
            {
                issue.id = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["id"].ToString());
                issue.volumeId = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["volume_id"].ToString());
                issue.name = ds.Tables[0].Rows[0]["name"].ToString();
                issue.issueNumber = ParseHelper.ParseFloat(ds.Tables[0].Rows[0]["issue_number"].ToString());
                issue.publishMonth = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["publish_month"].ToString());
                issue.publishYear = ParseHelper.ParseInt(ds.Tables[0].Rows[0]["publish_year"].ToString());
                issue.collected = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["collected"].ToString());
                issue.enabled = ParseHelper.ParseBool(ds.Tables[0].Rows[0]["enabled"].ToString());
            }
            return issue;
        }

        public bool UpdateIssueToCollected(int id)
        {
            if (IssueExists(id))
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "update issue set collected = 1 where id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public bool UpdateVolumeToUncollectable(int id)
        {
            if (VolumeExists(id))
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    string sql = "update volume set collectable = 0 where id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }


        private object TruncateString(string p, int length)
        {
            if (p != null && p.Length > length)
            {
                return p.Substring(0, length);
            }
            else
            {
                return p;
            }
        }


        public string GetWebServiceKey(string name)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("select value from settings where name = @name", con);
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                SqlDataAdapter ad = new SqlDataAdapter(command);
                DataSet ds = new DataSet("issue");
                ad.Fill(ds);
                return ds.Tables[0].Rows[0]["value"].ToString();
            }
        }
    }
}
