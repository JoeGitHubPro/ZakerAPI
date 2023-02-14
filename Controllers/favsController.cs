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
    public class favsController : ApiController
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: api/favs
        public IQueryable<fav> Getfavs()
        {
            return db.favs;
        }

        // GET: api/favs/5
        [ResponseType(typeof(fav))]
        public IHttpActionResult Getfav(string username)
        {

            var qeury = from one in db.favs
                        where one.UserName == username
                        select new
                        {
                            
                            //UserName = one.UserName,
                            SubjectID = one.Id_Subject,
                            subject = db.Subjects.Where(x => x.Id_Subject == one.Id_Subject).Select(x => x.Subject_Name /*new { x.Id_Subject, x.Subject_Name, x.Subject_Descrption }*/).FirstOrDefault(),
                            subjectCode = db.Subjects.Where(x => x.Id_Subject == one.Id_Subject).Select(x => x.Subject_Descrption /*new { x.Id_Subject, x.Subject_Name, x.Subject_Descrption }*/).FirstOrDefault(),
                        };

            return Ok(qeury);
        }
        //public IHttpActionResult Getfav(string id)
        //{
        //    fav fav = db.favs.Find(id);
        //    if (fav == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(fav);
        //}



        // PUT: api/favs/5
      
        
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfav(string id, fav fav)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fav.UserName)
            {
                return BadRequest();
            }

            db.Entry(fav).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!favExists(id))
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

        // POST: api/favs
        [ResponseType(typeof(fav))]

        public IHttpActionResult Postfav(string username, int SubjectID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Subject subject = db.Subjects.Find(SubjectID);
            if (subject == null)
            {
                return NotFound();
            }
            AspNetUser user = db.AspNetUsers.Where(x=>x.UserName==username).SingleOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            fav fav = new fav();
            fav.Id_Subject = SubjectID;
            fav.UserName = username;
            fav.Subject = subject;
            db.favs.Add(fav);
            db.SaveChanges();


            return Ok();
        }


        //public IHttpActionResult Postfav(fav fav)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.favs.Add(fav);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (favExists(fav.UserName))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = fav.UserName }, fav);
        //}

        // DELETE: api/favs/5
       
        
        [ResponseType(typeof(fav))]
        public IHttpActionResult Deletefav(string username, int SubjectID)
        {


            fav fav = db.favs.Where(x => x.UserName == username && x.Id_Subject == SubjectID).SingleOrDefault();
            if (fav == null)
            {
                return NotFound();
            }
            db.favs.Remove(fav);
            db.SaveChanges();
            return Ok();
        }
        //public IHttpActionResult Deletefav(string id)
        //{
        //    fav fav = db.favs.Find(id);
        //    if (fav == null)
        //    {
        //        return NotFound();
        //    }

        //    db.favs.Remove(fav);
        //    db.SaveChanges();

        //    return Ok(fav);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool favExists(string id)
        {
            return db.favs.Count(e => e.UserName == id) > 0;
        }
    }
}