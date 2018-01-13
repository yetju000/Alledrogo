using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Data.SqlTypes;

namespace DatabaseTaks
{
    public partial class Form1 : Form
    {

        void timer_Tick(object sender, EventArgs e)
        {
            int currentSecond = Convert.ToInt32(DateTime.Now.ToString("ss"));
            if (currentSecond == 0)
            {

                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Adrian\Desktop\ALLEDROGO\Alledrogo\Alledrogo\App_Data\Database.mdf;Integrated Security=True");
                conn.Open();

                string DeleteInProgress = "DELETE FROM InProgress where EndDate < GetDATE() AND NOT type = 'Licytacja'";
                SqlCommand com = new SqlCommand(DeleteInProgress, conn);

                com.ExecuteNonQuery();

                List<int> IdItem = new List<int>();
                List<string> Email = new List<string>();
                List<double> Price = new List<double>();
                List<string> Title = new List<string>();

                DeleteInProgress = "SELECT DISTINCT IP.IDItem ,IP.ActualPrice, I.Title ,U.Email  from "+
                "InProgress as IP "+
                "LEFT JOIN Items as I ON I.ID = IP.IdItem "+
                "LEFT JOIN Bought as B ON B.IdItem = I.Id "+
                "LEFT JOIN Users as U ON B.IDSeller = U.ID "+
                "WHERE EndDate < GetDATE() AND IP.type LIKE 'LICYTACJA'";



                com = new SqlCommand(DeleteInProgress, conn);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int item = reader.GetInt32(reader.GetOrdinal("IdItem"));
                        IdItem.Add(item);
                        try {
                            
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string title = reader.GetString(reader.GetOrdinal("Title"));
                            double price = reader.GetDouble(reader.GetOrdinal("ActualPrice"));
                            Email.Add(email);
                            Price.Add(price);
                            Title.Add(title);
                        }
                        catch(SqlNullValueException exc)
                        {
                            string email = "NULL";
                            Email.Add(email);
                            double price = 0;
                            Price.Add(price);
                            string title = "NULL";
                            Title.Add(title);
                        }
                    }
                    


                }
                reader.Close();
                int ilosc = IdItem.Count;



                for (int i = 0; i < ilosc; i++)
                {
                    if ((ilosc>0) && (!Email[i].Equals("NULL"))) { 
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("ProjektAlledrogo@gmail.com", "wchujdrogo");

                    //MailMessage mm = new MailMessage("donotreply@domain.com", Email[i], "Licytacja zakończona", "Wygrałeś aukcje '"+Title[i]+"'. Id aukcji:"+IdItem[i]+"! Ostateczna cena:"+ Price[i]+". Gratulacje!");
                   // mm.BodyEncoding = UTF8Encoding.UTF8;
                   // mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                  //  client.Send(mm);
                     }
                    }
                if (ilosc > 0)
                {
                    DeleteInProgress = "DELETE from InProgress where ";
                    for (int i = 0; i < ilosc; i++)
                    {
                        DeleteInProgress = DeleteInProgress + "IDItem =" + IdItem[i] ;
                        if ((i + 1) != ilosc)
                            DeleteInProgress = DeleteInProgress + " AND ";
                    }
                    com = new SqlCommand(DeleteInProgress, conn);
                    textBox1.Text = textBox1.Text +  ilosc;
                    com.ExecuteNonQuery();
                }

                textBox1.Text = textBox1.Text + "Wyczyszczono InProgress " + DateTime.Now.ToString("HH:mm:ss") + " " + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                conn.Close();

                IdItem = null;
                 Email = null;
               Price = null;
                Title = null;
                ilosc = 0;
            }
        }
        public Form1()
        {
            InitializeComponent();
           
            
            

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();


            t.Interval = 900; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            
            t.Start();

            
                        


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
