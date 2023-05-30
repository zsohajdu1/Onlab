using webapi.Model;

namespace webapi.DTO
{
    public class TournamentDetailDTO
    {
        public string Game { get; set; }
        public string Name { get; set; }
        public TournamentStatus Status { get; set; }
        public int MaxParticipants { get; set; }
        public List<TeamListDTO>? Participants { get; set; }

    }
}
