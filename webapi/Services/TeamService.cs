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

        public List<TeamListDTO> GetMyTeams(string id)
        {
            if (id == null) return null;
            List<TeamListDTO> convertedTeams = new();
            var teams = dB.Users
                .Where(u => u.Id.Equals(id))
                .SelectMany(u => u.Teams)
                .Include(t => t.TeamGame)
                .ToList();
            foreach (var team in teams)
            {
                var convertedTeam = mapper.Map<TeamListDTO>(team);
                convertedTeam.Game = team.TeamGame.Name;
                convertedTeams.Add(convertedTeam);
            }
            return convertedTeams;
        }

        public void CreateTeam(CreateTeamDTO createTeamDTO, string id)
        {
            var convertedTeam = mapper.Map<Team>(createTeamDTO);
            convertedTeam.TeamGame = dB.Games.FirstOrDefault(g => g.Id.Equals(createTeamDTO.TeamGameId));
            convertedTeam.Status = TeamStatus.FULL;
            //convertedTeam.Captain = dB.Users.FirstOrDefault(u => u.Id.Equals(id));
            dB.Add(convertedTeam);
            dB.SaveChanges();
        }

        public TeamDetailDTO GetTeam(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
