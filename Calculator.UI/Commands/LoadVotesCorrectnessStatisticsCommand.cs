using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.UI.Commands
{
    public class LoadVotesCorrectnessStatisticsCommand : ICommand
    {
        private ElectionStatisticsViewModel _viewModel;
        private IGenericRepository<Vote> _votesRepository;

        public LoadVotesCorrectnessStatisticsCommand(
            ElectionStatisticsViewModel viewModel,
            IGenericRepository<Vote> votesRepository)
        {
            _viewModel = viewModel;
            _votesRepository = votesRepository;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            IEnumerable<Vote> votes = await _votesRepository.GetAll();
            var result = CountVotes(votes);
            _viewModel.CorrectVotes = result.Item1.ToString();
            _viewModel.IncorrectVotes = result.Item2.ToString();
        }

        private (int,int) CountVotes(IEnumerable<Vote> votes)
        {
            int correct = votes.Where(v => v.IsValid == true).Count();
            int incorrect = votes.Where(v => v.IsValid == false).Count();

            return (correct, incorrect);
        }
    }
}
