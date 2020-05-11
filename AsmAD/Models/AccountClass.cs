using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace AsmAD.Models
{
    public class AccountClass
    {
        [Display(Name = "Id is: ")]
        public int Id_Account { get; set; }

        [Required(ErrorMessage = "The account field is empty")]
        [Display(Name = "Account is: ")]
        public string Account { get; set; }
        
        [Required(ErrorMessage = "The password field is empty")]
        [Display(Name ="Password is: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The role field is empty")]
        [Display(Name = "Role is: ")]
        public string Role { get; set; }
        public int Id_Role { get; set; }
        [NotMapped]
        public List<RoleClass> RoleDDL { get; set; }
    }

    class AccountList
    {
        DBConnection db;
        public AccountList()
        {
            db = new DBConnection();
        }
        public List<AccountClass> GetAccountClass(string Id_Account)
        {
            string sql;
            if (string.IsNullOrEmpty(Id_Account))
            {
                sql = "SELECT AccountManagement.Id_Account,AccountManagement.Account,AccountManagement.Password,AccountManagement.Id_Role,IIF(AccountRole.Name IS NULL,'Unkown',AccountRole.Name) AS Name FROM AccountManagement LEFT JOIN AccountRole on AccountManagement.Id_Role=AccountRole.Id_Role";
            }
            else
            {
                sql = "SELECT AccountManagement.Id_Account,AccountManagement.Account,AccountManagement.Password,AccountManagement.Id_Role,AccountRole.Name FROM AccountManagement LEFT JOIN AccountRole on AccountManagement.Id_Role=AccountRole.Id_Role WHERE Id_Account = " + Id_Account;
            }
            List<AccountClass> accList = new List<AccountClass>();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            AccountClass tmpAcc;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmpAcc = new AccountClass();
                tmpAcc.Id_Account = Convert.ToInt32(dt.Rows[i]["Id_Account"].ToString());
                tmpAcc.Account = dt.Rows[i]["Account"].ToString();
                tmpAcc.Password = dt.Rows[i]["Password"].ToString();
                tmpAcc.Role= dt.Rows[i]["Name"].ToString();
                tmpAcc.Id_Role=Convert.ToInt32(dt.Rows[i]["Id_Role"].ToString());
                accList.Add(tmpAcc);
            }
            return accList;
        }
        public void AddAccount(AccountClass acc)
        {
            string sql= "INSERT INTO AccountManagement(Account, Password, Id_Role) VALUES('"+acc.Account+"','"+acc.Password+"','"+acc.Role+"')";
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        
        public void UpdateAccount(AccountClass acc)
        {
            string sql = "UPDATE AccountManagement SET Account='" + acc.Account + "',Password='" + acc.Password + "',Id_Role='" + acc.Id_Role + "' WHERE Id_Account= " + acc.Id_Account;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteAccount(AccountClass acc)        
        {
            string sql = "DELETE AccountManagement WHERE Id_Account =" + acc.Id_Account;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}