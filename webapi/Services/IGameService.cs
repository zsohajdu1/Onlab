using webapi.Context;
using webapi.DTO;
using webapi.Model;

namespace webapi.Services
{
    public interface IGameService
    {
        public void CreateGame(CreateGameDTO createGameDTO);
        public List<GetGameDTO> GetGames();
    }
}
