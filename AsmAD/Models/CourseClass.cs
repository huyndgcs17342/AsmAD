using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AsmAD.Models
{
    public class CourseClass
    {
        [Display(Name ="Id is: ")]
        public int Id_Course { get; set; }

        [Required(ErrorMessage ="ghn")]
        [Display(Name ="Name is: ")]
        public string Name { get; set; }

        [Display(Name ="Description is: ")]
        public string Description { get; set; }

        [Required(ErrorMessage ="")]
        [Display(Name ="Id Category is: ")]
        public int Id_CateCourse { get; set; }
        public string CateCourse { get; set; }

        [NotMapped]
        public List<CategoryClass> CategoryDDL { get; set; }
    }

    class CourseList
    {
        DBConnection db;
        public CourseList()
        {
            db = new DBConnection();
        }
        public List<CourseClass> GetCourseClasses(string Id_Course)
        {
            string sql;
            if (string.IsNullOrEmpty(Id_Course))
            {
                sql = "SELECT Course.Id_Course, Course.Name, Course.Description, Course.Id_CateCourse, CourseManagerment.Name FROM Course LEFT JOIN CourseManagerment on Course.Id_CateCourse=CourseManagerment.Id_CateCourse";
            }
            else
            {
                sql = "SELECT Course.Id_Course, Course.Name, Course.Description, Course.Id_CateCourse, CourseManagerment.Name FROM Course LEFT JOIN CourseManagerment on Course.Id_CateCourse=CourseManagerment.Id_CateCoursee WHERE Course.Id_Course=" + Id_Course;
            }
            List<CourseClass> cList = new List<CourseClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            CourseClass tmpC;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpC = new CourseClass();
                tmpC.Id_Course = Convert.ToInt32(dt.Rows[i]["Id_Course"].ToString());
                tmpC.Name = dt.Rows[i]["Name"].ToString();
                tmpC.Description = dt.Rows[i]["Description"].ToString();
                tmpC.Id_CateCourse = Convert.ToInt32(dt.Rows[i]["Id_CateCourse"].ToString());
                tmpC.CateCourse = dt.Rows[i]["Name"].ToString();
                cList.Add(tmpC);
            }
            return cList;
        }
        public void AddCourse(CourseClass c)
        {
            string sql = "INSERT INTO Course (Name, Description, Id_CateCourse) VALUES('" + c.Name + "','" + c.Description + "','" + c.Id_CateCourse + "')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateCourse(CourseClass c)
        {
            string sql = "UPDATE Course SET Name='" + c.Name + "',Description='" + c.Description + "',Id_CateCourse='" + c.Id_CateCourse + "' WHERE Id_Account= " + c.Id_Course;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteCourse(CourseClass c)
        {
            string sql = "DELETE Course WHERE Id_Course =" + c.Id_Course;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}