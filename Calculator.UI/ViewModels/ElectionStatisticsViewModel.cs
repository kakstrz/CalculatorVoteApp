using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.UI.Commands;
using Calculator.UI.Models;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Calculator.UI.ViewModels
{
    public class ElectionStatisticsViewModel : ViewModelBase
    {
        public ElectionStatisticsViewModel(
            IGenericRepository<Vote> voteRepository, 
            IGenericRepository<PoliticalParty> partyRepository,
            IGenericRepository<Candidate> candidateRepository)
        {
            var now = DateTime.Now;
            VotingStatus = String.Format("Voting status on {0}.{1}.{2} at {3}:{4}", now.Day, now.Month, now.Year, now.Hour,now.Minute);
            Parties = new List<PartyWithSupport>();
            Candidates = new List<CandidateWithSupport>();
            LoadVotesCorrectnessStatistics = new LoadVotesCorrectnessStatisticsCommand(this, voteRepository);
            LoadVotesCorrectnessStatistics.Execute(null);
            LoadPartiesSupportStatistics = new LoadPartiesSupportStatisticsCommand(partyRepository, voteRepository, candidateRepository, this);
            LoadPartiesSupportStatistics.Execute(null);
            LoadCandidatesSupportStatistics = new LoadCandidatesSupportStatisticsCommand(voteRepository, candidateRepository, this);
            LoadCandidatesSupportStatistics.Execute(null);
            ManageChartVisibility = new ManageChartVisibilityCommand(this);
        }
        public string VotingStatus { get; set; }

        private string _correctVotes;
        public string CorrectVotes
        {
            get
            {
                return _correctVotes;
            }
            set
            {
                _correctVotes = value;
                OnPropertyChanged(nameof(CorrectVotes));
            }
        }

        private string _incorrectVotes;
        public string IncorrectVotes
        {
            get
            {
                return _incorrectVotes;
            }
            set
            {
                _incorrectVotes = value;
                OnPropertyChanged(nameof(IncorrectVotes));
            }
        }

        private List<PartyWithSupport> _parties;
        public List<PartyWithSupport> Parties
        {
            get
            {
                return _parties;
            }
            set
            {
                _parties = value;
                OnPropertyChanged(nameof(Parties));
            }
        }

        private List<CandidateWithSupport> _candidates;
        public List<CandidateWithSupport> Candidates
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

        private ChartValues<int> _partiesSupport;
        public ChartValues<int> PartySupport
        {
            get
            {
                return _partiesSupport;
            }
            set
            {
                _partiesSupport = value;
                OnPropertyChanged(nameof(PartySupport));
            }
        }

        private string[] _partiesName;
        public string[] PartiesName
        {
            get
            {
                return _partiesName;
            }
            set
            {
                _partiesName = value;
                OnPropertyChanged(nameof(PartiesName));
            }
        }

        private ChartValues<int> _candidatesSupport;
        public ChartValues<int> CandidatesSupport
        {
            get
            {
                return _candidatesSupport;
            }
            set
            {
                _candidatesSupport = value;
                OnPropertyChanged(nameof(CandidatesSupport));
            }
        }

        private string[] _candidatesName;
        public string[] CandidatesName
        {
            get
            {
                return _candidatesName;
            }
            set
            {
                _candidatesName = value;
                OnPropertyChanged(nameof(CandidatesName));
            }
        }

        private ChartValues<int> _voteCorrectnessAmount;
        public ChartValues<int> VoteCorrectnessAmount
        {
            get
            {
                return _voteCorrectnessAmount;
            }
            set
            {
                _voteCorrectnessAmount = value;
                OnPropertyChanged(nameof(VoteCorrectnessAmount));
            }
        }

        private string[] _voteCorrectnessType;
        public string[] VoteCorrectnessType
        {
            get
            {
                return _voteCorrectnessType;
            }
            set
            {
                _voteCorrectnessType = value;
                OnPropertyChanged(nameof(VoteCorrectnessType));
            }
        }

        private bool _chartsVisibility = false;
        public bool ChartsVisibility
        {
            get
            {
                return _chartsVisibility;
            }
            set
            {
                _chartsVisibility = value;
                OnPropertyChanged(nameof(ChartsVisibility));
            }
        }

        public ICommand LoadVotesCorrectnessStatistics { get; set; }
        public ICommand LoadPartiesSupportStatistics { get; set; }
        public ICommand LoadCandidatesSupportStatistics { get; set; }
        public ICommand ManageChartVisibility { get; set; }
    }
}

