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
    public class LoadCandidatesSupportStatisticsCommand : ICommand
    {
        private IGenericRepository<Vote> _voteRepository;
        private IGenericRepository<Candidate> _candidateRepository;
        private ElectionStatisticsViewModel _viewModel;

        public LoadCandidatesSupportStatisticsCommand(
            IGenericRepository<Vote> voteRepository,
            IGenericRepository<Candidate> candidateRepository,
            ElectionStatisticsViewModel viewModel)
        {
            _viewModel = viewModel;
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
            IEnumerable<Vote> votes = await _voteRepository.GetAll();
            IEnumerable<Candidate> candidates = await _candidateRepository.GetAll();
            var support = CountVotes(votes, candidates);
            _viewModel.Candidates = CandidateWithSupport.CreateCandidatesWithSupport(candidates, support);
        }

        private Dictionary<Guid, int> CountVotes(IEnumerable<Vote> votes, IEnumerable<Candidate> candidates)
        {
            Dictionary<Guid, int> candidatesSupport = new Dictionary<Guid, int>();

            foreach (var candidate in candidates)
            {
                var result = votes.Where(v => v.CandidateId == candidate.Id).Count();
                if (!candidatesSupport.ContainsKey(candidate.Id))
                {
                    candidatesSupport.Add(candidate.Id, result);
                }
                else if (candidatesSupport.ContainsKey(candidate.Id) && result != 0)
                {
                    candidatesSupport[candidate.Id]++;
                }
            }
            return candidatesSupport;
        }
    }
}
