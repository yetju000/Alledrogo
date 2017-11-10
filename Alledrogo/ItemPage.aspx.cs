﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;

public partial class ItemPage : System.Web.UI.Page
{
    String type;
    String ItemId;
    String ItemsLeft;
    String PriceForOne;
    String ActualPrice;
    String UserID;
    String userIDCheckItem;
    String BuyDate;
    String Email;
    String Money;


    protected void Page_Load(object sender, EventArgs e)
    {
        Email = Server.UrlDecode(Request.QueryString["email"]);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
        conn.Open();

        String checkuserID = "select ID from users where email = '" + Email + "'";
        SqlCommand com = new SqlCommand(checkuserID, conn);

        String temp = com.ExecuteScalar().ToString();
        UserID = temp;
        

        String checkmoney = "select money from users where email = '" + Email + "'";
        com = new SqlCommand(checkmoney, conn);

        temp = com.ExecuteScalar().ToString();
        Money = temp;

        

        ItemId = Server.UrlDecode(Request.QueryString["Row"]);
        type = Server.UrlDecode(Request.QueryString["Type"]);

        String UcheckuserID = "select IDSeller from Items where id = '" + ItemId + "'";
        com = new SqlCommand(UcheckuserID, conn);
        userIDCheckItem = com.ExecuteScalar().ToString();
        conn.Close();
        SqlDataSource1.SelectCommand =
            "select I.ID, I.Title as Tytuł ,I.Description as Opis,IP.Type as 'Typ Aukcji' , IP.ItemsLeft as 'Ilość' , IP.PriceForOne as 'Cena za sztuke' , IP.ActualPrice as 'Cena Aktualna', U.Name +' '+U.surname as Sprzedawca , IP.EndDate as 'Data zakończenia aukcji' " +
            "from InProgress as IP " +
            "LEFT        JOIN Items AS I ON IP.IdItem = I.Id " +
            "LEFT        JOIN Users AS U ON I.IDSeller = U.ID " +
            "WHERE I.id LIKE '" + ItemId + "'";

        GridView1.SelectRow(0);
        GridViewRow row = GridView1.SelectedRow;
        //cena za sztuke = 5
        ItemsLeft = row.Cells[4].Text;
        PriceForOne = row.Cells[5].Text;
        ActualPrice = row.Cells[6].Text;


        if (type[0] == 'K') {
            Label1.Text = "Ilość sztuk:";
            Button1.Text = "Kup";
            
            
        }
        else {
            Label1.Text = "Kwota Licytacji:";
            Button1.Text = "Licytuj";

        }
           
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        
        int TextBoxText = Convert.ToInt32(TextBox1.Text);
        double TextBoxText2 = Convert.ToDouble(TextBox1.Text);

        if (TextBox1.Text != "" && type[0] == 'K' && TextBoxText <= Convert.ToInt32(ItemsLeft) && TextBoxText > 0) {

            Label2.Text = "Cena całkowita: " + Convert.ToDouble(TextBoxText) * Convert.ToDouble(PriceForOne);
            Button1.Visible = false;
            TextBox1.Enabled = false; //
            Button2.Visible = true;
            Button3.Visible = true;
            Label2.Visible = true;
            // TextBox1.Text = Convert.ToString(FormView1.DataItemCount);

        }
        else if (TextBox1.Text != "" && type[0] == 'L' && TextBoxText2 > Convert.ToDouble(ActualPrice) && TextBoxText2 > 0)
        {
            Label2.Text = "Kwota do zalicytowania: " + Convert.ToString(TextBoxText2);
            Button1.Visible = false;
            TextBox1.Enabled = false;
            Button2.Visible = true;
            Button3.Visible = true;
            Label2.Visible = true;
        }
        
        else 
        {
            Response.Write("Za dużo przedmiotów badź niepoprawne dane");
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        Button1.Visible = true;
        TextBox1.Enabled = true;
        Button2.Visible = false;
        Button3.Visible = false;
        Label2.Visible = false;
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime thisDay = DateTime.Today;
        
        if (type[0] == 'K')
        {
            if(Convert.ToInt32(TextBox1.Text) == Convert.ToInt32(ItemsLeft))
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
                conn.Open();
                string insertQuery = "insert into Bought (IdItem,IdSeller,NumberOfItems,Type,Price,Date) values (@IdItem,@IdSeller,@NumberOfItems,@Type,@Price,@Date)";
                SqlCommand com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@IdItem", ItemId);
                com.Parameters.AddWithValue("@IdSeller", UserID);
                com.Parameters.AddWithValue("@NumberOfItems", TextBox1.Text);
                com.Parameters.AddWithValue("@Type", type);
                com.Parameters.AddWithValue("@Price", Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne));
                com.Parameters.AddWithValue("@Date", thisDay.ToString("d"));

                com.ExecuteNonQuery();

                string updateQuery = "Update InProgress SET Itemsleft =Itemsleft -" + TextBox1.Text + " where IdItem =" + ItemId;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                updateQuery = "Update Users SET Money=Money-" + Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne) + " where Id =" + UserID;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                string deleteQuery = "Delete from InProgress where IdItem =" + ItemId;
                com = new SqlCommand(deleteQuery, conn);
                com.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("Login.aspx");
            }
            
