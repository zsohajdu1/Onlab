using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public interface ITeamService
    {
        public void CreateTeam(CreateTeamDTO createTeamDTO);
        public List<TeamListDTO> GetAllTeams();
    }
}
