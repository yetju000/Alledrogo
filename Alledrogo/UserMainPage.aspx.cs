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
    String email;
    protected void Page_Load(object sender, EventArgs e)
    {
        email = Server.UrlDecode(Request.QueryString["email"]);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();

        String checkbalance = "select money from users where email = '" + email + "'";
        SqlCommand com = new SqlCommand(checkbalance, conn);

        Balance.Text = com.ExecuteScalar().ToString();


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

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void AddAuction_Click(object sender, EventArgs e)
    {
        
            String email = Server.UrlDecode(Request.QueryString["email"]);
                Response.Redirect("AddItem.aspx?Email=" + email);

            
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
            String email = Server.UrlDecode(Request.QueryString["email"]);
            Response.Redirect("Search.aspx?Email=" + email);
    }

    protected void BoughtButton_Click(object sender, EventArgs e)
    {
            String email = Server.UrlDecode(Request.QueryString["email"]);
            Response.Redirect("Bought.aspx?Email=" + email);
    }

    protected void SoldButton_Click(object sender, EventArgs e)
    {
        String email = Server.UrlDecode(Request.QueryString["email"]);
        Response.Redirect("Sold.aspx?Email=" + email);
    }

    protected void SoldingButton_Click(object sender, EventArgs e)
    {
        String email = Server.UrlDecode(Request.QueryString["email"]);
        Response.Redirect("Solding.aspx?Email=" + email);
    }

    protected void BidButton_Click(object sender, EventArgs e)
    {
        String email = Server.UrlDecode(Request.QueryString["email"]);
        Response.Redirect("Bidding.aspx?Email=" + email);
    }
}