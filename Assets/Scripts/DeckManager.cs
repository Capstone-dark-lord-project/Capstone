using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  // List to hold all cards in the deck
    public List<Card> deck = new List<Card>();

    public GameObject eventCardPrefab;
    public GameObject resourceCardPrefab;
    public GameObject itemCardPrefab;
    public GameObject actionCardPrefab;
    public GameObject defaultCardPrefab;

    private int cardIndex = 0;
    float cardWidth = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        InstantiateDeck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextCard();
        }
    }

    // Deck initialization
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

    // Load card to deck list
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

    // Log cards on load
    void LogLoadedCards<T>(T[] loadedCards) where T : Card
    {
        foreach (var card in loadedCards)
        {
            Debug.Log($"Loaded Card: {card.name} (Count: {card.count})");
        }
    }

    // Deck shuffle
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // Draw a card
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

    // Card names for logging
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

    // Card Instantiation
    void InstantiateCurrentCard(int cardIndex)
    {
        if (deck.Count > 0)
        {
            // GameObject previousCard = GameObject.FindGameObjectWithTag("Cards");
            // if (previousCard != null)
            // {
            //     Destroy(previousCard);
            // }

            Card currentCard = deck[cardIndex];

            GameObject currentCardPrefab = GetCardTypePrefab(currentCard);
            Vector3 cardPosition = new Vector3(Random.Range(0f, 0.2f), cardIndex * cardWidth, Random.Range(0f, 0.2f));
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 5.0f), 0f);

            GameObject instantiatedCard = Instantiate(currentCardPrefab, cardPosition, randomRotation);

            // Card Display
            CardDisplay cardDisplay = instantiatedCard.GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.card = currentCard;
                cardDisplay.DisplayCardInfo();
            }
            else
            {
                Debug.LogWarning("CardDisplay component not found on the instantiated object.");
            }
            instantiatedCard.transform.Rotate(new Vector3(-90f, 180f, 0f));
            Debug.Log($"Instantiating Card {cardIndex + 1}/{deck.Count}: {currentCard.cardName}");
        }
        else
        {
            Debug.LogWarning("Deck is empty. No cards to instantiate.");
        }
    }

    // Type logic
    GameObject GetCardTypePrefab(Card card)
    {
        if (card is EventCard)
        {
            return eventCardPrefab;
        }
        else if (card is ResourceCard)
        {
            return resourceCardPrefab;
        }
        else if (card is ItemCard)
        {
            return itemCardPrefab;
        }
        else if (card is ActionCard)
        {
            return actionCardPrefab;
        }
        else
        {
            return defaultCardPrefab;
        }
    }

    void LoadNextCard()
    {
        cardIndex++;

        if (cardIndex < deck.Count)
        {
            InstantiateCurrentCard(cardIndex);
        }
        else
        {
            Debug.LogWarning("No more cards in the deck.");
        }
    }

    // Instantiate whole deck
    void InstantiateDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            InstantiateCurrentCard(i);
        }
    }
}
