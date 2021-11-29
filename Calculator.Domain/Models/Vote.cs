using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Domain.Models
{
    public class Vote : DomainObject
    {
        public bool IsValid { get; set; }
        #nullable enable
        public Guid? CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        #nullable enable
        public virtual Candidate? Candidate { get; set; }
        public DateTime Date { get; set; }
    }
}