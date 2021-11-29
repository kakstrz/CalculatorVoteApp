using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Navigation;
using Calculator.UI.ViewModels;
using System;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly IGenericRepository<Vote> _voteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Candidate> _candidatesReository;
        private readonly IGenericRepository<PoliticalParty> _politicalPartyRepository;

        public LoginCommand(
            IAuthenticator authenticator,
            LoginViewModel loginViewModel,
            INavigator navigator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesReository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            _authenticator = authenticator;
            _loginViewModel = loginViewModel;
            _navigator = navigator;
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _candidatesReository = candidatesReository;
            _politicalPartyRepository = politicalPartyRepository;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            bool success = await _authenticator.Login(_loginViewModel.Name, _loginViewModel.Surename, _loginViewModel.Pesel);
            if (_authenticator.CurrentUser.HasVoted)
            {
                _navigator.CurrentViewModel = new ElectionStatisticsViewModel(_voteRepository, _politicalPartyRepository, _candidatesReository);
            }
            else
                _navigator.CurrentViewModel = new ElectionVoteViewModel(_authenticator, _navigator, _voteRepository, _userRepository, _candidatesReository, _politicalPartyRepository);
        }
    }
}
