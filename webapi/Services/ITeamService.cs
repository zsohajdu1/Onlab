using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public interface ITeamService
    {
        public void CreateTeam(CreateTeamDTO createTeamDTO, string id);
        public List<TeamListDTO> GetAllTeams();
        public List<TeamListDTO> GetMyTeams(string userId);
        public TeamDetailDTO GetTeam(string userId);
    }
}
