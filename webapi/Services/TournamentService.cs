﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public class TournamentService : ITournamentService
    {
        private OnlabProjectContext dB;
        private readonly IMapper mapper;

        public TournamentService(OnlabProjectContext _dB, IMapper _mapper)
        {
            dB = _dB;
            mapper = _mapper;
        }
        public List<TournamentListDTO> GetAllTournaments(string? organizerName, string? name, TournamentStatus? status, int? gameId)
        {
            List<TournamentListDTO> convertedTournaments = new();
            var tournaments = dB.Tournaments
                .Include(t => t.TournamentGame)
                .Include(t => t.Teams)
                .Include(t => t.Owner)
                .ToList();
            if (name != null) tournaments = tournaments.Where(t => t.Name.Contains(name)).ToList();
            if (status != null) tournaments = tournaments.Where(t => t.Status == status).ToList();
            if (gameId != null) tournaments = tournaments.Where(t => t.TournamentGame.Id == gameId).ToList();
            if (organizerName != null) tournaments = tournaments.Where(t => t.Owner.UserName.Contains(organizerName)).ToList();
            foreach (var tournament in tournaments)
            {
                var convertedTournament = mapper.Map<TournamentListDTO>(tournament);
                convertedTournament.Game = tournament.TournamentGame.Name;
                if (tournament.Teams != null)
                    convertedTournament.Participants = tournament.Teams.ToList().Count();
                else convertedTournament.Participants = 0;
                convertedTournament.Organizer = tournament.Owner.UserName;
                convertedTournament.GameIcon = tournament.TournamentGame.Icon;
                convertedTournaments.Add(convertedTournament);
            }
            return convertedTournaments;
        }

        public List<TournamentListDTO> GetMyTournaments(string id, string? name, TournamentStatus? status, int? gameId)
        {
            if (id == null) return null;
            List<TournamentListDTO> convertedTournaments = new();
            var tournaments = dB.Tournaments
                .Include(t => t.TournamentGame)
                .Include(t => t.Teams)
                .Include(t => t.Owner)
                .Where(t => t.Owner.Id == id)
                .ToList();
            if (name != null) tournaments = tournaments.Where(t => t.Name.Contains(name)).ToList();
            if (status != null) tournaments = tournaments.Where(t => t.Status == status).ToList();
            if (gameId != null) tournaments = tournaments.Where(t => t.TournamentGame.Id == gameId).ToList();
            foreach (var tournament in tournaments)
            {
                var convertedTournament = mapper.Map<TournamentListDTO>(tournament);
                convertedTournament.Game = tournament.TournamentGame.Name;
                if (tournament.Teams != null)
                    convertedTournament.Participants = tournament.Teams.ToList().Count();
                else convertedTournament.Participants = 0;
                convertedTournament.GameIcon = tournament.TournamentGame.Icon;
                convertedTournaments.Add(convertedTournament);
            }
            return convertedTournaments;
        }

        public void CreateTournament(CreateTournamentDTO createTournamentDTO, string userId)
        {

            var convertedTournament = mapper.Map<Tournament>(createTournamentDTO);
            convertedTournament.TournamentGame = dB.Games.FirstOrDefault(g => g.Id.Equals(createTournamentDTO.GameId));
            convertedTournament.Status = TournamentStatus.UPCOMING;
            convertedTournament.Format = new Elimination();
            dB.Add(convertedTournament);
            dB.SaveChanges();
        }

        public TournamentDetailDTO GetTournament(string userId, int id)
        {
            var tournament = dB.Tournaments
                .Include(t => t.TournamentGame)
                .Include(t => t.Teams)
                .Include(t => t.Teams)
                .Include(t => t.Owner)
                .Include(t => t.TournamentApplications)
                .FirstOrDefault(t => t.Id == id);

            var format = dB.TournamentFormats
                .Include(f => f.Matches)
                .Include(f => f.Tournament)
                .FirstOrDefault(f => f.Tournament.Id == id);

            var matches = dB.Matches
                .Include(m => m.TournamentOfMatch)
                .Include(m => m.Teams)
                .Include(m => m.Winner)
                .Where(m => m.TournamentOfMatch.Id == id);

            var convertedTournament = mapper.Map<TournamentDetailDTO>(tournament);
            convertedTournament.GameName = tournament.TournamentGame.Name;
            convertedTournament.GameId = tournament.TournamentGame.Id;
            convertedTournament.GameIcon = tournament.TournamentGame.Icon;

            convertedTournament.TeamsId = new List<int>();
            convertedTournament.TeamsName = new List<string>();
            foreach (var team in tournament.Teams)
            {
                convertedTournament.TeamsId.Add(team.Id);
                convertedTournament.TeamsName.Add(team.Name);
            }
            convertedTournament.Matches = new List<MatchDTO>();
            foreach (var match in matches)
            {
                var convertedMatch = mapper.Map<MatchDTO>(match);

                convertedMatch.TeamsId = new List<int>();
                convertedMatch.TeamsName = new List<string>();
                foreach (var team in match.Teams)
                {
                    convertedMatch.TeamsId.Add(team.Id);
                    convertedMatch.TeamsName.Add(team.Name);
                }
                convertedMatch.WinnerName = match.Winner.Name;
                convertedMatch.WinnerId = match.Winner.Id;
                convertedMatch.TournamentId = match.TournamentOfMatch.Id;
            }
            return convertedTournament;
        }

        public void StartTournament (int id)
        {
            var tournament = dB.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Format)
                .FirstOrDefault(t => t.Id == id);
            var teamsCount = tournament.Teams.Count;
            double verticalDepthDouble = Math.Log(teamsCount, 2);
            int verticalDepth = (int)Math.Ceiling(verticalDepthDouble);
            if (teamsCount == tournament.MaxParticipants)
            {
                for (int i = 0; i < (teamsCount / 2 - 1); i++)
                {
                    Match match = new();
                    match.Played = false;
                    match.TournamentOfMatch = tournament;
                    match.Teams = new List<Team>
                    {
                        tournament.Teams.ElementAt(i), tournament.Teams.ElementAt(teamsCount - i -1)
                    };
                    double horizontal = i / 2;
                    match.HorizontalDepth = (int)Math.Floor(horizontal);
                    match.VerticalDepth = verticalDepth - 1;
                    tournament.Format.Matches.Add(match);
                    dB.Add(match);
                }
                for (int i = 0; i< verticalDepth - 1; i++)
                {
                    for (int j = 0; j < Math.Pow(i, 2); j++)
                    {
                        Match match = new();
                        match.Played = false;
                        match.TournamentOfMatch = tournament;
                        match.Teams = new List<Team>();
                        double horizontal = j;
                        match.HorizontalDepth = (int)Math.Floor(horizontal);
                        match.VerticalDepth = i;
                        tournament.Format.Matches.Add(match);
                        dB.Add(match);
                    }
                }
                tournament.Status = TournamentStatus.INPROGRESS;
                dB.Update(tournament);
                dB.SaveChanges();
            }
            else
            {
                //possible new feature
                throw new NotImplementedException();
            }
        }
        public void MatchWinner (int matchId, int winnerId)
        {
            var match = dB.Matches
                .Include(m => m.TournamentOfMatch)
                .FirstOrDefault(m => m.Id == matchId);
            var winner = dB.Teams
                .FirstOrDefault(t => t.Id == winnerId);
            var tournament = dB.Tournaments
                .Include(t => t.Format)
                .Include(t => t.Format.Matches)
                .FirstOrDefault(t => t.Id == match.TournamentOfMatch.Id);
            if (!match.Played)
            {
                match.Winner = winner;
                match.Played = true;
                if (match.VerticalDepth == 0)
                {
                    tournament.Status = TournamentStatus.COMPLETED;
                }
                else
                {
                    double x = (double)match.HorizontalDepth / 2;
                    var newHorizontalDepth = (int)Math.Floor(x);
                    var newVerticalDepth = match.VerticalDepth - 1;
                    var newMatch = dB.Matches
                        .Include(m => m.TournamentOfMatch)
                        .Include(m => m.Teams)
                        .FirstOrDefault(m => m.TournamentOfMatch.Id == tournament.Id
                                           && m.VerticalDepth == newVerticalDepth
                                           && m.HorizontalDepth == newHorizontalDepth);
                    if (newMatch != null)
                    {
                        newMatch.Teams.Add(winner);
                        dB.Update(newMatch);
                    }
                }
                dB.Update(match);
                dB.SaveChanges();

            }
        }
        public void ApplyTournament(int teamId, int tournamentId)
        {
            var appliantTeam = dB.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamGame)
                .FirstOrDefault(t => t.Id == teamId);
            var tournament = dB.Tournaments
                .Include(t => t.Teams)
                .FirstOrDefault(t => t.Id == tournamentId);
            var possibleApplication = dB.TournamentApplications
                .Include(ta => ta.Tournament)
                .Include(ta => ta.Team)
                .FirstOrDefault(ta => ta.Team.Id == teamId && ta.Tournament.Id == tournamentId && !ta.IsCompleted);
            if (!tournament.Teams.Contains(appliantTeam) && tournament.Teams.Count != tournament.MaxParticipants && possibleApplication == null)
            {
                TournamentApplication application = new();
                application.Team = appliantTeam;
                application.Tournament = tournament;
                application.IsCompleted = false;
                dB.Add(application);
                dB.SaveChanges();
            }
        }

        public void AcceptApplication(string userId, int tournamentId, int applicationId)
        {
            var application = dB.TournamentApplications
                .Include(ta => ta.Team)
                .Include(ta => ta.Tournament)
                .FirstOrDefault(ta => ta.Id == applicationId);
            var appliantTeam = dB.Teams
                .Include(t => t.Members)
                .Include(t => t.TeamGame)
                .FirstOrDefault(t => t.Id == application.Team.Id);
            var tournament = dB.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Owner)
                .FirstOrDefault(t => t.Id == tournamentId);
            var acceptor = dB.Users.FirstOrDefault(u => u.Id == userId);
            var isValid = acceptor.Id == tournament.Owner.Id
                && tournament.Teams.Count != tournament.MaxParticipants
                && !tournament.Teams.Contains(appliantTeam)
                && !application.IsCompleted;
            if (isValid)
            {
                tournament.Teams.Add(appliantTeam);
                dB.Update(tournament);
                application.IsCompleted = true;
                dB.Update(application);
                dB.SaveChanges();
            }

        }

        public void DenyApplication(string userId, int tournamentId, int applicationId)
        {
            var application = dB.TournamentApplications
                .Include(ta => ta.Team)
                .Include(ta => ta.Tournament)
                .FirstOrDefault(ta => ta.Id == applicationId);
            var tournament = dB.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Owner)
                .FirstOrDefault(t => t.Id == tournamentId);
            if (tournament.Owner.Id == userId) application.IsCompleted = true;
            dB.Update(application);
            dB.SaveChanges();
        }

        public List<TournamentApplicationDTO> GetApplications(int tournamentId)
        {
            var applications = dB.TournamentApplications
                .Include(ta => ta.Tournament)
                .Include(ta => ta.Team)
                .Where(ta => ta.Tournament.Id == tournamentId && !ta.IsCompleted);
            List<TournamentApplicationDTO> convertedApplications = new();
            foreach (var application in applications)
            {
                var convertedApplication = mapper.Map<TournamentApplicationDTO>(application);
                convertedApplication.TeamName = application.Team.Name;
                convertedApplication.TeamId = application.Team.Id;
                convertedApplication.TournamentId = application.Tournament.Id;
                convertedApplications.Add(convertedApplication);
            }
            return convertedApplications;

    }

        public void RemoveTeam(int tournamentId, int teamId)
        {
            var team = dB.Teams.FirstOrDefault(t => t.Id == teamId);
            var tournament = dB.Tournaments.FirstOrDefault(t => t.Id == tournamentId);
            if (tournament.Teams.Contains(team))
            {
                tournament.Teams.Remove(team);
                dB.Update(tournament);
                dB.SaveChanges();
            }
        }
    }
}
