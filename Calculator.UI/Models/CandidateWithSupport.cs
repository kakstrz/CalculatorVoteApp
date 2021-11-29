using Calculator.Domain.Models;
using System;
using System.Collections.Generic;

namespace Calculator.UI.Models
{
    public class CandidateWithSupport : Candidate
    {
        public int Support { get; set; }

        internal static List<CandidateWithSupport> CreateCandidatesWithSupport(IEnumerable<Candidate> candidates, Dictionary<Guid, int> support)
        {
            List<CandidateWithSupport> candidatesWithSupport = new List<CandidateWithSupport>();
            foreach (var candidate in candidates)
            {
                candidatesWithSupport.Add(
                    new CandidateWithSupport()
                    {
                        Id = candidate.Id,
                        Name = candidate.Name,
                        Surename = candidate.Surename,
                        Support = support.GetValueOrDefault(candidate.Id),
                        Votes = candidate.Votes,
                        PoliticalParty = candidate.PoliticalParty,
                        PoliticalPartyId = candidate.PoliticalPartyId,
                    });
            }
            return candidatesWithSupport;
        }
    }
}
