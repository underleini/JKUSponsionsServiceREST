using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using JKUSponsionsService.Models;

namespace JKUSponsionsService.Controllers
{
    public class RepresentativesController : ApiController
    {
        private JKUSponsionsServiceEntities1 db = new JKUSponsionsServiceEntities1();

        // GET: api/Representatives
        public IQueryable<Representative> GetRepresentative()
        {
            return db.Representative;
        }

        // GET: api/Representatives/5
        [ResponseType(typeof(Representative))]
        public async Task<IHttpActionResult> GetRepresentative(int id)
        {
            Representative representative = await db.Representative.FindAsync(id);
            if (representative == null)
            {
                return NotFound();
            }

            return Ok(representative);
        }

        // PUT: api/Representatives/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRepresentative(int id, Representative representative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != representative.Id)
            {
                return BadRequest();
            }

            db.Entry(representative).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepresentativeExists(id))
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

        // POST: api/Representatives
        [ResponseType(typeof(Representative))]
        public async Task<IHttpActionResult> PostRepresentative(Representative representative)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Representative.Add(representative);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = representative.Id }, representative);
        }

        // DELETE: api/Representatives/5
        [ResponseType(typeof(Representative))]
        public async Task<IHttpActionResult> DeleteRepresentative(int id)
        {
            Representative representative = await db.Representative.FindAsync(id);
            if (representative == null)
            {
                return NotFound();
            }

            db.Representative.Remove(representative);
            await db.SaveChangesAsync();

            return Ok(representative);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RepresentativeExists(int id)
        {
            return db.Representative.Count(e => e.Id == id) > 0;
        }
    }
}