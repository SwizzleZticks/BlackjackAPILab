using System.Text.Json;

namespace BlackJackLab.Models
{
    public class Card
    {
        public string Suit { get; set; }
        public string Value { get; set; }

        public static List<Card> ConvertToCardList(List<JsonElement> cards)
        {
            List<Card> convertedCards = new List<Card>();

            foreach (JsonElement card in cards)
            {
                string? suit = card.GetProperty("suit").GetString();
                string? value = card.GetProperty("value").GetString();

                Card newCard = new Card
                {
                    Suit = suit,
                    Value = value
                };

                convertedCards.Add(newCard);
            }

            return convertedCards;
        }

        public static Card ConvertToCard(JsonElement card)
        {
            Card newCard = new Card
            {
                Suit = card.GetProperty("suit").GetString(),
                Value = card.GetProperty("value").GetString()
            };

            return newCard;
        }

        public static int GetCardValue(Card aCard)
        {
            bool isNumberCard = int.TryParse(aCard.Value, out int value);

            if (!isNumberCard)
            {
                if (aCard.Value == "ACE")
                {
                    return 11;
                }
                else
                {
                    return 10;
                }
            }
            else
            {
                return value;
            }
        }

        public static int GetHandValue(List<Card> cards)
        {
            int total = 0;
            int aceCount = 0;

            foreach(Card aCard in cards)
            {
                bool isNumberCard = int.TryParse(aCard.Value, out int value);

                if (!isNumberCard)
                {
                    if (aCard.Value == "ACE")
                    {
                        aceCount++;
                        total += 11;
                    }
                    else
                    {
                        total += 10;
                    }
                }
                else
                {
                    total += value;
                }
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }
}
