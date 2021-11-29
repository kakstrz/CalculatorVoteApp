using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Authentication;
using Calculator.UI.Commands;
using Calculator.UI.Models;
using Calculator.UI.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.UI.ViewModels
{
    public class ElectionVoteViewModel : ViewModelBase
    {
        public ElectionVoteViewModel(
            IAuthenticator authenticator,
            INavigator navigator,
            IGenericRepository<Vote> voteRepository,
            IUserRepository userRepository,
            IGenericRepository<Candidate> candidatesReository,
            IGenericRepository<PoliticalParty> politicalPartyRepository)
        {
            CurrentUser = authenticator.CurrentUser;
            Candidates = new ObservableCollection<CandidateWithParty> { };
            LoadCandidatesCommand = new LoadCandidatesCommand(candidatesReository, politicalPartyRepository, this);
            VoteCommand = new VoteCommand(this, voteRepository, politicalPartyRepository, candidatesReository, userRepository, navigator);
            LoadCandidatesCommand = new LoadCandidatesCommand(candidatesReository, politicalPartyRepository, this);
            LoadCandidatesCommand.Execute(null);
        }

        public User CurrentUser { get; set; }
        private ObservableCollection<CandidateWithParty> _candidates;
        public ObservableCollection<CandidateWithParty> Candidates 
        {
            get
            {
                return _candidates;
            }
            set
            {
                _candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }
        
        private Vote _vote;
        public Vote Vote
        {
            get
            {
                return _vote;
            }
            set
            {
                _vote = value;
                OnPropertyChanged(nameof(Vote));
            }
        }

        public ICommand VoteCommand { get; set; }
        public ICommand LoadCandidatesCommand { get; set; }
    }
}
