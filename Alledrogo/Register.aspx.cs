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
        if (IsPostBack)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
            conn.Open();

            string checkuser = "select count(*) from Users where Email= '" + Email.Text + "'";
            SqlCommand com = new SqlCommand(checkuser, conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (temp == 1)
            {
                Response.Write("Użytkownik o takim emailu juz istnieje.");
            }

            conn.Close();
        }
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        try {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
            conn.Open();

            string insertQuery = "insert into Users (Id,Name,Surname,Email,Password,Money) values (@Id,@Name,@Surname,@Email,@Password,@Money)";
            SqlCommand com = new SqlCommand(insertQuery, conn);
            com.Parameters.AddWithValue("@Id", 3);
            com.Parameters.AddWithValue("@Name", Name.Text);
            com.Parameters.AddWithValue("@Surname", Surname.Text);
            com.Parameters.AddWithValue("@Email", Email.Text);
            com.Parameters.AddWithValue("@Password", Password.Text);
            com.Parameters.AddWithValue("@Money", 0);
            
            com.ExecuteNonQuery();
            Response.Redirect("Login.aspx");
            Response.Write("Zarejestrowano pomyślnie");

            conn.Close();
        }
        catch (Exception exce)
        {
            Response.Write(exce.ToString());
        }
    }

    
}