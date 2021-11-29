using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Domain.Models
{
    public class Candidate : DomainObject
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        [ForeignKey("PoliticalPartyId")]
        public virtual PoliticalParty PoliticalParty { get; set; }
        public Guid PoliticalPartyId { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
