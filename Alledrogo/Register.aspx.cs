using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();
        string checkuser = "select count(*) from Users where Email= '" + Email.Text + "'";
        SqlCommand com = new SqlCommand(checkuser, conn);
        int tempEmail = Convert.ToInt32(com.ExecuteScalar().ToString());
        if (tempEmail == 1)
        {
            Response.Write("Użytkownik o takim emailu juz istnieje.");
        }
        else {
            try
            {

                string BiggestID = "select MAX(Id) from Users";
                SqlCommand id = new SqlCommand(BiggestID, conn);
                int temp = Convert.ToInt32(id.ExecuteScalar().ToString());

                string insertQuery = "insert into Users (Id,Name,Surname,Email,Password,Money) values (@Id,@Name,@Surname,@Email,@Password,@Money)";
                com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@Id", temp + 1);
                com.Parameters.AddWithValue("@Name", Name.Text);
                com.Parameters.AddWithValue("@Surname", Surname.Text);
                com.Parameters.AddWithValue("@Email", Email.Text);
                com.Parameters.AddWithValue("@Password", Password.Text);
                com.Parameters.AddWithValue("@Money", 0);

                com.ExecuteNonQuery();
                Response.Redirect("Login.aspx");
                Response.Write("Zarejestrowano pomyślnie");


            }
            catch (Exception exce)
            {
                Response.Write(exce.ToString());
            }
        }
        conn.Close();
    }

    
}