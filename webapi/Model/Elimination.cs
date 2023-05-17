namespace webapi.Model
{
    public class Elimination : TournamentFormat
    {
        public ICollection<Match>? Matches { get; set; }
    }
}
