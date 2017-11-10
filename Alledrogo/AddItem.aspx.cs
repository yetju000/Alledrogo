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
    String email;
    protected void Page_Load(object sender, EventArgs e)
    {
        email = Server.UrlDecode(Request.QueryString["email"]);
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedIndex == 0)
        {
            Label1.Text = "Ilość:";
            Label2.Text = "Cena za sztuke:";
        }
        else
        {
            Label1.Text = "Cena początkowa:";
            Label2.Text = "(opcjonalnie) Cena wykupu:";

        }
    }

    protected void CreateButton_Click(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedValue[0] == 'K')
        {
            double check;
            int check2;
            if (Title.Text == "")
            {
                Response.Write("Proszę podać tytuł aukcji gdyż ułatwia to wyszukiwanie aukcji potencjalnym kupcom");
            }
            else if (Description.Text == "")
            {
                Response.Write("Pole opisu jest obowiązkowe");
            }
            else if (int.TryParse(TextBox1.Text, out check2) == false)
            {
                Response.Write("Proszę podać poprawną ilość przedmiotów");
            }
            else if (Double.TryParse(TextBox2.Text, out check) == false)
            {
                Response.Write("Proszę podać poprawną cenę");
            }

            else {
                TextBox2.Text = Convert.ToString(Math.Round(Convert.ToDouble(TextBox2.Text), 2));

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
                com.Parameters.AddWithValue("@Id", idItem + 1);
                com.Parameters.AddWithValue("@IdSeller", idUser);
                com.Parameters.AddWithValue("@Title", Title.Text);
                com.Parameters.AddWithValue("@Description", Description.Text);
                com.Parameters.AddWithValue("@Image", "a");

                com.ExecuteNonQuery();


                string insertQueryInProgress = "insert into InProgress (IdItem,ItemsLeft,Type,PriceForOne,ActualPrice,EndDate) values (@IdItem,@ItemsLeft,@Type,@PriceForOne,@ActualPrice,@EndDate)";
                com = new SqlCommand(insertQueryInProgress, conn);
                com.Parameters.AddWithValue("@IdItem", idItem + 1);
                com.Parameters.AddWithValue("@ItemsLeft", TextBox1.Text);
                com.Parameters.AddWithValue("@Type", DropDownList1.SelectedValue);
                com.Parameters.AddWithValue("@PriceForOne", Convert.ToDouble(TextBox2.Text));
                com.Parameters.AddWithValue("@ActualPrice", "0");
                com.Parameters.AddWithValue("@EndDate", "2000-03-03");

                com.ExecuteNonQuery();

                string DateUpdate = "Update InProgress set EndDate = (GetDate()+" + (Dni.SelectedValue) + ") where IdItem like '" + (idItem + 1) + "'";
                com = new SqlCommand(DateUpdate, conn);

                com.ExecuteNonQuery();

                Response.Redirect("UserMainPage.aspx?Email=" + email);
                Response.Write("Przedmiot pomyślnie dodany");
                conn.Close();
            }
        }
        else
        {

            double check;
            
            if (Title.Text == "")
            {
                Response.Write("Proszę podać tytuł aukcji gdyż ułatwia to wyszukiwanie aukcji potencjalnym kupcom");
            }
            else if (Description.Text == "")
            {
                Response.Write("Pole opisu jest obowiązkowe");
            }
            else if (Double.TryParse(TextBox1.Text, out check) == false)
            {
                Response.Write("Proszę podać poprawną cenę początku licytacji");
            }
            else if ((Double.TryParse(TextBox2.Text, out check) == false)&&(TextBox2.Text != ""))
            {
                Response.Write("Proszę podać poprawną cenę lub pozostawić puste");
            }
            else if ((TextBox2.Text != "") && (Convert.ToDouble(TextBox1.Text)> (Convert.ToDouble(TextBox2.Text)))) {
                Response.Write("Cena wykupu nie może być mniejsza niż cena minimalna");
            }
            else {
                if (TextBox2.Text != "")
                TextBox2.Text = Convert.ToString(Math.Round(Convert.ToDouble(TextBox2.Text), 2));
                TextBox1.Text = Convert.ToString(Math.Round(Convert.ToDouble(TextBox1.Text), 2));

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
            com.Parameters.AddWithValue("@Id", idItem + 1);
            com.Parameters.AddWithValue("@IdSeller", idUser);
            com.Parameters.AddWithValue("@Title", Title.Text);
            com.Parameters.AddWithValue("@Description", Description.Text);
            com.Parameters.AddWithValue("@Image", "a");

            com.ExecuteNonQuery();


            string insertQueryInProgress = "insert into InProgress (IdItem,ItemsLeft,Type,PriceForOne,ActualPrice,EndDate) values (@IdItem,@ItemsLeft,@Type,@PriceForOne,@ActualPrice,@EndDate)";
            com = new SqlCommand(insertQueryInProgress, conn);
            com.Parameters.AddWithValue("@IdItem", idItem + 1);
            com.Parameters.AddWithValue("@ItemsLeft", "0");
            com.Parameters.AddWithValue("@Type", DropDownList1.SelectedValue);
            com.Parameters.AddWithValue("@PriceForOne", TextBox2.Text);
            com.Parameters.AddWithValue("@ActualPrice", TextBox1.Text);
            com.Parameters.AddWithValue("@EndDate", "2000-12-12");

            com.ExecuteNonQuery();

                string DateUpdate = "Update InProgress set EndDate = (GetDate()+"+(Dni.SelectedValue)+") where IdItem like '" + (idItem + 1) + "'";
                com = new SqlCommand(DateUpdate, conn);

                com.ExecuteNonQuery();


                Response.Redirect("UserMainPage.aspx?Email=" + email);
            Response.Write("Przedmiot pomyślnie dodany");
            conn.Close();
        }
    }
    }
}