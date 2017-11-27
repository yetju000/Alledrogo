using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Sold : System.Web.UI.Page
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
           "SELECT I.Id, I.Image , I.Title ,B.Type ,(CASE WHEN B.NumberOfItems = 0 then 1 else B.NumberOfItems End) as NumberOfItems  , B.Price , B.Date FROM " +
                "Bought as B " +
                "LEFT JOIN Items as I ON I.id = B.Iditem " +
                "LEFT JOIN Users as U ON U.ID = I.IDSeller "+
                "WHERE U.ID like '" + UserID + "'";
    }
}