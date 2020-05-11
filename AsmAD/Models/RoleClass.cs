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
    public class RoleClass
    {
        public int Id_Role { get; set; }

        [Required(ErrorMessage ="Name is empty")]
        [Display(Name ="Role")]
        public string Name { get; set; }
        //[NotMapped]
        //public List<RoleClass> RoleDDL { get; set; }
    }

    class RoleList
    {
        DBConnection db;
        public RoleList()
        {
            db = new DBConnection();
        }
        public List<RoleClass> GetRoleClasses(string Id_Role)
        {
            string sql;
            if (string.IsNullOrEmpty(Id_Role))
            {
                sql = "SELECT * FROM AccountRole";
            }
            else
            {
                sql = "SELECT * FROM AccountRole WHERE Id_Role =" + Id_Role;
            }
            List<RoleClass> roleList = new List<RoleClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            RoleClass tmpRole;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpRole = new RoleClass();
                tmpRole.Id_Role = Convert.ToInt32(dt.Rows[i]["Id_Role"].ToString());
                tmpRole.Name = dt.Rows[i]["Name"].ToString();
                roleList.Add(tmpRole);
            }
            return roleList;
        }
        public void AddRole(RoleClass role)
        {
            string sql = "INSERT INTO AccountRole(Name) VALUES('" + role.Name + "')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void UpdateRole(RoleClass role)
        {
            string sql = "UPDATE AccountRole SET Name='" + role.Name + "' WHERE Id_Role= " + role.Id_Role;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteRole(RoleClass role)
        {
            string sql = "DELETE AccountRole WHERE Id_Role =" + role.Id_Role;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}