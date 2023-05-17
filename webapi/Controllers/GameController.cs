using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.DTO;
using System.Security.Claims;

namespace webapi.Controllers
{

    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpPost]
        [Route("create")]
        public void CreateGame(CreateGameDTO createGameDTO)
        {
            _gameService.CreateGame(createGameDTO);
        }

        [HttpGet]
        [Route("")]
        public List<GetGameDTO> GetGames()
        {
            return _gameService.GetGames();
        }
    }
}