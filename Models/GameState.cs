namespace BlackJackLab.Models
{
    public class GameState
    {
        public string DeckId { get; }
        // The type "Card" will be part of the model you generate for the
        // Deck of Cards API.
        //public List<Card> DealerCards { get; set; } = new List<Card>();
        //public List<Card> PlayerCards { get; set; } = new List<Card>();
        public int DealerScore { get; set; }
        public int PlayerScore { get; set; }
        public bool GameOver { get { return Outcome != null; } }
        public string? Outcome { get; set; }

        public GameState(string deckId)
        {
            DeckId = deckId;
        }
    }
}
