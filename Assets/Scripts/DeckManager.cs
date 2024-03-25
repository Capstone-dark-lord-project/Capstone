using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public TextMeshProUGUI deckCountText;

    public List<GameObject> instantiatedDeckCards = new List<GameObject>();
    public List<Card> deck = new List<Card>();

    public GameObject defaultCardPrefab;

    float cardWidth = 0.01f;

    void Start()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        InitializeDeck();
        ShuffleDeck();
        InstantiateDeck();
        UpdateDeckCountUI();
        for (int i = 0; i < 1; i++)
        {
            DrawCard(playerManager);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();

            if (playerManager != null)
            {
                DrawCard(playerManager);
            }
            else
            {
                Debug.LogWarning("TempPlayerManager not found in the scene!");
            }
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
    public Card DrawCard(PlayerManager playerManager)
    {        
        if (deck.Count > 0)
        {
            Card drawnCard = deck[0];
            
            deck.RemoveAt(0);
            UpdateDeckCountUI();
            DestroyTopCard();
            playerManager.AddCardToHand(drawnCard);
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
            Card currentCard = deck[cardIndex];

            Vector3 cardPosition = new Vector3(Random.Range(0f, 0.2f), cardIndex * cardWidth, Random.Range(0f, 0.2f));
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 5.0f), 0f);

            GameObject instantiatedCard = Instantiate(defaultCardPrefab, cardPosition, randomRotation);

            instantiatedCard.transform.Rotate(new Vector3(-90f, 180f, 0f));

            // Add the instantiated card to the list
            instantiatedDeckCards.Add(instantiatedCard);

            Debug.Log($"Instantiating Card {cardIndex + 1}/{deck.Count}: {currentCard.cardName}");
        }
        else
        {
            Debug.LogWarning("Deck is empty. No cards to instantiate.");
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

    // Deck Count UI
    void UpdateDeckCountUI()
    {
        string deckCountString = "Deck count: " + deck.Count;

        deckCountText.text = deckCountString;
    }

    void DestroyTopCard()
    {
        if (instantiatedDeckCards.Count > 0)
        {
            // Get the index of the last card in the list
            int lastIndex = instantiatedDeckCards.Count - 1;

            // Get the GameObject of the last card
            GameObject topCard = instantiatedDeckCards[lastIndex];

            // Destroy the last card GameObject
            Destroy(topCard);

            // Remove the destroyed card from the list
            instantiatedDeckCards.RemoveAt(lastIndex);
        }
        else
        {
            Debug.LogWarning("No cards instantiated to destroy.");
        }
    }
}
