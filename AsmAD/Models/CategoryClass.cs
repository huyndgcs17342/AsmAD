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
    public class CategoryClass
    {
        [Display(Name ="Id is: ")]
        public int Id_Cate { get; set; }

        [Required(ErrorMessage ="The Name field is empty")]
        [Display(Name="Name is: ")]
        public string Name { get; set; }

        [Display(Name ="Description is: ")]
        public string Description { get; set; }

    }
    class CategoryList
    {
        DBConnection db;
        public CategoryList()
        {
            db = new DBConnection();
        }
        public List<CategoryClass> GetCategoryClasses(string Id_Category)
        {
            string sql;
            if (string.IsNullOrEmpty(Id_Category))
            {
                sql = "SELECT * FROM CourseManagerment";
            }
            else
            {
                sql = "SELECT * FROM CourseManagerment WHERE Id_CateCourse =" + Id_Category;
            }
            List<CategoryClass> cateList = new List<CategoryClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            CategoryClass tmpCate;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpCate = new CategoryClass();
                tmpCate.Id_Cate=Convert.ToInt32(dt.Rows[i]["Id_CateCourse"].ToString());
                tmpCate.Name= dt.Rows[i]["Name"].ToString();
                tmpCate.Description = dt.Rows[i]["Description"].ToString();
                cateList.Add(tmpCate);
            }
            return cateList;
        }

        public void AddCategory(CategoryClass cate)
        {
            string sql = "INSERT INTO CourseManagerment(Name, Description) VALUES('" + cate.Name + "','" + cate.Description + "')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateCategory(CategoryClass cate)
        {
            string sql = "UPDATE CourseManagerment SET Name='" + cate.Name + "',Description='" + cate.Description + "' WHERE Id_Account= " + cate.Id_Cate;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteCategory(CategoryClass cate)
        {
            string sql = "DELETE CourseManagerment WHERE Id_CateCourse =" + cate.Id_Cate;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}