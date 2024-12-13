using BlackJackLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlackJackLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlackjackController : ControllerBase
    {
        // This holds the current game state
        private static GameState? _gameState;
        private static HttpClient _httpClient = new HttpClient();

        private const string BASE_URL = "https://deckofcardsapi.com/api/deck";

        public BlackjackController()
        {
        }

        [HttpGet]
        public IActionResult GetGame()
        {
            if (_gameState != null)
            {
                return Ok(_gameState);
            }
            else
            {
                return NotFound("No game started.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewGame()
        {
            // TODO create new GameState instance, set starting values,
            // and set _gameState.
            return Created("/api/Blackjack", _gameState);
        }

        [HttpPost("play")]
        public async Task<IActionResult> Play(string action)
        {
            // TODO handle each action.
            // Else, respond "Invalid action"
            return ValidationProblem($"Invalid action: {action}.");
        }
    }
}
