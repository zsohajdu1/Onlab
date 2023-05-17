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

        [HttpPost]
        [Route("create")]
        public void CreateTeam(CreateTeamDTO createTeamDTO)
        {
            _teamService.CreateTeam(createTeamDTO);
        }
    }
}