using System;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Domain.Models
{
    public class DomainObject
    {
        [Key]
        public Guid Id { get; set; }
    }
}
