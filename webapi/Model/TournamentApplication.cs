namespace webapi.Model
{
    public class TournamentApplication
    {
        public int Id { get; set; }
        public Tournament Tournament { get; set; }
        public Team Team { get; set; }
        public bool IsCompleted { get; set; }
    }
}
