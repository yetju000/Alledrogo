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

        
        [BasicAuthentication]
        [HttpPost]
        [Route("api/InProgress/{dni}")]
        public HttpResponseMessage Post([FromBody] InProgress inProgress, int dni)
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int item = entities.Items.Where(e => e.IdSeller == entity.Id).Max(u => u.Id);
                  
                    if (username != "")
                    {
                        if (inProgress.Type.Equals("Kup teraz") && ((Convert.ToInt32(inProgress.ItemsLeft)<1))|| (Convert.ToDouble(inProgress.PriceForOne) <= 0))
                        {
                     
                            entities.Items.Remove(entities.Items.Where(e=>e.Id.Equals(item)).FirstOrDefault());

                            throw new Exception("Złe dane");
                        }
                        if (inProgress.Type.Equals("Licytacja") && ((Convert.ToDouble(inProgress.ActualPrice) > (Convert.ToDouble(inProgress.PriceForOne))) || (Convert.ToDouble(inProgress.PriceForOne) <= 0)))
                        {
                            entities.Items.Remove(entities.Items.Where(e => e.Id.Equals(item)).FirstOrDefault());
                            throw new Exception("Złe dane");
                        }
                        inProgress.IdItem = item;
                        inProgress.ItemsLeft = Convert.ToInt32((inProgress.ItemsLeft).ToString()); 
                        inProgress.PriceForOne = Convert.ToDouble((inProgress.PriceForOne).ToString());
                        inProgress.ActualPrice = Convert.ToDouble((inProgress.ActualPrice).ToString());
                        inProgress.EndDate = DateTime.Now.AddDays(dni);

                           entities.InProgresses.Add(inProgress);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, inProgress);

                        return message;
                    }
                    else
                    {
                        entities.Items.Remove(entities.Items.Where(e => e.Id.Equals(item)).FirstOrDefault());
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
        [Route("api/InProgress/Solding")]
        public HttpResponseMessage Get()
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int id = entity.Id;
                    var response = from IP in entities.InProgresses
                                join I in entities.Items on IP.IdItem equals I.Id
                                join U in entities.Users on I.IdSeller equals U.Id
                                   where U.Id == id

                                select new { I, IP };

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
        [Route("api/InProgress/Licytation")]
        public HttpResponseMessage GetLicytation()
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                using (DatabaseEntities entities = new DatabaseEntities())
                {
                    
                    var entity = entities.Users.Where(e => e.Email.Equals(username)).FirstOrDefault();
                    int id = entity.Id;

                    var bought = (from B in entities.Boughts
                                         group new {B.Date,B.IdItem,B.IdSeller,B.NumberOfItems,B.Price,B.Type } by B.IdItem into uniqueIds
                                         select uniqueIds.FirstOrDefault()).Distinct();

                    var response = from IP in entities.InProgresses
                                   join B in bought on IP.IdItem equals B.IdItem
                                   join I in entities.Items on IP.IdItem equals I.Id
                                   join U in entities.Users on B.IdSeller equals U.Id
                                   where U.Id == id && IP.Type == "Licytacja"                          

                     select new { IP,B,I};

                    
                    var items = response.ToList();
                   // items.OrderBy(e=>e.B.IdItem);
                  //  items.Max(e => e.IP.ActualPrice);
                    
                    
                    
                   // item.GroupBy(e => e.B.IdItem);
                   // item.Max(e => e.B.Price);
                   // item.ToList();
                    if (username != "")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, items);
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

