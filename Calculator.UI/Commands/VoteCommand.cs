using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Models;
using Calculator.UI.Navigation;
using Calculator.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class VoteCommand : ICommand
    {
        private ElectionVoteViewModel _viewModel;
        private IGenericRepository<Vote> _voteRepository;
        private IGenericRepository<PoliticalParty> _partyRepository;
        private IGenericRepository<Candidate> _candidateRepository;
        private IUserRepository _userRepository;
        private readonly INavigator _navigator;

        public event EventHandler CanExecuteChanged;
        public VoteCommand(
            ElectionVoteViewModel viewModel, 
            IGenericRepository<Vote> voteRepository,
            IGenericRepository<PoliticalParty> partyRepository,
            IGenericRepository<Candidate> candidateRepository,
            IUserRepository userRepository,
            INavigator navigator)
        {
            _viewModel = viewModel;
            _voteRepository = voteRepository;
            _partyRepository = partyRepository;
            _candidateRepository = candidateRepository;
            _userRepository = userRepository;
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Is this your final decision? After clicking, you won't be able to change your vote.", "Confirm your choice", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                var selestedCandidates3 = parameter as System.Collections.IList;
                System.Collections.IList items = (System.Collections.IList)parameter;
                var selectedCollection = items.Cast<CandidateWithParty>();

                Vote vote = new Vote
                {
                    Date = DateTime.Now
                };

                if (selectedCollection.Count() != 1 || !_viewModel.CurrentUser.HasVoteRight)
                {
                    vote.IsValid = false;
                    vote.CandidateId = null;
                }
                else
                {
                    vote.IsValid = true;
                    vote.CandidateId = selectedCollection.First().Id;
                }

                await _voteRepository.Create(vote);
                await _userRepository.Update(
                    _viewModel.CurrentUser.Id,
                    new User
                    {
                        Name = _viewModel.CurrentUser.Name,
                        SureName = _viewModel.CurrentUser.SureName,
                        HasVoteRight = _viewModel.CurrentUser.HasVoteRight,
                        PeselHash = _viewModel.CurrentUser.PeselHash,
                        PeselSalt = _viewModel.CurrentUser.PeselSalt,
                        Id = _viewModel.CurrentUser.Id,
                        HasVoted = true
                    });
                _navigator.CurrentViewModel = new ElectionStatisticsViewModel(_voteRepository, _partyRepository, _candidateRepository);
            }
        }
    }
}
