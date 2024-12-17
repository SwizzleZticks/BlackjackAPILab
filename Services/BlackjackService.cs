using BlackJackLab.Models;
using System.Text.Json;

namespace BlackJackLab.Services
{
    public class BlackjackService
    {
        private HttpClient _client;

        public BlackjackService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck");
        }

        public async Task<GameState> CreateGameAsync()
        {
            var response = await _client.GetStringAsync($"{_client.BaseAddress}/new/shuffle/?deck_count=6");

            var json = JsonDocument.Parse(response);
            string? deckId = json.RootElement.GetProperty("deck_id").GetString();

            var drawResponse = await _client.GetStringAsync($"{_client.BaseAddress}/{deckId}/draw/?count=3");
            var drawJson = JsonDocument.Parse(drawResponse);
            var cardsJson = drawJson.RootElement.GetProperty("cards").EnumerateArray().ToList();

            var cardsConverted = Card.ConvertToCardList(cardsJson);


            GameState _newGame = new GameState
            {
                DeckId = deckId,
                DealerCards = new List<Card> { cardsConverted[0] },
                PlayerCards = new List<Card> { cardsConverted[1], cardsConverted[2] }
            };

            _newGame.DealerScore = Card.GetHandValue(_newGame.DealerCards);
            _newGame.PlayerScore = Card.GetHandValue(_newGame.PlayerCards);

            return _newGame;    
        }

        public async Task<Card> DrawCardAsync(string deckId)
        {
            var drawResponse = await _client.GetStringAsync($"{_client.BaseAddress}/{deckId}/draw/?count=1");
            var drawJson = JsonDocument.Parse(drawResponse);
            var cardJson = drawJson.RootElement.GetProperty("cards")[0];

            Card cardDrawn = Card.ConvertToCard(cardJson);

            return cardDrawn;
        }
    }
}
