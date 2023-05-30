using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.DTO;
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
        public List<TournamentListDTO> GetAllTournaments()
        {
            return _tournamentService.GetAllTournaments();
        }

        [HttpGet]
        [Route("my")]
        public List<TournamentListDTO> GetMyTournaments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.GetMyTournaments(userId);
        }

        [HttpPost]
        [Route("create")]
        public void CreateTeam(CreateTournamentDTO createTournamentDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _tournamentService.CreateTournament(createTournamentDTO, userId);
        }

        [HttpGet]
        [Route("{id}")]
        public TournamentDetailDTO GetTournament()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _tournamentService.GetTournament(userId);
        }
    }
}
