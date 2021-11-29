using Calculator.Domain.Models;
using System;
using System.Collections.Generic;

namespace Calculator.UI.Models
{
    public class PartyWithSupport : PoliticalParty
    {
        public int Support { get; set; }
        internal static List<PartyWithSupport> CreatePartiesWithSupport(IEnumerable<PoliticalParty> parties, Dictionary<Guid, int> support)
        {
            List<PartyWithSupport> partiesWithSupport = new List<PartyWithSupport>();
            foreach(var party in parties)
            {
                partiesWithSupport.Add(
                    new PartyWithSupport() {
                    Id = party.Id,
                    Name = party.Name,
                    Candidates = party.Candidates,
                    Support = support.GetValueOrDefault(party.Id)
                    });
            }
            return partiesWithSupport;
        }
    }
}
