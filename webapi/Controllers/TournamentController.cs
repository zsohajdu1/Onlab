using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.DTO;
using webapi.Model;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;


        /*
        public void AcceptApplication(string userId, int tournamentId, int applicationId);
        public void DenyApplication(string userId, int tournamentId, int applicationId);
        public List<TournamentApplicationDTO> GetApplications(int tournamentId);
        public void RemoveTeam(int tournamentId, int teamId);
        public void ChangeTournament(string userId, TournamentDetailDTO tournament);*/
        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        [Route("")]
        public List<TournamentListDTO> GetAllTournaments(string? organizerName, string? name, TournamentStatus? status, int? gameId)
        {
            return _tournamentService.GetAllTournaments(organizerName, name, status, gameId);
        }

        [HttpGet]
        [Route("my")]
        public List<TournamentListDTO> GetMyTournaments(string? name, TournamentStatus? status, int? gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.GetMyTournaments(userId, name, status, gameId);
        }

        [HttpPost]
        [Route("create")]
        public int CreateTournament(CreateTournamentDTO createTournamentDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.CreateTournament(createTournamentDTO, userId);
        }

        [HttpGet]
        [Route("{id}")]
        public TournamentDetailDTO GetTournament(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.GetTournament(userId, id);
        }

        [HttpPost]
        [Route("{id}/start")]
        public void StartTournament (int id)
        {
            _tournamentService.StartTournament(id);
        }

        [HttpPost]
        [Route("matches/{matchId}/{winnerId}")]
        public void MatchWinner(int matchId, int winnerId)
        {
            _tournamentService.MatchWinner(matchId, winnerId);
        }

        [HttpPost]
        [Route("{tournamentId}/apply/{teamId}")]
        public void ApplyTournament(int teamId, int tournamentId)
        {
            _tournamentService.ApplyTournament(teamId, tournamentId);
        }

        [HttpPost]
        [Route("{tournamentId}/accept/{applicationId}")]
        public void AcceptApplication(int tournamentId, int applicationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _tournamentService.AcceptApplication(userId, tournamentId, applicationId);
        }
        [HttpPost]
        [Route("{tournamentId}/deny/{applicationId}")]
        public void DenyApplication(int tournamentId, int applicationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _tournamentService.DenyApplication(userId, tournamentId, applicationId);
        }
        [HttpGet]
        [Route("{tournamentId}/applications")]
        public List<TournamentApplicationDTO> GetApplications(int tournamentId)
        {
            return _tournamentService.GetApplications(tournamentId);
        }
        [HttpPatch]
        [Route("{tournamentId}/remove/{teamId}")]
        public void RemoveTeam(int tournamentId, int teamId)
        {
            _tournamentService.RemoveTeam(tournamentId, teamId);
        }
        [HttpPatch]
        [Route("change")]
        public void ChangeTournament(TournamentDetailDTO tournament)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _tournamentService.ChangeTournament(userId, tournament);
        }
    }
}
