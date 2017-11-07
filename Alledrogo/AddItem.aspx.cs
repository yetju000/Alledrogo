using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Amount.Visible = true;
        InitialPrice.Visible = false;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedIndex == 1)
        {
            Amount.Text = "1";
            Amount.Visible = false;

            InitialPrice.Visible = true;
        }
        else
        {
            Amount.Visible = true;
            InitialPrice.Visible = false;
        }
    }

    protected void CreateButton_Click(object sender, EventArgs e)
    {

        String email = Server.UrlDecode(Request.QueryString["email"]);

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();

        string IDuser = "select ID from users where email = '" + email + "'";
        SqlCommand com = new SqlCommand(IDuser, conn);
        String idUser = com.ExecuteScalar().ToString();

        string IDitem = "select max(ID) from items";
        com = new SqlCommand(IDitem, conn);
        int idItem = Convert.ToInt32(com.ExecuteScalar().ToString());

        string insertQueryItem = "insert into Items (Id,IdSeller,Title,Description,Image) values (@Id,@IdSeller,@Title,@Description,@Image)";
        com = new SqlCommand(insertQueryItem, conn);
        com.Parameters.AddWithValue("@Id", idItem +1);
        com.Parameters.AddWithValue("@IdSeller", idUser);
        com.Parameters.AddWithValue("@Title", Title.Text);
        com.Parameters.AddWithValue("@Description", Description.Text);
        com.Parameters.AddWithValue("@Image", "a");

        com.ExecuteNonQuery();

        DateTime thisDay = DateTime.Today;
        
        

        string insertQueryInProgress = "insert into InProgress (IdItem,ItemsLeft,Type,PriceForOne,ActualPrice,EndDate) values (@IdItem,@ItemsLeft,@Type,@PriceForOne,@ActualPrice,@EndDate)";
        com = new SqlCommand(insertQueryInProgress, conn);
        com.Parameters.AddWithValue("@IdItem", idItem +1);
        com.Parameters.AddWithValue("@ItemsLeft", Amount.Text);
        com.Parameters.AddWithValue("@Type", DropDownList1.SelectedValue[0]);
        com.Parameters.AddWithValue("@PriceForOne", PriceForOne.Text);
        com.Parameters.AddWithValue("@ActualPrice", InitialPrice.Text);
        com.Parameters.AddWithValue("@EndDate", (thisDay.AddDays(Convert.ToDouble(Dni.SelectedValue))).ToString("d"));

        com.ExecuteNonQuery();


        Response.Redirect("UserMainPage.aspx?Email=" + email);
        Response.Write("Przedmiot pomyślnie dodany");
        conn.Close();
    }
}