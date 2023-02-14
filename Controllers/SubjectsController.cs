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
    public class SubjectsController : ApiController
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: api/Subjects
        //public IQueryable<Subject> GetSubjects()
        //{
        //    return db.Subjects;
        //}
       // [Authorize]
        public IHttpActionResult GetSubjects()
        {
            var sub = db.Subjects
                        .Select(x => new { x.Id_Subject, x.Subject_Name, x.Subject_Descrption });
            NoClick noClick = new NoClick();
            noClick.Data = DateTime.Now;
            db.NoClicks.Add(noClick);
            db.SaveChanges();

            return Ok(sub);
        }
       // [Authorize]
        // GET: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult GetSubject(int SubjectId, int CategoryId)
        {
            //Subject subject = db.Subjects.Find(id);
            //if (subject == null)
            //{
            //    return NotFound();
            //}

            //return Ok(subject);

            var links = db.Links.Where(x => x.Id_Subject == SubjectId && x.Id_Category == CategoryId).ToList().Distinct().Select(x => new { x.Id_Subject, x.Id_Link, x.Pdf_Name, x.Link1 });
            //var sub = db.Subjects
            //          .Select(x => new { x.Id_Subject, x.Subject_Name, x.Subject_Descrption })
            //          .Where(x => x.Id_Subject == id);
            return Ok(links);
        }
        public IHttpActionResult GetSubject(int id )
        {
     var sub = db.Subjects
                      .Select(x => new { x.Id_Subject, x.Subject_Name, x.Subject_Descrption })
                      .Where(x => x.Id_Subject == id);
            return Ok(sub);
        }

        // PUT: api/Subjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubject(int id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Id_Subject)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [ResponseType(typeof(Subject))]
        public IHttpActionResult PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subject.Id_Subject }, subject);
        }

        // DELETE: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subject);
            db.SaveChanges();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(int id)
        {
            return db.Subjects.Count(e => e.Id_Subject == id) > 0;
        }
    }
}