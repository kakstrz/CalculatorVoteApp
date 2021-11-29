using System;

namespace Calculator.Domain.Models
{
    public class User : DomainObject
    {
        public string Name { get; set; }
        public string SureName { get; set; }
        public byte[] PeselHash { get; set; }
        public byte[] PeselSalt { get; set;}
        public bool HasVoted { get; set; }
        public bool HasVoteRight { get; set; }
    }
}
