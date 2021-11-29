using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Navigation;

namespace Calculator.UI.ViewModels.Factories
{
    public class LoginViewModelFactory : ICalculatorViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private IGenericRepository<Vote> _voteRepository;
        private IUserRepository _userRepository;
        private IGenericRepository<Candidate> _candidatesRepository;
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;

        public LoginViewModelFactory(
            IAuthenticator authenticator,
            INavigator navigator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesRepository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _candidatesRepository = candidatesRepository;
            _politicalPartyRepository = politicalPartyRepository;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(
                _authenticator, 
                _navigator, 
                _voteRepository, 
                _userRepository,
                _candidatesRepository,
                _politicalPartyRepository);
        }
    }
}
