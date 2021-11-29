using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;

namespace Calculator.UI.ViewModels.Factories
{
    public class ElectionStatisticsViewModelFactory : ICalculatorViewModelFactory<ElectionStatisticsViewModel>
    {
        private IGenericRepository<Vote> _voteRepository;
        private IGenericRepository<PoliticalParty> _partyRepository;
        private IGenericRepository<Candidate> _candidateRepository;
        public ElectionStatisticsViewModelFactory(
            IGenericRepository<Vote> voteRepository,
            IGenericRepository<PoliticalParty> partyRepository,
            IGenericRepository<Candidate> candidateRepository)
        {
            _voteRepository = voteRepository;
            _partyRepository = partyRepository;
            _candidateRepository = candidateRepository;
        }
        public ElectionStatisticsViewModel CreateViewModel()
        {
            return new ElectionStatisticsViewModel(_voteRepository, _partyRepository, _candidateRepository);
        }
    }
}
