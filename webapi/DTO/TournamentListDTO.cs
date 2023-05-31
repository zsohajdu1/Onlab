using webapi.Model;

namespace webapi.DTO
{
    public class TournamentListDTO
    {
        public int Id { get; set; }
        public string Game { get; set; }
        public string Name { get; set; }
        public TournamentStatus Status { get; set; }
        public string Organizer { get; set; }
        public int MaxParticipants { get; set; }
        public int Participants { get; set; }
        public string GameIcon { get; set; }

    }
}
