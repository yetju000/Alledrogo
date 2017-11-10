using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Bidding : System.Web.UI.Page
{
    String email;
    String UserID;

    protected void Page_Load(object sender, EventArgs e)
    {

        email = Server.UrlDecode(Request.QueryString["email"]);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();

        String checkuserID = "select ID from users where email = '" + email + "'";
        SqlCommand com = new SqlCommand(checkuserID, conn);

        String temp = com.ExecuteScalar().ToString();
        UserID = temp;
        conn.Close();

        SqlDataSource1.SelectCommand =
           "SELECT DISTINCT I.Id, I.Image , I.Title ,B.Type , IP.ActualPrice , B.Date FROM "+
                "Bought as B "+
                "LEFT JOIN Items as I ON I.id = B.Iditem "+
                "LEFT JOIN InProgress as IP ON IP.IdItem = I.ID "+
                "WHERE B.IDSeller like '" + UserID + "' AND B.NumberOfItems = '0'";
    }
}