namespace BlackJackLab.Models
{
    public class GameState
    {
        private string _deckId;
        public string DeckId 
        { 
            get { return _deckId; }
            set { _deckId = value; }
        }

        public List<Card> DealerCards { get; set; } = new List<Card>();
        public List<Card> PlayerCards { get; set; } = new List<Card>();
        public int DealerScore { get; set; }
        public int PlayerScore { get; set; }
        public bool GameOver { get; set; }
        public string? Outcome { get; set; }

        public GameState()
        {
            _deckId = DeckId;
        }
    }
}
