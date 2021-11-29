using Newtonsoft.Json;
using System.Collections.Generic;

namespace Calculator.Domain.Models
{
    public class Person
    {
        [JsonProperty("pesel")]
        public string Pesel { get; set; }
    }

    public class Disallowed
    {
        [JsonProperty("publicationDate")]
        public string PublicationDate { get; set; }
        [JsonProperty("person")]
        public List<Person> Person { get; set; }
    }

    public class DisallowedSet
    {
        [JsonProperty("disallowed")]
        public Disallowed Disallowed { get; set; }
    }

}
