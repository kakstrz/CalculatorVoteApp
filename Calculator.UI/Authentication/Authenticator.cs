using Calculator.Domain.AuthenticationServices;
using Calculator.Domain.Models;
using Calculator.UI.Models;
using System;
using System.Threading.Tasks;

namespace Calculator.UI.Authentication
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        private User _currentUser;
        public User CurrentUser 
        {
            get 
            {
                return _currentUser;
            }
            private set 
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsLoggedIn));
            } 
        }
        public bool IsLoggedIn => CurrentUser != null;
        public bool Voted => CurrentUser.HasVoted;

        public async Task<bool> Login(string name, string surename, string pesel)
        {
            bool loginSucceeded;
            try
            {
                CurrentUser = await _authenticationService.Login(name, surename, pesel);
                loginSucceeded = true;
            }
            catch(Exception)
            {
                loginSucceeded = false;
            }
            return loginSucceeded;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
