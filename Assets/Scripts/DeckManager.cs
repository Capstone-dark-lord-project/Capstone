using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DeckManager : MonoBehaviour
{
    public TextMeshProUGUI deckCountText;

    public List<GameObject> instantiatedDeckCards = new List<GameObject>();
    public List<Card> deck = new List<Card>();

    public Button drawCardButton;
    public Canvas UIcanvas;
    public GameObject defaultCardPrefab;
    public GameObject eventCardPrefab;
    private GameObject eventUI;
    
    public float scaleSpeed;
    public Vector3 finalScale;

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
            StartCoroutine(DrawCard(playerManager));
        }
        drawCardButton.onClick.AddListener(DrawCardOnClick);
    }

    void Update()
    {
        
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
    public IEnumerator DrawCard(PlayerManager playerManager)
    {        
        if (deck.Count > 0)
        {
            Card drawnCard = deck[0];
            
            deck.RemoveAt(0);
            UpdateDeckCountUI();
            DestroyTopCard();

            if (!(drawnCard is EventCard))
            {
                if (drawnCard is ResourceCard)
                {
                    ResourceCard resourceCard = drawnCard as ResourceCard;
                    ResourceType resourceType = resourceCard.resourceType;
                    switch (resourceType)
                    {
                        case ResourceType.Wood:
                            playerManager.TaskVariableUpdate(ref playerManager.Wood);
                            break;
                        case ResourceType.Food:
                            playerManager.TaskVariableUpdate(ref playerManager.Food);
                            break;
                        case ResourceType.Junk:
                            playerManager.TaskVariableUpdate(ref playerManager.Junk);
                            break;
                        case ResourceType.Scrap:
                            playerManager.TaskVariableUpdate(ref playerManager.Scrap);
                            break; 
                    }

                }
                playerManager.AddCardToHand(drawnCard);
            }
            else
            {
                yield return StartCoroutine(InstantiateEventCard(drawnCard)); // Ensure that the coroutine for InstantiateEventCard() completes before proceeding
                if (drawnCard is ICardEventDrawn cardEventDrawn)
                {
                    yield return cardEventDrawn.Drawn();
                }
            }
        }
        else
        {
            Debug.LogWarning("Deck is empty!");
        }
    }

    private void DrawCardOnClick()
    {
        StartCoroutine(DrawCardCoroutine());
    }

    private IEnumerator DrawCardCoroutine()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        if (playerManager != null)
        {
            if (playerManager.IsItemInHand(ItemName.Map))
            {
                Debug.LogWarning("You got a map! Get one more card!");
                yield return StartCoroutine(DrawCard(playerManager));
                Debug.LogWarning("Draw extra finish");
            }
            Debug.LogWarning("Draw main start");
            yield return StartCoroutine(DrawCard(playerManager));
            Debug.LogWarning("Draw main finish");
        }
        else
        {
            Debug.LogWarning("PlayerManager not found in the scene!");
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

    // Card Model Instantiation
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

    public IEnumerator InstantiateEventCard(Card card)
    {
        GameObject CardPrefab = eventCardPrefab;
        Vector3 center = UIcanvas.transform.position;
        eventUI = Instantiate(CardPrefab, center, Quaternion.identity, UIcanvas.transform);

        eventUI.gameObject.AddComponent<Canvas>();
        Canvas eventCanvas = eventUI.GetComponent<Canvas>();
        eventCanvas.overrideSorting = true;
        eventCanvas.sortingOrder = 30;

        // Card Display
        CardDisplay cardDisplay = eventUI.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.card = card;
            cardDisplay.DisplayCardInfo();
        }
        else
        {
            Debug.LogWarning("CardDisplay component not found on the instantiated object.");
        }
        
        Debug.LogWarning($"Instantiating Event Card {card.cardName}");

        yield return StartCoroutine(ScaleObject());
    }

    private IEnumerator ScaleObject()
    {
        while (eventUI.transform.localScale != finalScale)
        {
            eventUI.transform.localScale = Vector3.MoveTowards(eventUI.transform.localScale, finalScale, scaleSpeed * Time.deltaTime);
            yield return null;
        }

        yield return StartCoroutine(DestroyAfterSeconds(2)); // Ensure that the coroutine for DestroyAfterSeconds() completes before proceeding
    }

    private IEnumerator DestroyAfterSeconds(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        Destroy(eventUI);
    }
}