            else if (Convert.ToDouble(Money) < (Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne)))
            {
                Response.Write("Za mało środków na koncie");
            } 
            else if (userIDCheckItem.Equals(UserID))
            {
                Response.Write("Nie można kupic przedmiotu od siebie");
            }
            else
            {
                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
                conn.Open();
                string insertQuery = "insert into Bought (IdItem,IdSeller,NumberOfItems,Type,Price,Date) values (@IdItem,@IdSeller,@NumberOfItems,@Type,@Price,@Date)";
                SqlCommand com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@IdItem", ItemId);
                com.Parameters.AddWithValue("@IdSeller",UserID);
                com.Parameters.AddWithValue("@NumberOfItems",TextBox1.Text);
                com.Parameters.AddWithValue("@Type", type);
                com.Parameters.AddWithValue("@Price", Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne));
                com.Parameters.AddWithValue("@Date", thisDay.ToString("d"));

                com.ExecuteNonQuery();

                string updateQuery = "Update InProgress SET Itemsleft =Itemsleft -"+TextBox1.Text+" where IdItem ="+ItemId;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                updateQuery = "Update Users SET Money=Money-"+ Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne) + " where Id =" + UserID;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("Login.aspx");
            }
        }
        if (type[0] == 'L')
        {
            if (Convert.ToDouble(Money) < Convert.ToDouble(ActualPrice))
            {
                Response.Write("Za mało środków na koncie badź za mała kwota by przebić");
            }
            else
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegisterInput"].ConnectionString);
                conn.Open();
                SqlCommand com;

                string updateQuery = "Update InProgress SET ActualPrice =" + TextBox1.Text + " where IdItem =" + ItemId;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                updateQuery = "Update Users SET Money=Money-" + Convert.ToDouble(TextBox1.Text) * Convert.ToDouble(PriceForOne) + " where Id =" + UserID;
                com = new SqlCommand(updateQuery, conn);
                com.ExecuteNonQuery();

                string insertQuery = "insert into Bought (IdItem,IdSeller,NumberOfItems,Type,Price,Date) values (@IdItem,@IdSeller,@NumberOfItems,@Type,@Price,@Date)";
                com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@IdItem", ItemId);
                com.Parameters.AddWithValue("@IdSeller", UserID);
                com.Parameters.AddWithValue("@NumberOfItems", "0");
                com.Parameters.AddWithValue("@Type", type);
                com.Parameters.AddWithValue("@Price", TextBox1.Text);
                com.Parameters.AddWithValue("@Date", thisDay.ToString("d"));

                com.ExecuteNonQuery();

                conn.Close();
                Response.Redirect("Login.aspx");
            }
        }
    }
}