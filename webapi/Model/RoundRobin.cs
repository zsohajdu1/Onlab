namespace webapi.Model
{
    public class RoundRobin : TournamentFormat
    {
        public ICollection<Match>? Matches { get; set; }
    }
}
