using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Data
{
    public class CandidatesProvider : ICandidateProvider
    {
        private readonly ICandidateService _candidateService;
        private readonly IGenericRepository<Candidate> _candidateRepository;
        private readonly IGenericRepository<PoliticalParty> _partyRepository;
        public List<PoliticalParty> Parties { get; set; }
        private CandidatesSet CandidatesSet { get; set; }

        public CandidatesProvider(
             ICandidateService candidateService,
             IGenericRepository<Candidate> candidateRepository,
             IGenericRepository<PoliticalParty> partyRepository)
        {
            _candidateService = candidateService;
            _candidateRepository = candidateRepository;
            _partyRepository = partyRepository;

            Parties = new List<PoliticalParty>();
        }

        public async Task ProvidePoliticalParties()
        {
            CandidatesSet = await _candidateService.GetCandidates(Domain.Models.IndexTypes.CandidatesIndexType.Candidates);

            foreach (var party in CandidatesSet.Candidates.Candidate.Select(x => x.Party).Distinct())
            {
                Parties.Add(new PoliticalParty()
                {
                    Name = party
                });
            }
        }

        public async Task ProvideCandidates()
        {
            foreach(JsonCandidate candidate in CandidatesSet.Candidates.Candidate)
            {
                await _candidateRepository.Create(new Candidate()
                {
                    Name = candidate.Name.Split(" ")[0],
                    Surename = candidate.Name.Split(" ")[1],
                    Votes = new List<Vote>(),
                    PoliticalParty = Parties.First(x => x.Name == candidate.Party)
                });
            }
        }
    }
}
