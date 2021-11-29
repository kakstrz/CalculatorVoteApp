using Calculator.Domain.Models;
using Calculator.Domain.Models.IndexTypes;
using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidatesSet> GetCandidates(CandidatesIndexType index);
    }
}
