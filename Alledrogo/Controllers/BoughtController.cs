using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Web.Http;
using System.Text;
using System.Data.SqlTypes;

namespace Alledrogo.Controllers
{
    public class BoughtController : ApiController
    {
        

        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Bought bought,int id)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    var IP = entities.InProgresses.Where(e => e.IdItem == id).FirstOrDefault();
                    var item = entities.Items.Where(e => e.Id == id).FirstOrDefault();
                    var userWhoSold = entities.Users.Where(e => e.Id == item.IdSeller).FirstOrDefault();

                   
                    DateTime date = DateTime.Now;
                    bought.Date = date;
                    bought.IdItem = IP.IdItem;
                    bought.IdSeller = entity.Id;
                    bought.Type = IP.Type;
                    bought.NumberOfItems = Convert.ToInt32(bought.NumberOfItems);
                    bought.Price = Convert.ToDouble(bought.Price);
                    entity.Money = entity.Money - bought.Price;
                       entities.Boughts.Add(bought);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, bought);

                    try { 
                        System.Net.Mail.SmtpClient client = new SmtpClient();
                       // client.UseDefaultCredentials = false;
                       // client.Credentials new System.Net.NetworkCredential("yetju000", "uughns56", "yetju000-001-site1.htempurl.com");
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("ProjektAlledrogo@gmail.com", "wchujdrogo");

                        MailMessage mm = new MailMessage("donotreply@domain.com", entity.Email, "Alledrogo - Zakupione przedmioty",
                         "Kupiłeś przedmiot[y] '" + item.Title + "'. Id aukcji:" + item.Id + "! Sztuk:" + bought.NumberOfItems + " Cena : " + bought.Price);
                        mm.BodyEncoding = UTF8Encoding.UTF8;
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                        client.Send(mm);


                        client = new SmtpClient();
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("ProjektAlledrogo@gmail.com", "wchujdrogo");

                        mm = new MailMessage("donotreply@domain.com", userWhoSold.Email, "Alledrogo - Sprzedane przedmioty",
                         "Sprzedałeś przedmiot[y] '" + item.Title + "'. Id aukcji:" + item.Id + "! Sztuk:" + bought.NumberOfItems + " Cena : " + bought.Price);
                        mm.BodyEncoding = UTF8Encoding.UTF8;
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                        client.Send(mm);
                    }
                    catch (Exception exx)
                    {
                        
                    }

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        [BasicAuthentication]
        [HttpGet]
        [Route("api/Bought/Sold")]
        public HttpResponseMessage Get()
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int id = entity.Id;
                    var response = from B in entities.Boughts
                                   join I in entities.Items on B.IdItem equals I.Id
                                   join U in entities.Users on I.IdSeller equals U.Id
                                   where U.Id == id

                                   select new { I, B };

                    var item = response.ToList();
                    if (username != "")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, item);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        [BasicAuthentication]
        [HttpGet]
        [Route("api/Bought/Bought")]
        public HttpResponseMessage GetBought()
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int id = entity.Id;
                    var response = from B in entities.Boughts
                                   join I in entities.Items on B.IdItem equals I.Id
                                   join U in entities.Users on B.IdSeller equals U.Id
                                   where U.Id == id && B.NumberOfItems>0

                                   select new { I, B };

                    var item = response.ToList();
                    if (username != "")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, item);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}
