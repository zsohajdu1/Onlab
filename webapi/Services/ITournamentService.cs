using webapi.DTO;

namespace webapi.Services
{
    public interface ITournamentService
    {
        public void CreateTournament(CreateTournamentDTO createTournamentDTO, string userId);
        public List<TournamentListDTO> GetAllTournaments();
        public List<TournamentListDTO> GetMyTournaments(string id);
        public TournamentDetailDTO GetTournament(string userId);
    }
}
