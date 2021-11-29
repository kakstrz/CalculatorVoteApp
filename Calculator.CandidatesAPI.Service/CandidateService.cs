using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Calculator.Domain.Models.IndexTypes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculator.CandidatesAPI.Service
{
    public class CandidateService : ICandidateService
    {
        public async Task<CandidatesSet> GetCandidates(CandidatesIndexType index)
        {
            using(HttpClient client = new HttpClient())
            {
                string uri = "http://webtask.future-processing.com:8069/" + GetUriSuffix(index);
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                CandidatesSet candidatesSet = JsonConvert.DeserializeObject<CandidatesSet>(jsonResponse);
                return candidatesSet;
            }
        }

        private static string GetUriSuffix(CandidatesIndexType index)
        {
            switch (index)
            {
                case CandidatesIndexType.Candidates:
                    return "Candidates";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
