using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public interface ITournamentService
    {
        public void CreateTournament(CreateTournamentDTO createTournamentDTO, string userId);
        public List<TournamentListDTO> GetAllTournaments(string? organizerName, string? name, TournamentStatus? status, int? gameId);
        public List<TournamentListDTO> GetMyTournaments(string id, string? name, TournamentStatus? status, int? gameId);
        public TournamentDetailDTO GetTournament(string userId, int id);
        public void StartTournament(int id);
        public void MatchWinner(int matchId, int winnerId);
        public void ApplyTournament(int teamId, int tournamentId);
        public void AcceptApplication(string userId, int tournamentId, int applicationId);
        public void DenyApplication(string userId, int tournamentId, int applicationId);
        public List<TournamentApplicationDTO> GetApplications(int tournamentId);
        public void RemoveTeam(int tournamentId, int teamId);
    }
}
