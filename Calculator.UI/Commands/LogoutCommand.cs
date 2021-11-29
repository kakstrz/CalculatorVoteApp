using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Navigation;
using Calculator.UI.ViewModels;
using System;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        private IGenericRepository<Vote> _voteRepository;
        private IUserRepository _userRepository;
        private IGenericRepository<Candidate> _candidatesRepository;
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;

        public LogoutCommand(
            INavigator navigator,
            IAuthenticator authenticator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesRepository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _candidatesRepository = candidatesRepository;
            _politicalPartyRepository = politicalPartyRepository;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _authenticator.Logout();
            _navigator.CurrentViewModel = new LoginViewModel(
                _authenticator,
                _navigator,
                _voteRepository,
                _userRepository,
                _candidatesRepository,
                _politicalPartyRepository);
        }
    }
}
