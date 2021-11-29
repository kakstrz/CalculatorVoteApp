using Calculator.UI.Authentication;
using Calculator.UI.Navigation;
using System;

namespace Calculator.UI.ViewModels.Factories
{
    public class CalculatorViewModelAbstractFactory : ICalculatorViewModelAbstractFactory
    {
        private ICalculatorViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private ICalculatorViewModelFactory<ElectionStatisticsViewModel> _electionStatisticsViewModelFactory;
        private ICalculatorViewModelFactory<ElectionVoteViewModel> _electionVoteViewModelFactory;
        public CalculatorViewModelAbstractFactory(
            ICalculatorViewModelFactory<LoginViewModel> loginViewModelFactory,
            ICalculatorViewModelFactory<ElectionStatisticsViewModel> electionStatisticsViewModelFactory,
            ICalculatorViewModelFactory<ElectionVoteViewModel> electionVoteViewModelFactory)
        {
            _loginViewModelFactory = loginViewModelFactory;
            _electionStatisticsViewModelFactory = electionStatisticsViewModelFactory;
            _electionVoteViewModelFactory = electionVoteViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.ElectionVote:
                    return _electionVoteViewModelFactory.CreateViewModel();
                case ViewType.ElectionStatistics:
                    return _electionStatisticsViewModelFactory.CreateViewModel();
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
