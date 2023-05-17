using AutoMapper;
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
            return mapper.Map<List<GetGameDTO>>(dB.Games);
        }
    }
}
