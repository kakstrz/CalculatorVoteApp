using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Commands;
using Calculator.UI.Navigation;
using System.Windows.Input;

namespace Calculator.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surename;
        public string Surename
        {
            get
            {
                return _surename;
            }
            set
            {
                _surename = value;
                OnPropertyChanged(nameof(Surename));
            }
        }

        private string _pesel;
        public string Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
                OnPropertyChanged(nameof(Pesel));
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(
            IAuthenticator authenticator, 
            INavigator navigator, 
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesReository,
            IGenericRepository<PoliticalParty> politicalPartyReository
            )
        {
            LoginCommand = new LoginCommand(
                authenticator, 
                this, 
                navigator, 
                voteRepository, 
                userRepository, 
                candidatesReository,
                politicalPartyReository);
        }
    }
}
