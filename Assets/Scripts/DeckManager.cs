using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  // List to hold all cards in the deck
    public List<Card> deck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    void InitializeDeck()
    {
        EventCard[] eventCards = Resources.LoadAll<EventCard>("Scriptables/Cards/Events");
        LoadAndAddCards(eventCards);

        ResourceCard[] resourceCards = Resources.LoadAll<ResourceCard>("Scriptables/Cards/Resources");
        LoadAndAddCards(resourceCards);

        ItemCard[] itemCards = Resources.LoadAll<ItemCard>("Scriptables/Cards/Items");
        LoadAndAddCards(itemCards);

        ActionCard[] actionCards = Resources.LoadAll<ActionCard>("Scriptables/Cards/Actions");
        LoadAndAddCards(actionCards);

        Debug.Log($"Deck initialized with {deck.Count} cards: {GetCardNamesList()}");
    }

    void LoadAndAddCards<T>(T[] cards) where T : Card
    {
        if (cards.Length == 0)
        {
            Debug.LogWarning($"No {typeof(T).Name} found!");
        }
        else
        {
            foreach (T card in cards)
            {
                for (int i = 0; i < card.count; i++)
                {
                    deck.Add(card);
                }
            }
            LogLoadedCards(cards);
        }
    }

    void LogLoadedCards<T>(T[] loadedCards) where T : Card
    {
        foreach (var card in loadedCards)
        {
            Debug.Log($"Loaded Card: {card.name} (Count: {card.count})");
        }
    }

    void ShuffleDeck()
    {
        // Shuffle the deck
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // Method to draw a card from the deck
    public Card DrawCard()
    {
        if (deck.Count > 0)
        {
            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            return drawnCard;
        }
        else
        {
            Debug.LogWarning("Deck is empty!");
            return null;
        }
    }

    string GetCardNamesList()
    {
        StringBuilder cardNamesList = new StringBuilder();
        for (int i = 0; i < deck.Count; i++)
        {
            cardNamesList.Append(deck[i].cardName);
            if (i < deck.Count - 1)
            {
                cardNamesList.Append(", ");
            }
        }
        return cardNamesList.ToString();
    }
}
