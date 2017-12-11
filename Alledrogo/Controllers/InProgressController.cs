using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Alledrogo.Controllers
{
    public class InProgressController : ApiController
    {
      /*  [BasicAuthentication]
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
        */

        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] InProgress inProgress)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int item = entities.Items.Where(e => e.IdSeller == entity.Id).Max(u => u.Id);
                    
                    
                  //  var response = entities.Items.Where(e => e.Id == item).FirstOrDefault();
                    if (username != "")
                    {

                        inProgress.IdItem = item;
                        inProgress.ItemsLeft = Convert.ToInt32((inProgress.ItemsLeft).ToString()); 
                        inProgress.PriceForOne = Convert.ToDouble((inProgress.PriceForOne).ToString());
                        inProgress.ActualPrice = Convert.ToDouble((inProgress.ActualPrice).ToString());
                        inProgress.EndDate = DateTime.Now;
                        //     var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                        //   item.IdSeller = entity.Id;
                           entities.InProgresses.Add(inProgress);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, inProgress);
                     //   message.Headers.Location = new Uri(Request.RequestUri + item.Id.ToString());
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

