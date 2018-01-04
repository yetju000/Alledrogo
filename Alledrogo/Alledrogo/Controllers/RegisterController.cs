using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseAccess;
using System.Web.Mvc;
using System.Threading;
using System.Text.RegularExpressions;

namespace Alledrogo.Controllers
{
    
    public class RegisterController : ApiController
    {
        

        [BasicAuthentication]
        public HttpResponseMessage Get()
        {

            string username = Thread.CurrentPrincipal.Identity.Name;
            using (DatabaseEntities entities = new DatabaseEntities())
            {
                entities.Database.Connection.Open();
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
                 //   entities.Database.Connection.Open();
                    Regex rgx = new Regex("[A-Za-z0-9]+[@]{1}[A-Za-z0-9]+([.][A-Za-z0-9]+)+$");
                    user.Money = 0;
                    if (user.Name == null || user.Password == null || user.Surname == null || user.Email == null) { throw new Exception("Uzupełnij dane"); }
                    if (!rgx.IsMatch(user.Email)) { throw new Exception("Niepoprawny Email"); }
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
                    if (user.Money < 0) { throw new FormatException(""); }
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cos poszło nie tak");
                    }
                    else
                    {
                        
                        
                        entity.Money = entity.Money + Convert.ToDouble(Convert.ToInt32(user.Money));
                        
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            }
            catch (FormatException Excs)
            {
                Console.Write(Excs);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Niepoprawna kwota");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
