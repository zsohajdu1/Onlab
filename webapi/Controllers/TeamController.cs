using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.DTO;
using System.Security.Claims;
using webapi.Model;

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
        public List<TeamListDTO>GetAllTeams(string? name, TeamStatus? status, int? gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.GetAllTeams(userId, name, status, gameId);
        }

        [HttpGet]
        [Route("my")]
        public List<TeamListDTO> GetMyTeams(string? name, TeamStatus? status, int? gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.GetMyTeams(userId, name, status, gameId);
        }

        [HttpPost]
        [Route("create")]
        public int CreateTeam(CreateTeamDTO createTeamDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.CreateTeam(createTeamDTO, userId);
        }

        [HttpGet]
        [Route("{id}")]
        public TeamDetailDTO GetTeam(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _teamService.GetTeam(userId, id);
        }

        [HttpPatch]
        [Route("change")]
        public void ChangeTeam(TeamDetailDTO team)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _teamService.ChangeTeam(userId, team);
        }

        [HttpPost]
        [Route("{id}/apply")]
        public void ApplyTeam(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _teamService.ApplyTeam(userId, id);
        }

        [HttpPatch]
        [Route("{teamId}/accept/{applicationId}")]
        public void AcceptApplication(int teamId, int applicationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _teamService.AcceptApplication(userId, teamId, applicationId);
        }

        [HttpPatch]
        [Route("{teamId}/deny/{applicationId}")]
        public void DenyApplication(int teamId, int applicationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _teamService.DenyApplication(userId, teamId, applicationId);
        }

        [HttpGet]
        [Route("{teamId}/applications")]
        public List<MemberApplicationDTO> GetApplications(int teamId)
        {
            return _teamService.GetApplications(teamId);
        }
        [HttpPatch]
        [Route("{teamId}/remove/{playerId}")]
        public void RemovePlayer(string playerId, int teamId)
        {
            _teamService.RemovePlayer(teamId, playerId);
        }
    }
}