using webapi.Model;

namespace webapi.DTO
{
    public class CreateTournamentDTO
    {
        public string Name { get; set; }
        public string GameId { get; set; }
        public TournamentFormat Format { get; set; }
        public int MaxParticipants { get; set; }

    }
}
