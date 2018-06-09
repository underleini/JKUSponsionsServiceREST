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
using Swashbuckle.Swagger.Annotations;


namespace JKUSponsionsService.Controllers
{
    public class EventsController : ApiController
    {
        private JKUSponsionsServiceEntities1 db = new JKUSponsionsServiceEntities1();

        // GET: api/Events
        [SwaggerOperation("GetAll")]
        public IQueryable<Event> GetEvent()
        {
            return db.Event;
        }

        // GET: api/Events/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Event.Add(@event);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Event.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Event.Count(e => e.Id == id) > 0;
        }

        [Route("api/Events/{EventId}/addMember/{MemberId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> addMember(int EventId, int MemberId)
        {
            Event_Member event_Member = new Event_Member
            {
                EventId = EventId,
                MemberId = MemberId
            };

            db.Event_Member.Add(event_Member);
            await db.SaveChangesAsync();

            return Ok(event_Member);
        }

        [Route("api/Events/{EventId}/deleteMember/{MemberId}")]
        [ResponseType(typeof(Event_Member))]
        public async Task<IHttpActionResult> deleteMember(int EventId, int MemberId)
        {
            IEnumerable<Event_Member> event_Members = db.Event_Member.Where(em => em.EventId == EventId && em.MemberId == MemberId);
            if (event_Members == null)
            {
                return NotFound();
            }
            foreach (Event_Member em in event_Members)
            {
                db.Event_Member.Remove(em);
            }
            
            await db.SaveChangesAsync();
           
            return Ok(event_Members.ToList());
        }
        
        [Route("api/Events/{EventId}/addRepresentative/{RepresentativeId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> addRepresentative(int EventId, int RepresentativeId)
        {
            Event_Representative event_Rep = new Event_Representative
            {
                EventId = EventId,
                RepresentativeId = RepresentativeId
            };

            db.Event_Representative.Add(event_Rep);
            await db.SaveChangesAsync();

            return Ok(event_Rep);
        }

        [Route("api/Events/{EventId}/deleteRepresentative/{RepresentativeId}")]
        [ResponseType(typeof(Event_Representative))]
        public async Task<IHttpActionResult> deleteRepresentative(int EventId, int RepresentativeId)
        {
            IEnumerable<Event_Representative> event_Representatives = db.Event_Representative.Where(er => er.EventId == EventId && er.RepresentativeId == RepresentativeId);
            if (event_Representatives == null)
            {
                return NotFound();
            }
            foreach (Event_Representative er in event_Representatives)
            {
                db.Event_Representative.Remove(er);
            }

            await db.SaveChangesAsync();

            return Ok(event_Representatives.ToList());
        }


    }
}