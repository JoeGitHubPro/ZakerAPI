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
    public class CategoriesController : ApiController
    {
        private ScienceEntities db = new ScienceEntities();

        // GET: api/Categories
        [Route("api/Categories/GetCategories")]
        public IHttpActionResult GetCategories(int SubjectId)
        {
            var result = db.Links.Where(q => q.Id_Subject == SubjectId).Select(q => new { q.Category.ID, q.Category.Name }).Distinct().ToList();
            return Ok(result);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.Where(q=>q.ID == id).Select(a => new { a.ID, a.Name }).SingleOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.ID == id) > 0;
        }
    }
}