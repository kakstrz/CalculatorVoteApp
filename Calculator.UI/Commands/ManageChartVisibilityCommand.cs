using Calculator.UI.ViewModels;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class ManageChartVisibilityCommand : ICommand
    {
        private ElectionStatisticsViewModel _viewModel;
        public ManageChartVisibilityCommand(ElectionStatisticsViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.PartiesName == null)
            {
                Load(_viewModel);
            }
                
            _viewModel.ChartsVisibility = !_viewModel.ChartsVisibility;
        }

        private void Load(ElectionStatisticsViewModel viewModel)
        {
            _viewModel.PartySupport = new ChartValues<int>(_viewModel.Parties.Select(p => p.Support));
            _viewModel.PartiesName = _viewModel.Parties.Select(p => p.Name).ToArray();

            _viewModel.CandidatesSupport = new ChartValues<int>(_viewModel.Candidates.Select(p => p.Support));
            _viewModel.CandidatesName = _viewModel.Candidates.Select(p => p.Surename).ToArray();

            _viewModel.VoteCorrectnessAmount = new ChartValues<int>(new List<int> { int.Parse(_viewModel.CorrectVotes), int.Parse(_viewModel.IncorrectVotes) });
            _viewModel.VoteCorrectnessType = (new List<string> { "Correct", "Incorrect" }).ToArray();
        }
    }
}
