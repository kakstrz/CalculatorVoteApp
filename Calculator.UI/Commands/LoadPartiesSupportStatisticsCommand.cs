using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Models;
using Calculator.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class LoadPartiesSupportStatisticsCommand : ICommand
    {
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;
        private IGenericRepository<Vote> _voteRepository;
        private IGenericRepository<Candidate> _candidateRepository;
        private ElectionStatisticsViewModel _viewModel;
        public LoadPartiesSupportStatisticsCommand(
            IGenericRepository<PoliticalParty> politicalPartyRepository,
            IGenericRepository<Vote> voteRepository,
            IGenericRepository<Candidate> candidateRepository,
            ElectionStatisticsViewModel viewModel)
        {
            _viewModel = viewModel;
            _politicalPartyRepository = politicalPartyRepository;
            _voteRepository = voteRepository;
            _candidateRepository = candidateRepository;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            IEnumerable<PoliticalParty> parties = await _politicalPartyRepository.GetAll();
            IEnumerable<Vote> votes = await _voteRepository.GetAll();
            IEnumerable<Candidate> candidates = await _candidateRepository.GetAll();
            var support = CountVotes(parties, votes, candidates);
            _viewModel.Parties = PartyWithSupport.CreatePartiesWithSupport(parties, support);
        }

        private Dictionary<Guid, int> CountVotes(IEnumerable<PoliticalParty> parties, IEnumerable<Vote> votes, IEnumerable<Candidate> candidates)
        {
            Dictionary<Guid, int> partiesSupport = new Dictionary<Guid, int>();
            
            foreach (var party in parties)
            {
                partiesSupport.Add(party.Id, 0);
            }

            foreach (var vote in votes)
            {
                if (vote.IsValid)
                {
                    foreach (var candidate in candidates)
                    {
                        if (parties.Any(p => p.Id == candidate.PoliticalPartyId && candidate.Id == vote.CandidateId))
                        {
                            partiesSupport[candidate.PoliticalPartyId]++;
                        }
                    }
                }
            }
            return partiesSupport;
        }
    }
}
