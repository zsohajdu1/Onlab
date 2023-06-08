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
            var games = modelBuilder.Entity<Game>();
            games.HasData(new[]
            {
                new Game() {Id = 1, Name = "CSGO", TeamSize = 5, Icon = "CSGO.jpg"},
                new Game() {Id = 2, Name = "LOL", TeamSize = 5, Icon = "LOL.jpg"},
                new Game() {Id = 3, Name = "DOTA2", TeamSize = 5, Icon = "DOTA2.jpg"},
                new Game() {Id = 4, Name = "Valorant", TeamSize = 5, Icon = "Valorant.jpg"},
                new Game() {Id = 5, Name = "Rocket League", TeamSize = 3, Icon = "RocketLeague.jpg"}
            }) ;

            var matches = modelBuilder.Entity<Match>();
            matches.HasOne(m => m.Winner)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
            matches.HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .OnDelete(DeleteBehavior.ClientCascade);


            var teams = modelBuilder.Entity<Team>();
            teams.HasOne(t => t.TeamGame)
                .WithMany(g => g.Teams)
                .OnDelete(DeleteBehavior.ClientCascade);
            teams.HasOne(t => t.Captain)
                .WithMany(u => u.OwnedTeams)
                .OnDelete(DeleteBehavior.ClientCascade);
            teams.HasMany(t => t.Members)
                .WithMany(u => u.Teams);

            var tournaments = modelBuilder.Entity<Tournament>();
            tournaments.HasOne(t => t.Owner)
                .WithMany(u => u.Tournaments)
                .OnDelete(DeleteBehavior.ClientCascade);

            var users = modelBuilder.Entity<User>();

            var memberApplications = modelBuilder.Entity<MemberApplication>();
            memberApplications.HasOne(ma => ma.User)
                .WithMany(u => u.MemberApplications)
                .OnDelete(DeleteBehavior.ClientCascade);
            memberApplications.HasOne(ma => ma.Team)
                .WithMany(t => t.MemberApplications)
                .OnDelete(DeleteBehavior.ClientCascade);

            var tournamentApplications = modelBuilder.Entity<TournamentApplication>();
            tournamentApplications.HasOne(ta => ta.Tournament)
                .WithMany(t => t.TournamentApplications)
                .OnDelete(DeleteBehavior.ClientCascade);
            tournamentApplications.HasOne(ta => ta.Team)
                .WithMany(t => t.TournamentApplications)
                .OnDelete(DeleteBehavior.ClientCascade);

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MemberApplication> MemberApplications { get; set; }
        public DbSet<TournamentApplication> TournamentApplications { get; set; }
    }
}
