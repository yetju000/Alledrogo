using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }



    protected void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
            conn.Open();

            string password = "select password from users where email = '" + Email.Text + "'";
            SqlCommand com = new SqlCommand(password, conn);

            String temp = com.ExecuteScalar().ToString();
            if (temp.Equals(Password.Text))
            {
                Response.Redirect("UserMainPage.aspx?Email=" + Email.Text);
                Response.Redirect("UserMainPage.aspx");
            //    Response.Write("Zalogowano pomyślnie.");
            }
            

            conn.Close();
        }
        catch (Exception exce)
        {
            Response.Write("Niepoprawne dane logowania");
        }
    }
}