using webapi.Model;

namespace webapi.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public List<string> TeamsName { get; set; }
        public List<int> TeamsId { get; set; }
        public int TournamentId { get; set; }
        public string? WinnerName { get; set; }
        public int? WinnerId {  get; set; }
        public bool Played { get; set; }
        public int? HorizontalDepth { get; set; }
        public int? VerticalDepth { get; set; }
    }
}
