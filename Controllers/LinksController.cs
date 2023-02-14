using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TokenAPI;

namespace TokenAPI.Controllers
{
    public class LinksController : ApiController
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: api/Links
        //public IQueryable<Link> GetLinks()
        //{
        //    return db.Links;
        //}
        // [Authorize]
        public IHttpActionResult GetLinks(int CategoryId)
        {

            var link = db.Links
                      .Where(a => a.Id_Category == CategoryId).ToList().Distinct()
                      .Select(x => new { x.Id_Subject, x.Id_Link, x.Pdf_Name, x.Link1 });

            return Ok(link);
            //  return db.Links;
        }   
        //  [Authorize]
        // GET: api/Links/5
        [ResponseType(typeof(Link))]
        public IHttpActionResult GetLink(int id)
        {
            //Link link = db.Links.Find(id);
            //if (link == null)
            //{
            //    return NotFound();
            //}

            //return Ok(link);
            var link = db.Links
                     .Select(x => new { x.Id_Subject, x.Id_Link, x.Pdf_Name, x.Link1 })
                     .Where(x => x.Id_Link == id);
            return Ok(link);
        }

        // PUT: api/Links/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLink(int id, Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != link.Id_Link)
            {
                return BadRequest();
            }

            db.Entry(link).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Links
        [ResponseType(typeof(Link))]
        public IHttpActionResult PostLink(Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Links.Add(link);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = link.Id_Link }, link);
        }

        // DELETE: api/Links/5
        [ResponseType(typeof(Link))]
        public IHttpActionResult DeleteLink(int id)
        {
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return NotFound();
            }

            db.Links.Remove(link);
            db.SaveChanges();

            return Ok(link);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinkExists(int id)
        {
            return db.Links.Count(e => e.Id_Link == id) > 0;
        }
    }
}