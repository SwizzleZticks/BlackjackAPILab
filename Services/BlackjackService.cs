namespace BlackJackLab.Services
{
    public class BlackjackService
    {
        private readonly HttpClient _client;

        public BlackjackService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck");
        }
    }
}
