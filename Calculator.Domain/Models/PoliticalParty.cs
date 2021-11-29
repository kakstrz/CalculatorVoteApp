using System.Collections.Generic;

namespace Calculator.Domain.Models
{
    public class PoliticalParty : DomainObject
    {
        public string Name { get; set; }
        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
