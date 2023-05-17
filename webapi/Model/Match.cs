namespace webapi.Model
{
    public class Match
    {
        public int Id { get; set; }
        public bool Played { get; set; }
        public Team? Winner { get; set; }
        public string Result { get; set; }
        public ICollection<Team>? Teams { get; set; }
        public Tournament TournamentOfMatch { get; set; }
        public DateTime Date { get; set; }
    }
}
