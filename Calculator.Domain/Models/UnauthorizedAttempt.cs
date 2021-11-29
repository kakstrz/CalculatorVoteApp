using System;

namespace Calculator.Domain.Models
{
    public class UnauthorizedAttempt : DomainObject
    {
        public DateTime Date { get; set; }
    }
}
