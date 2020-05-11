using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AsmAD.Models
{
    public class TopicClass
    {
        public int Id_Topic { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Id_Course { get; set; }
        public string Course { get; set; }
        [NotMapped]
        public List<CourseClass> CourseDDL { get; set; }

        public int Id_Trainer { get; set; }
        public string Trainer { get; set; }
        [NotMapped]
        public List<TrainerClass> TrainerDDL { get; set; }
    }

    class TopicList
    {
        DBConnection db;
        public TopicList()
        {
            db = new DBConnection();
        }
        public List<TopicClass> GetTopicClasses(string id_Topic)
        {
            string sql;
            if (string.IsNullOrEmpty(id_Topic))
            {
                sql = "SELECT * FROM Topic";
            }
            else
            {
                sql = "SELECT * FROM Topic WHERE Id_Topic = " + id_Topic;
            }
            List<TopicClass> tList = new List<TopicClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            TopicClass tmpT;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpT = new TopicClass();
                tmpT.Id_Topic = Convert.ToInt32(dt.Rows[i]["Id_Topic"].ToString());
                tmpT.Name = dt.Rows[i]["Name"].ToString();
                tmpT.Description = dt.Rows[i]["Description"].ToString();
                tmpT.Id_Course = Convert.ToInt32(dt.Rows[i]["Id_Course"].ToString());
                tmpT.Id_Trainer = Convert.ToInt32(dt.Rows[i]["Id_Trainer"].ToString());
                tList.Add(tmpT);
            }
            return tList;
        }
        public void AddTopic(TopicClass t)
        {
            string sql = "INSERT INTO Topic(Name, Description, Id_Course, Id_Trainer) VALUES('" + t.Name + "','" + t.Description + "','" + t.Id_Course + "',,'" + t.Id_Trainer + "')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateTopic(TopicClass t)
        {
            string sql = "UPDATE Topic SET Name='" + t.Name + "',Description='" + t.Description + "',Id_Course='" + t.Id_Course + "',Id_Trainer='" + t.Id_Trainer + "' WHERE Id_Topic= " + t.Id_Topic;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteAccount(TopicClass t)
        {
            string sql = "DELETE Topic WHERE Id_Topic =" + t.Id_Topic;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }


}