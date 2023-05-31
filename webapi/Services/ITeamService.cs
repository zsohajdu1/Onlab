using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public interface ITeamService
    {
        public void AcceptApplication(string userId, int teamId, int applicationId);
        public void ApplyTeam(string userId, int id);
        public void ChangeTeam(string userId, TeamDetailDTO team);
        public int CreateTeam(CreateTeamDTO createTeamDTO, string id);
        public void DenyApplication(string userId, int teamId, int applicationId);
        public List<TeamListDTO> GetAllTeams(string? id, string? name, TeamStatus? status, int? gameId);
        public List<MemberApplicationDTO> GetApplications(int teamId);
        public List<TeamListDTO> GetMyTeams(string id, string? name, TeamStatus? status, int? gameId);
        public TeamDetailDTO GetTeam(string userId, int id);
        public void RemovePlayer(int teamId, string id);
    }
}
