using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class UserMainPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String email = Server.UrlDecode(Request.QueryString["email"]);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();

        string checkbalance = "select money from users where email = '" + email + "'";
        SqlCommand com = new SqlCommand(checkbalance, conn);

        String temp = com.ExecuteScalar().ToString();
        Balance.Text = temp;


        conn.Close();
    }

    protected void Balance_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void BuyCoinsButton_Click(object sender, EventArgs e)
    {
        try
        {
            Double Amount = Convert.ToDouble(AmountToCharge.Text);
            String email = Server.UrlDecode(Request.QueryString["email"]);
            if (Amount > 0)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
                conn.Open();


                String charge = "Update Users SET money = money + "+AmountToCharge.Text+" where email ='"+email+"'";
                SqlCommand com = new SqlCommand(charge, conn);
                com.ExecuteNonQuery();
                  Response.Redirect("UserMainPage.aspx?Email=" + email);
                
                Response.Write("Kwota została pomyślnie dodana");
                conn.Close();
            }
            else
            {
                Response.Write("Niepoprawna kwota");
            }
        }
        catch (Exception exc)
        {
            Response.Write("Niepoprawna kwota");
        }
    }
}