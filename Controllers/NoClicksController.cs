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
    public class NoClicksController : ApiController
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: api/NoClicks
        public /*IQueryable<NoClick>*/ int GetNoClicks()
        {
           
            return db.NoClicks.Count()/*db.NoClicks*/;
        }
        [HttpGet]
        [Route("api/DeleteAllClicks")]
        public IHttpActionResult DeleteAllClicks()
        {
            db.NoClicks.RemoveRange(db.NoClicks);
            db.SaveChanges();
            return Ok() ;
        }

        //// GET: api/NoClicks/5
        //[ResponseType(typeof(NoClick))]
        //public IHttpActionResult GetNoClick(int id)
        //{
        //    NoClick noClick = db.NoClicks.Find(id);
        //    if (noClick == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(noClick);
        //}

        //// PUT: api/NoClicks/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutNoClick(int id, NoClick noClick)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != noClick.DBReturnNo)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(noClick).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!NoClickExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/NoClicks
        //[ResponseType(typeof(NoClick))]
        //public IHttpActionResult PostNoClick(NoClick noClick)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.NoClicks.Add(noClick);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = noClick.DBReturnNo }, noClick);
        //}

        //// DELETE: api/NoClicks/5
        //[ResponseType(typeof(NoClick))]
        //public IHttpActionResult DeleteNoClick(int id)
        //{
        //    NoClick noClick = db.NoClicks.Find(id);
        //    if (noClick == null)
        //    {
        //        return NotFound();
        //    }

        //    db.NoClicks.Remove(noClick);
        //    db.SaveChanges();

        //    return Ok(noClick);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoClickExists(int id)
        {
            return db.NoClicks.Count(e => e.DBReturnNo == id) > 0;
        }
    }
}