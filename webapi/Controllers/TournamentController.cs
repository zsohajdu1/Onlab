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
        public void CreateTournament(CreateTournamentDTO createTournamentDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _tournamentService.CreateTournament(createTournamentDTO, userId);
        }

        [HttpGet]
        [Route("{id}")]
        public TournamentDetailDTO GetTournament(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.GetTournament(userId, id);
        }

        [HttpPatch]
        [Route("{id}/start")]
        public void StartTournament (int id)
        {
            _tournamentService.StartTournament(id);
        }
    }
}
