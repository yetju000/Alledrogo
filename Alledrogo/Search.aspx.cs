using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        SqlDataSource1.SelectCommand = 
            "select I.ID, I.Title as Tytuł ,IP.Type as 'Typ Aukcji' , IP.ItemsLeft as 'Ilość przedmiotów' , IP.PriceForOne as 'Cena za sztuke' , IP.ActualPrice as 'Cena Aktualna', U.Name +' '+U.surname as Sprzedawca , IP.EndDate as 'Data zakończenia aukcji' "+
            "from InProgress as IP "+
            "LEFT        JOIN Items AS I ON IP.IdItem = I.Id "+
            "LEFT        JOIN Users AS U ON I.IDSeller = U.ID "+
            "WHERE I.Title like concat('%','"+SearchText.Text+"','%')";

        
        

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String email = Server.UrlDecode(Request.QueryString["email"]);
        GridViewRow row = GridView1.SelectedRow; 
        Response.Redirect("ItemPage.aspx?Email="+email+"&Row="+ row.Cells[1].Text+"&Type="+row.Cells[3].Text);
    }
}