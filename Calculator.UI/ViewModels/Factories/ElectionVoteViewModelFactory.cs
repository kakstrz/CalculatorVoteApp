using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Navigation;

namespace Calculator.UI.ViewModels.Factories
{
    public class ElectionVoteViewModelFactory : ICalculatorViewModelFactory<ElectionVoteViewModel>
    {
        private IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private IGenericRepository<Vote> _voteRepository;
        private IUserRepository _userRepository;
        private IGenericRepository<Candidate> _candidatesReository;
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;
        public ElectionVoteViewModelFactory(
            IAuthenticator authenticator,
            INavigator navigator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesReository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _candidatesReository = candidatesReository;
            _politicalPartyRepository = politicalPartyRepository;
        }
        public ElectionVoteViewModel CreateViewModel()
        {
            return new ElectionVoteViewModel(
                _authenticator, 
                _navigator, 
                _voteRepository, 
                _userRepository, 
                _candidatesReository, 
                _politicalPartyRepository);
        }
    }
}
