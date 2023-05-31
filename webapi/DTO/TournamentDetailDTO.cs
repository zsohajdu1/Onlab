using webapi.Model;

namespace webapi.DTO
{
    public class TournamentDetailDTO
    {
        public string GameName { get; set; }
        public string GameIcon { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public TournamentStatus Status { get; set; }
        public bool IsOwner { get; set; }
        public int MaxParticipants { get; set; }
        public List<string>? TeamsName { get; set; }
        public List<int>? TeamsId { get; set; }
        public List<MatchDTO>? Matches { get; set; }

    }
}
