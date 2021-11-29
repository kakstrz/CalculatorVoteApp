using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Models;
using Calculator.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class LoadCandidatesCommand : ICommand
    {
        private IGenericRepository<Candidate> _candidatesRepository;
        private IGenericRepository<PoliticalParty> _politicalPartyRepository;
        private ElectionVoteViewModel _viewModel;
        public LoadCandidatesCommand(
            IGenericRepository<Candidate> candidatesRepository,
            IGenericRepository<PoliticalParty> politicalPartyRepository,
            ElectionVoteViewModel viewModel)
        {
            _candidatesRepository = candidatesRepository;
            _politicalPartyRepository = politicalPartyRepository;
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            IEnumerable<PoliticalParty> parties = await _politicalPartyRepository.GetAll();
            try
            {
                _viewModel.Candidates = CandidateWithParty.ConvertCandidateToCandidateWithParty(await _candidatesRepository.GetAll(), parties);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
