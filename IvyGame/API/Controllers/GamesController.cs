using IvyGame.Data;
using IvyGame.Models.Domain;
using IvyGame.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IvyGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        public IvyGameContext Context { get; }
        public GamesController(IvyGameContext context)
        {
            Context = context;
        }
        //GET/api/games
        //GET/api/games?name=pacman
        public IEnumerable<GameDto> Index([FromQuery] string? name)
        {
            List<Game> games = string.IsNullOrEmpty(name)
                ? Context.Game.ToList()
                : Context.Game.Where(x => x.GameName.Contains(name)).ToList();

            var gameDtos = games.Select(game => new GameDto
            {
                GameId = game.GameId,
                GameName = game.GameName,

            });
            return gameDtos;
        }
        [HttpGet("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GameDto> GetGame(int gameId)
        {
            var game = Context.Game.FirstOrDefault(x => x.GameId == gameId);

            if (game == null)
            {
                // Sätter statuskod till 404 Not Found
                return NotFound();
            }

            var gameDto = new GameDto
            {
                GameId = game.GameId,
                GameName = game.GameName,

            };

            return gameDto;
        }

        [HttpPost]
        public IActionResult CreateGame(CreateGameDto createGameDto)
        {
            var game = new Game
            {

                GameName = createGameDto.GameName,
                Description = createGameDto.Description,
                Year = createGameDto.Year,
                Genre = createGameDto.Genre,
                ImageUrl = createGameDto.ImageUrl,
            };
            Context.Add(game);
            Context.SaveChanges();
            var gameDto = new GameDto
            {
                GameId = game.GameId,
                GameName = game.GameName,
            };
            //return Created($"/api/products/{product.Id}", null);
            return CreatedAtAction(
                nameof(GetGame),
                new { gameId = game.GameId },
                gameDto);
        }


        // DELETE /api/games/1
        [HttpDelete("{gameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteProduct(int gameId)
        {
            var game = Context.Game.FirstOrDefault(x => x.GameId == gameId);

            if (game != null)
            {
                Context.Game.Remove(game);

                Context.SaveChanges();
            }

            // Returnera 204 No Content
            return NoContent();
        }
    }
}
