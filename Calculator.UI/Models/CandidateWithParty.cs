using Calculator.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Calculator.UI.Models
{
    public class CandidateWithParty : Candidate
    {
        public string PartyName { get; set; }
        public static ObservableCollection<CandidateWithParty> ConvertCandidateToCandidateWithParty(IEnumerable<Candidate> candidates, IEnumerable<PoliticalParty> parties)
        {
            ObservableCollection<CandidateWithParty> candidatesWithParties = new ObservableCollection<CandidateWithParty>();

            foreach (var candidate in candidates)
            {
                candidatesWithParties.Add(new CandidateWithParty()
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    PoliticalParty = candidate.PoliticalParty,
                    PoliticalPartyId = candidate.PoliticalPartyId,
                    Surename = candidate.Surename,
                    PartyName = parties.First(x=>x.Id == candidate.PoliticalPartyId).Name,
                    Votes = candidate.Votes
                });
            }
            return candidatesWithParties;
        }
    }
}
