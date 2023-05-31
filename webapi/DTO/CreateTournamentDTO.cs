using webapi.Model;

namespace webapi.DTO
{
    public class CreateTournamentDTO
    {
        public string Name { get; set; }
        public int GameId { get; set; }
        public int MaxParticipants { get; set; }

    }
}
