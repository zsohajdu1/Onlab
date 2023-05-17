using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public class TeamService : ITeamService
    {
        private OnlabProjectContext dB;
        private readonly IMapper mapper;

        public TeamService(OnlabProjectContext _dB, IMapper _mapper)
        {
            dB = _dB;
            mapper = _mapper;
        }

        public List<TeamListDTO> GetAllTeams()
        {
            List<TeamListDTO> convertedTeams = new(); 
            var teams = dB.Teams.Include(t => t.TeamGame).ToList();
            foreach (var team in teams)
            {
                var convertedTeam = mapper.Map<TeamListDTO>(team);
                convertedTeam.Game = team.TeamGame.Name;
                convertedTeams.Add(convertedTeam);
            }
            return convertedTeams;
        }

        public void CreateTeam(CreateTeamDTO createTeamDTO)
        {
            var convertedTeam = mapper.Map<Team>(createTeamDTO);
            convertedTeam.TeamGame = dB.Games.FirstOrDefault(g => g.Id.Equals(createTeamDTO.TeamGameId));
            convertedTeam.Status = TeamStatus.FULL;
            dB.Add(convertedTeam);
            dB.SaveChanges();
        }
    }
}
