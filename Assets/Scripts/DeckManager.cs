using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
    // Load and add Event Cards
    EventCard[] eventCards = Resources.LoadAll<EventCard>("Cards/Events");
    deck.AddRange(eventCards);

    // Load and add Resource Cards
    ResourceCard[] resourceCards = Resources.LoadAll<ResourceCard>("Cards/Resources");
    deck.AddRange(resourceCards);

    // Load and add Item Cards
    ItemCard[] itemCards = Resources.LoadAll<ItemCard>("Cards/Items");
    deck.AddRange(itemCards);

    // Load and add Action Cards
    ActionCard[] actionCards = Resources.LoadAll<ActionCard>("Cards/Actions");
    deck.AddRange(actionCards);

    Debug.Log($"Deck initialized with {deck.Count} cards.");
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
}
