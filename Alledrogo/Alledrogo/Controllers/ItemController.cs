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
        
        [HttpGet]
        public HttpResponseMessage Get(string exp)
        {

            using (DatabaseEntities entities = new DatabaseEntities())
            {

                // var list = entities.Items.Where(e => e.Title.Contains(exp)).ToList();
                var list = entities.Items.SqlQuery("select I.Id, I.Description, I.IdSeller, I.Title , I.Image  from items as I RIGHT JOIN InProgress as IP on IP.IdItem = I.Id WHERE GETDATE() < IP.EndDate AND I.Title like '%" + exp + "%'").ToList();
                if (list != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        [Route("api/ItemList/Item/{id}")]
        public HttpResponseMessage Get(int id)
        {

            using (DatabaseEntities entities = new DatabaseEntities())
            {


                var query = from I in entities.Items
                            join IP in entities.InProgresses on I.Id equals IP.IdItem
                            where I.Id == id
                            select new { I, IP };

                var item = query.ToList();


                if (item != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

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
        public HttpResponseMessage Post([FromBody]Item item,int dni)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    if (username != "")
                    {
                        if (item.Title == null || item.Description == null) { throw new Exception("Uzupełnij dane"); }
                        var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                        item.IdSeller = entity.Id;
                        
                        entities.Items.Add(item);
                        entities.SaveChanges();
                 
                       
                        

                       
                        
                        var message = Request.CreateResponse(HttpStatusCode.Created, item);
                            message.Headers.Location = new Uri(Request.RequestUri + item.Id.ToString());
                             return message;
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
        [HttpDelete]
        [Route("api/Item/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    var item = entities.InProgresses.Where(e => e.IdItem == id).FirstOrDefault();
                    
                    if (username != "")
                    {
                       
                        entities.InProgresses.Remove(item);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,"DELETED");
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
        [HttpPut]
        [Route("api/Item/{id}")]
        public HttpResponseMessage Put([FromBody]InProgress inProgress,int id)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                   
                    if (username != "")
                    {
                        var Item = entities.InProgresses.Where(e => e.IdItem == id).FirstOrDefault();
                        Item.ItemsLeft = Item.ItemsLeft - Convert.ToInt32(inProgress.ItemsLeft);
                        Item.ActualPrice = Convert.ToDouble(inProgress.ActualPrice);
                        
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "UPDATED");
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
        [HttpPut]
        [Route("api/Item/lic/{id}")]
        public HttpResponseMessage Putlic([FromBody]InProgress inProgress, int id)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    
                    if (username != "")
                    {
                        var Item = entities.InProgresses.Where(e => e.IdItem == id).FirstOrDefault();
                        Item.ActualPrice = inProgress.ActualPrice;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "UPDATED");
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
