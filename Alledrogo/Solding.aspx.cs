using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Solding : System.Web.UI.Page
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
           "SELECT I.Id, I.Image , I.Title, IP.ItemsLeft , IP.Type , IP.PriceForOne , IP.ActualPrice , IP.EndDate  FROM "+
                "InProgress as IP "+
                "LEFT JOIN Items as I ON I.id = IP.IdItem "+
                "LEFT JOIN Users as U ON U.ID = I.IdSeller "+
                "WHERE U.ID like '" + UserID + "' AND IP.EndDate >= GETDATE()";
    }
}
