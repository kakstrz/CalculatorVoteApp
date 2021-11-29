using Calculator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Calculator.Data
{
    public class CalculatorDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<PoliticalParty> PoliticalParties { get; set; }
        public DbSet<UnauthorizedAttempt> UnauthorizedAttempts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public CalculatorDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Temporarily seeding hardcoded data.
            base.OnModelCreating(mb);
            var piastowie = new PoliticalParty() { Id = Guid.NewGuid(), Name = "Piastowie" };
            var jagiellonowie = new PoliticalParty() { Id = Guid.NewGuid(), Name = "Dynastia Jagiellonów" };
            var elekcyjni = new PoliticalParty() { Id = Guid.NewGuid(), Name = "Elekcyjni dla Polski" };
            var wazowie = new PoliticalParty() { Id = Guid.NewGuid(), Name = "Wazowie" };

            mb.Entity<PoliticalParty>().HasData(
                piastowie,
                jagiellonowie,
                elekcyjni,
                wazowie
                );

            mb.Entity<Candidate>().HasData(
               new Candidate() { Id = Guid.NewGuid(), Name = "Mieszko", Surename ="I", PoliticalPartyId = piastowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Bolesław", Surename = "Chrobry", PoliticalPartyId = piastowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Władysław", Surename = "Łokietek", PoliticalPartyId = piastowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Kazimierz", Surename = "Wielki", PoliticalPartyId = piastowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Władysław", Surename = "Jagiełło", PoliticalPartyId = jagiellonowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Władysław", Surename = "Warneńczyk", PoliticalPartyId = jagiellonowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Zygmunt", Surename = "Stary", PoliticalPartyId = jagiellonowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Henryk", Surename = "Walezy", PoliticalPartyId = elekcyjni.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Anna", Surename = "Jagiellonka", PoliticalPartyId = elekcyjni.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Stefan", Surename = "Batory", PoliticalPartyId = elekcyjni.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Zygmunt", Surename = "Waza", PoliticalPartyId = wazowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Władysław", Surename = "Waza", PoliticalPartyId = wazowie.Id },
               new Candidate() { Id = Guid.NewGuid(), Name = "Jan", Surename = "Kazimierz", PoliticalPartyId = wazowie.Id }
               );
        }

    }
}
