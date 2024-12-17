using BlackJackLab.Models;
using BlackJackLab.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace BlackJackLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlackjackController : ControllerBase
    {
        private static BlackjackService _service;
        private static GameState? _currentGame;

        public BlackjackController(BlackjackService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetGame()
        {
            if (_currentGame != null)
            {
                return Ok(_currentGame);
            }
            else
            {
                return NotFound("No game started.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewGame()
        {
            _currentGame = await _service.CreateGameAsync();

            return Created("/api/Blackjack", _currentGame);
        }

        [HttpPost("play")]
        public async Task<IActionResult> Play([FromQuery]string action)
        {
            if (_currentGame == null)
            {
                return NotFound("There is no game in progress, please start a game first.");
            }

            if (action == "hit")
            {
                Card drawnCard = await _service.DrawCardAsync(_currentGame.DeckId);
                _currentGame.PlayerScore += Card.GetCardValue(drawnCard);

                _currentGame.PlayerCards.Add(drawnCard);

                if (_currentGame.PlayerScore > 21)
                {
                    _currentGame.GameOver = true;
                    _currentGame.Outcome = "Bust";
                }
                else if (_currentGame.PlayerScore == 21)
                {
                    _currentGame.GameOver = true;
                    _currentGame.Outcome = "Win";
                }

            }

            else if (action == "stand")
            {
                while (_currentGame.DealerScore < 17)
                {
                    Card drawnCard = await _service.DrawCardAsync(_currentGame.DeckId);

                    _currentGame.DealerCards.Add(drawnCard);
                    _currentGame.DealerScore += Card.GetCardValue(drawnCard);
                }

                _currentGame.GameOver = true;

                if (_currentGame.DealerScore > 21 ||_currentGame.PlayerScore > _currentGame.DealerScore)
                {
                    _currentGame.Outcome = "Win";
                }
                else if (_currentGame.PlayerScore == _currentGame.DealerScore)
                {
                    _currentGame.Outcome = "Standoff";
                }
                else
                {
                    _currentGame.Outcome = "Loss";
                }
            }

            else
            {
                return ValidationProblem($"Invalid action: {action}.");
            }

            return Ok();
        }
    }
}
