using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    
    public class GameService : IGameService
    {

        private OnlabProjectContext dB;
        private readonly IMapper mapper;

        public GameService(OnlabProjectContext dB, IMapper mapper)
        {
            this.dB = dB;
            this.mapper = mapper;
        }
        public void CreateGame(CreateGameDTO createGameDTO)
        {
            var convertedGame = mapper.Map<Game>(createGameDTO);
            dB.Add(convertedGame);
            dB.SaveChanges();
        }

        public List<GetGameDTO> GetGames()
        {
            List<GetGameDTO> result = new();
            var games = dB.Games.Include(g => g.Tournaments).ToList();
            foreach (var game in games)
            {
                var convertedGame = mapper.Map<GetGameDTO>(game);
                if (game.Tournaments == null) convertedGame.TournamentCount = 0;
                else convertedGame.TournamentCount = game.Tournaments.Count;
                result.Add(convertedGame);
            }
            return result;
        }
    }
}
