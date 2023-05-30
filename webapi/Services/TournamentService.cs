using AutoMapper;
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
        public List<TournamentListDTO> GetAllTournaments()
        {
            List<TournamentListDTO> convertedTournaments = new();
            var tournaments = dB.Tournaments.Include(t => t.TournamentGame).Include(t => t.Teams).ToList();
            foreach (var tournament in tournaments)
            {
                var convertedTournament = mapper.Map<TournamentListDTO>(tournament);
                convertedTournament.Game = tournament.TournamentGame.Name;
                if (tournament.Teams != null)
                    convertedTournament.Participants = tournament.Teams.ToList().Count();
                else convertedTournament.Participants = 0;
                convertedTournaments.Add(convertedTournament);
            }
            return convertedTournaments;
        }

        public List<TournamentListDTO> GetMyTournaments(string id)
        {
            if (id == null) return null;
            List<TournamentListDTO> convertedTournaments = new();
            var tournaments = dB.Users
                .Where(u => u.Id.Equals(id))
                .SelectMany(u => u.Tournaments)
                .Include(t => t.TournamentGame)
                .Include(t => t.Teams)
                .ToList();
            foreach (var tournament in tournaments)
            {
                var convertedTournament = mapper.Map<TournamentListDTO>(tournament);
                convertedTournament.Game = tournament.TournamentGame.Name;
                if (tournament.Teams != null)
                    convertedTournament.Participants = tournament.Teams.ToList().Count();
                else convertedTournament.Participants = 0;
                convertedTournaments.Add(convertedTournament);
            }
            return convertedTournaments;
        }

        public void CreateTournament(CreateTournamentDTO createTournamentDTO, string userId)
        {

            var convertedTournament = mapper.Map<Tournament>(createTournamentDTO);
            convertedTournament.TournamentGame = dB.Games.FirstOrDefault(g => g.Id.Equals(createTournamentDTO.GameId));
            convertedTournament.Status = TournamentStatus.UPCOMING;
            dB.Add(convertedTournament);
            dB.SaveChanges();
        }

        public TournamentDetailDTO GetTournament(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
