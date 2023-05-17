using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using webapi.Model;
using Match = webapi.Model.Match;

namespace webapi.Context
{
    public class OnlabProjectContext : ApiAuthorizationDbContext<User>
    {
        public OnlabProjectContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UUWEOR;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tournamentformats = modelBuilder.Entity<TournamentFormat>();
            tournamentformats.ToTable("TournamentFormats")
                .HasDiscriminator<int>("FormatType")
                .HasValue<RoundRobin>(1)
                .HasValue<Elimination>(2);

            tournamentformats.HasOne(tf => tf.Tournament)
                .WithOne(t => t.Format)
                .HasForeignKey<Tournament>(t => t.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            var eliminations = modelBuilder.Entity<Elimination>();

            var games = modelBuilder.Entity<Game>();

            var matches = modelBuilder.Entity<Match>();
            matches.HasOne(m => m.Winner)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            var roundrobins = modelBuilder.Entity<RoundRobin>();

            var teams = modelBuilder.Entity<Team>();
            teams.HasOne(t => t.TeamGame)
                .WithMany(g => g.Teams)
                .OnDelete(DeleteBehavior.ClientCascade);

            var tournaments = modelBuilder.Entity<Tournament>();

            var users = modelBuilder.Entity<User>();

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Elimination> Eliminations { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<RoundRobin> RoundRobins { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TournamentFormat> TournamentFormats { get; set; }
    }
}
