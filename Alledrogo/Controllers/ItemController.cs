using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseAccess;

using System.Threading;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Alledrogo.Controllers
{
    public class ItemController : ApiController
    {
        
        
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (DatabaseEntities entities = new DatabaseEntities())
            {
                var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                int item = entities.Items.Where(e => e.IdSeller == entity.Id).Max(u => u.Id);
                var response = entities.Items.Where(e => e.Id == item).FirstOrDefault();
                if (username != "")
                {

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

    
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Item item,int dni)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    if (username != "")
                    {
                        var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                        item.IdSeller = entity.Id;
                        entities.Items.Add(item);
                        int maxID = entities.InProgresses.Max(e => e.IdItem);
                        DateTime date = DateTime.Now;
                        dni = 1;
                        var adddate = entities.InProgresses.Where(e => e.IdItem == maxID).FirstOrDefault();
                        adddate.EndDate = date.AddDays(dni);
                        entities.SaveChanges();
                        

                        entities.SaveChanges();
                        
                        var message = Request.CreateResponse(HttpStatusCode.Created, item);
                            message.Headers.Location = new Uri(Request.RequestUri + item.Id.ToString());
                             return message;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
               //     user.Money = 0;

                //    entities.Users.Add(user);
               //     entities.SaveChanges();

                //    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                //    message.Headers.Location = new Uri(Request.RequestUri + user.Id.ToString());
               //     return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
    }
}
