using webapi.Model;

namespace webapi.DTO
{
    public class TournamentListDTO
    {
        public string Game { get; set; }
        public string Name { get; set; }
        public TournamentStatus Status { get; set; }
        public int MaxParticipants { get; set; }
        public int Participants { get; set; }

    }
}
