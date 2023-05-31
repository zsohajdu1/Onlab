using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
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

        public List<TeamListDTO> GetAllTeams(string? id, string? name, TeamStatus? status, int? gameId)
        {
            List<TeamListDTO> convertedTeams = new(); 
            var teams = dB.Teams
                .Include(t => t.TeamGame)
                .Include(t => t.Members)
                .ToList();
            if (name != null) teams = teams.Where(t => t.Name.Contains(name)).ToList();
            if (status != null) teams = teams.Where(t => t.Status == status).ToList();
            if (gameId != null) teams = teams.Where(t => t.TeamGame.Id == gameId).ToList();
            foreach (var team in teams)
            {
                var convertedTeam = mapper.Map<TeamListDTO>(team);
                convertedTeam.Game = team.TeamGame.Name;
                convertedTeam.GameIcon = team.TeamGame.Icon;
                convertedTeam.GameMember = team.TeamGame.TeamSize;
                convertedTeam.MemberCount = team.Members.Count();
                convertedTeams.Add(convertedTeam);
            }
            return convertedTeams;
        }

        public List<TeamListDTO> GetMyTeams(string id, string? name, TeamStatus? status, int? gameId)
        {
            if (id == null) return null;
            List<TeamListDTO> convertedTeams = new();
            var teams = dB.Users
                .Where(u => u.Id.Equals(id))
                .SelectMany(u => u.Teams)
                .Include(t => t.TeamGame)
                .Include(t => t.Members)
                .ToList();
            if (name != null) teams = teams.Where(t => t.Name.Contains(name)).ToList();
            if (status != null) teams = teams.Where(t => t.Status == status).ToList();
            if (gameId != null) teams = teams.Where(t => t.TeamGame.Id == gameId).ToList();
            foreach (var team in teams)
            {
                var convertedTeam = mapper.Map<TeamListDTO>(team);
                convertedTeam.Game = team.TeamGame.Name;
                convertedTeam.GameIcon = team.TeamGame.Icon;
                convertedTeam.GameMember = team.TeamGame.TeamSize;
                convertedTeam.MemberCount = team.Members.Count();
                convertedTeams.Add(convertedTeam);
            }
            return convertedTeams;
        }

        public int CreateTeam(CreateTeamDTO createTeamDTO, string id)
        {
            var convertedTeam = mapper.Map<Team>(createTeamDTO);
            convertedTeam.TeamGame = dB.Games.FirstOrDefault(g => g.Id.Equals(createTeamDTO.TeamGameId));
            convertedTeam.Status = TeamStatus.LFT;
            var captain = dB.Users.FirstOrDefault(u => u.Id.Equals(id));
            convertedTeam.Captain = captain;
            convertedTeam.Members = new List<User>
            {
                captain
            };
            dB.Add(convertedTeam);
            dB.SaveChanges();
            return convertedTeam.Id;
        }

        public TeamDetailDTO GetTeam(string userId, int id)
        {
            var team = dB.Teams
                .Include(t => t.TeamGame)
                .Include(t => t.Captain)
                .Include(t => t.Matches)
                .Include(t => t.Tournaments)
                .Include(t => t.Members)
                .FirstOrDefault(t => t.Id == id);

            var convertedTeam = mapper.Map<TeamDetailDTO>(team);

            convertedTeam.TeamGame = team.TeamGame.Name;
            convertedTeam.GameIcon = team.TeamGame.Icon;
            convertedTeam.CaptainId = team.Captain.Id;

            convertedTeam.TournamentsId = new List<int>();
            foreach (var tournament in team.Tournaments)
            {
                convertedTeam.TournamentsId.Add(tournament.Id);
            }

            convertedTeam.MatchesId = new List<int>();
            foreach (var match in team.Matches)
            {
                convertedTeam.MatchesId.Add(match.Id);
            }

            convertedTeam.MembersId = new List<string>();
            convertedTeam.MembersName = new List<string>();
            foreach (var member in team.Members)
            {
                convertedTeam.MembersId.Add(member.Id);
                convertedTeam.MembersName.Add(member.UserName);
            }
            return convertedTeam;
        }

        public void ChangeTeam(string userId, TeamDetailDTO team)
        {
            var changedTeam = dB.Teams.Include(t => t.Captain).FirstOrDefault(t => t.Id == team.Id);
            if (changedTeam.Captain.Id == team.CaptainId)
            {
                changedTeam.Status = team.Status;
                changedTeam.Name = team.Name;
                changedTeam.LftDescription = team.LftDescription;
            }
            dB.Update(changedTeam);
            dB.SaveChanges();
        }

        public void ApplyTeam(string userId, int id)
        {
            var team = dB.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamGame)
                .FirstOrDefault(t => t.Id == id);
            var appliant = dB.Users.FirstOrDefault(u => u.Id == userId);
            var possibleApplication = dB.MemberApplications
                .Include(mb => mb.User)
                .Include(mb => mb.Team)
                .FirstOrDefault(mb => mb.User.Id == userId && mb.Team.Id == id && !mb.IsCompleted);
            if (!team.Members.Contains(appliant) && team.Members.Count != team.TeamGame.TeamSize && possibleApplication == null)
            {
                MemberApplication application = new();
                application.Team = team;
                application.User = appliant;
                application.IsCompleted = false;
                dB.Add(application);
                dB.SaveChanges();
            }
        }

        public void AcceptApplication(string userId, int teamId, int applicationId)
        {
            var application = dB.MemberApplications
                .Include(mb => mb.User)
                .Include(mb => mb.Team)
                .FirstOrDefault(mb => mb.Id == applicationId);
            var appliant = dB.Users.FirstOrDefault(u => u.Id == application.User.Id);
            var team = dB.Teams
                .Include(t => t.Members)
                .Include(t => t.Captain)
                .Include(t => t.TeamGame)
                .FirstOrDefault(t => t.Id == teamId);
            var acceptor = dB.Users.FirstOrDefault(u => u.Id == userId);
            var isValid = acceptor.Id == team.Captain.Id 
                && team.Members.Count != team.TeamGame.TeamSize
                && !team.Members.Contains(appliant)
                && !application.IsCompleted;
            if (isValid)
            {
                team.Members.Add(appliant);
                dB.Update(team);
                application.IsCompleted = true;
                dB.Update(application);
                dB.SaveChanges();
            }

        }

        public void DenyApplication(string userId, int teamId, int applicationId)
        {
            var application = dB.MemberApplications
                .FirstOrDefault(mb => mb.Id == applicationId);
            var team = dB.Teams
                .Include(t => t.Captain)
                .FirstOrDefault(t => t.Id == teamId);
            if (team.Captain.Id == userId) application.IsCompleted = true;
            dB.Update(application);
            dB.SaveChanges();
        }

        public List<MemberApplicationDTO> GetApplications(int teamId)
        {
            var applications = dB.MemberApplications
                .Include(mb => mb.User)
                .Include(mb => mb.Team)
                .Where(mb => mb.Team.Id == teamId && !mb.IsCompleted);
            List<MemberApplicationDTO> convertedApplications = new();
            foreach(var application in applications)
            {
                var convertedApplication = mapper.Map<MemberApplicationDTO>(application);
                convertedApplication.UserName = application.User.UserName;
                convertedApplication.UserId = application.User.Id;
                convertedApplication.TeamId = application.Team.Id;
                convertedApplications.Add(convertedApplication);
            }
            return convertedApplications;

        }

        public void RemovePlayer(int teamId, string id)
        {
            var team = dB.Teams
                .Include(t => t.Members)
                .Include(t => t.Captain)
                .FirstOrDefault(t => t.Id == teamId);
            var user = dB.Users.FirstOrDefault(u => u.Id == id);
            if (team.Members.Contains(user) && team.Captain.Id != id)
            {
                team.Members.Remove(user);
                dB.Update(team);
                dB.SaveChanges();
            }
        }
    }
}
