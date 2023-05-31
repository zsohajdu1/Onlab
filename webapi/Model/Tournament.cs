namespace webapi.Model
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxParticipants { get; set; }
        public Game TournamentGame { get; set; }
        public User Owner { get; set; }
        public TournamentStatus Status { get; set; }
        public TournamentFormat Format { get; set; }
        public ICollection<Team>? Teams { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<TournamentApplication>? TournamentApplications { get; set; }

    }
}
