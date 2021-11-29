using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Calculator.Domain.Models
{
    public class CandidatesSet
    {
        [JsonProperty("candidates")]
        public Candidates Candidates { get; set; }
    }

    public class Candidates
    {
        [JsonProperty("publicationDate")]
        public DateTimeOffset PublicationDate { get; set; }

        [JsonProperty("candidate")]
        public List<JsonCandidate> Candidate { get; set; }
    }

    public class JsonCandidate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("party")]
        public string Party { get; set; }
    }
}
