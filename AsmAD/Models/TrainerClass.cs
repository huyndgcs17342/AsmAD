using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AsmAD.Models
{
    public class TrainerClass
    {
        public int Id_Trainer { get; set; }

        public string Name { get; set; }

        public string ExOrInType { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public int Id_Topic { get; set; }
        public string Topic { get; set; }
        [NotMapped]
        public List<TopicClass> TopicDDL { get; set; }
    }

    class TrainerList
    {

        DBConnection db;
        public TrainerList()
        {
            db = new DBConnection();
        }
        public List<TrainerClass> GetTrainerClasses(string id_Trainer)
        {
            string sql;
            if (string.IsNullOrEmpty(id_Trainer))
            {
                sql = "SELECT * FROM TrainerProfile";
            }
            else
            {
                sql = "SELECT * FROM TrainerProfile WHERE Id_Trainer = " + id_Trainer;
            }
            List<TrainerClass> tList = new List<TrainerClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            TrainerClass tmpT;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpT = new TrainerClass();
                tmpT.Id_Trainer = Convert.ToInt32(dt.Rows[i]["Id_Topic"].ToString());
                tmpT.Name = dt.Rows[i]["Name"].ToString();
                tmpT.ExOrInType = dt.Rows[i]["Name"].ToString();
                tmpT.Telephone = dt.Rows[i]["Telephone"].ToString();
                tmpT.Email = dt.Rows[i]["EmailAddress"].ToString();
                tmpT.Id_Topic = Convert.ToInt32(dt.Rows[i]["Id_Course"].ToString());
                tList.Add(tmpT);
            }
            return tList;
        }
        public void AddTrainer(TrainerClass t)
        {
            string sql = "INSERT INTO TrainerProfile(Name, ExOrInType, Telephone, EmailAddress, Id_Topic) VALUES('" + t.Name + "','" + t.ExOrInType + "','" + t.Telephone + "','" + t.Email + "',,'" + t.Id_Topic + "')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateTrainer(TrainerClass t)
        {
            string sql = "UPDATE TrainerProfile SET Name='" + t.Name + "',ExOrInType='" + t.ExOrInType + "',Telephone='" + t.Telephone + "',EmailAddress='" + t.Email + "',Id_Topic='" + t.Id_Topic + "' WHERE Id_Trainer= " + t.Id_Trainer;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteTrainer(TrainerClass t)
        {
            string sql = "DELETE TrainerProfile WHERE Id_Trainer =" + t.Id_Trainer;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}