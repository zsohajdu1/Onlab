using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.DTO;
using System.Security.Claims;

namespace webapi.Controllers
{

    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        [Route("")]
        public List<TeamListDTO>GetAllTeams()
        {
            return _teamService.GetAllTeams();
        }

        [HttpGet]
        [Route("my")]
        public List<TeamListDTO> GetMyTeams()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.GetMyTeams(userId);
        }

        [HttpPost]
        [Route("create")]
        public void CreateTeam(CreateTeamDTO createTeamDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _teamService.CreateTeam(createTeamDTO, userId);
        }

        [HttpGet]
        [Route("{id}")]
        public TeamDetailDTO GetTeam()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.GetTeam(userId);
        }
    }
}