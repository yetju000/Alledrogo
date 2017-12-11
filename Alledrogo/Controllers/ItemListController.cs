using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alledrogo.Controllers
{
    public class ItemListController : ApiController
    {

        [HttpGet]
        [Route("api/ItemList/Item/{id}")]
        public HttpResponseMessage Get(int id)
        {

            using (DatabaseEntities entities = new DatabaseEntities())
            {

                // var item = entities.Items.SqlQuery("SELECT * from Items as I RIGHT JOIN InProgress as IP ON IP.IdItem = I.Id WHERE I.Id ="+id).FirstOrDefault();
                // var inProgres = entities.InProgresses.SqlQuery("SELECT * from InProgress where IdItem ="+id).FirstOrDefault();

                var query = from I in entities.Items
                            join IP in entities.InProgresses on I.Id equals IP.IdItem
                            where I.Id == id
                            select new { I, IP };

                var item = query.ToList();


                if (item != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, item );
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }


        [HttpGet]
        public HttpResponseMessage Get(string exp)
        {
            
            using (DatabaseEntities entities = new DatabaseEntities())
            {

                // var list = entities.Items.Where(e => e.Title.Contains(exp)).ToList();
                var list = entities.Items.SqlQuery("select I.Id, I.Description, I.IdSeller, I.Title , I.Image  from items as I RIGHT JOIN InProgress as IP on IP.IdItem = I.Id WHERE GETDATE() < IP.EndDate AND I.Title like '%"+exp+"%'").ToList();
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
    }
}
