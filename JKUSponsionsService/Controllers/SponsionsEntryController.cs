using JKUSponsionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JKUSponsionsService.Controllers
{
    public class SponsionsEntryController : ApiController
    {
        private JKUSponsionsServiceEntities1 db = new JKUSponsionsServiceEntities1();

        // GET: api/SponsionsEntry/5
        [ResponseType(typeof(SponsionsEntry))]
        public async Task<IHttpActionResult> GetSponsionsEntry(int id)
        {

            SponsionsEntry sponsionsEntry = new SponsionsEntry();
            List<Member> Members = new List<Member>();
            List<Representative> Representatives = new List<Representative>(); ;

            Event @event = await db.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            sponsionsEntry.Event = @event;

            IEnumerable<Event_Member> em = db.Event_Member.Where(m => m.EventId == @event.Id);
            foreach (Event_Member e in em)
            {
                Member mem = db.Member.Where(m => m.Id == e.MemberId).First();
                if (mem!= null)
                    Members.Add(mem);
            }
            sponsionsEntry.Members = Members;

            IEnumerable<Event_Representative> er = db.Event_Representative.Where(m => m.EventId == @event.Id);
            foreach (Event_Representative e in er)
            {
                Representative rep = db.Representative.Where(r => r.Id == e.RepresentativeId).First();
                if (rep != null)
                    Representatives.Add(rep);
            }
            sponsionsEntry.Representatives = Representatives;

            sponsionsEntry.MembersCount = Members.Count;
            sponsionsEntry.RepresentativesCount = Representatives.Count;
            return Ok(sponsionsEntry);
        }
        
    }
}
