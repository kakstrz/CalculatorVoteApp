using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface ICandidateProvider
    {
        public Task ProvideCandidates();
        public Task ProvidePoliticalParties();
    }
}
