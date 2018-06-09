using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JKUSponsionsService.Models
{
    public class SponsionsEntry
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Representative> Representatives { get; set; }
        public Event Event { get; set; }
        public int MembersCount { get; set; }
        public int RepresentativesCount { get; set; }
    }
}