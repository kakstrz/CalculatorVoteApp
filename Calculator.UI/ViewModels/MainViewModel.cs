using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Commands;
using Calculator.UI.Navigation;
using Calculator.UI.ViewModels.Factories;
using System.Windows.Input;

namespace Calculator.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICalculatorViewModelAbstractFactory _calculatorViewModelFactory;
        private IGenericRepository<Vote> _voteRepository;
        private IUserRepository _userRepository;
        private IGenericRepository<Candidate> _candidatesRepository;
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand LogoutCommand { get; }
        public MainViewModel(
            INavigator navigator, 
            ICalculatorViewModelAbstractFactory calculatorViewModelFactory, 
            IAuthenticator authenticator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesRepository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            Navigator = navigator;
            _calculatorViewModelFactory = calculatorViewModelFactory;
            Authenticator = authenticator;
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _candidatesRepository = candidatesRepository;
            _politicalPartyRepository = politicalPartyRepository;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _calculatorViewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);

            LogoutCommand = new LogoutCommand(
                Navigator, 
                Authenticator, 
                _voteRepository, 
                _userRepository, 
                _candidatesRepository,
                _politicalPartyRepository);
        }
    }
}
