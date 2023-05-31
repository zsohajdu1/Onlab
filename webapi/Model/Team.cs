namespace webapi.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeamStatus Status { get; set; }
        public Game TeamGame { get; set; }
        public string? LftDescription { get; set; }
        public ICollection<User>? Members { get; set; }
        public User Captain { get; set; }
        public ICollection<Tournament>? Tournaments { get; set; }
        public ICollection<Match>? Matches { get; set; }
        public ICollection<MemberApplication>? MemberApplications { get; set; }
        public ICollection<TournamentApplication>? TournamentApplications { get; set; }
    }
}
