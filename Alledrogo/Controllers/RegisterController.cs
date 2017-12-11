using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseAccess;
using System.Web.Mvc;
using System.Threading;

namespace Alledrogo.Controllers
{
    
    public class RegisterController : ApiController
    {
        // [RequireHttps]
        // public IEnumerable<User> Get()
        // {
        //    using (DatabaseEntities entities = new DatabaseEntities())
        //    {
        //       return entities.Users.ToList();
        //   }
        // }
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (DatabaseEntities entities = new DatabaseEntities())
            {
              //  var entity = entities.Users.Where(e => e.Email == username).ToList();
                if (username!= "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.Users.Where(e => e.Email.Equals(username)).ToList());
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        
        public HttpResponseMessage Post([FromBody] User user)
        {
            try {
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    
                    user.Money = 0;
                    
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                
            }
        }
        [BasicAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " does not exists");
                    }
                    else
                    {
                        entities.Users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [BasicAuthentication]
        public HttpResponseMessage Put ([FromBody]User user)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            try
            {
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    // var entity = (entities.Users.Where(e => e.Email.Equals(username))).ToList();
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Email = " + username.ToString() + " does not exists");
                    }
                    else
                    { 
                        
                        
                        entity.Money = entity.Money + Convert.ToDouble((user.Money.ToString()));
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
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
