using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject cardParent;
    public GameObject eventCardPrefab;
    public GameObject resourceCardPrefab;
    public GameObject itemCardPrefab;
    public GameObject actionCardPrefab;
    public GameObject defaultCardPrefab;
    public TextMeshProUGUI handCountText;
    public List<Card> hand = new List<Card>();
    public int health = 3;
    private int handCount = 0;

    void Start()
    {
        UpdateHandCountUI();
    }

    void Update()
    {
        
    }

    // Add Card from DeckManager.cs to the hand
    public void AddCardToHand(Card card)
    {
        handCount++;
        hand.Add(card);
        UpdateHandCountUI();
        UpdateHandUI();
        Debug.Log($"Added {card.cardName} to hand.");
    }

    public void RemoveCardFromHand(Card card)
    {
        if (allCards != null)
        {
            allCards.Remove(card);
        }
    }

    // Update Hand Count UI
    void UpdateHandCountUI()
    {
        string handCountString = "Hand count: " + handCount;

        handCountText.text = handCountString;
    }

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

    public void InstantiateCardUI(Card card, int position)
    {
            GameObject CardPrefab = GetCardTypePrefab(card);
            // Vector3 cardPosition = new Vector3(Random.Range(0f, 0.2f), cardIndex * cardWidth, Random.Range(0f, 0.2f));
            // Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 5.0f), 0f);
            GameObject cardUI  = Instantiate(CardPrefab, cardParent.transform);
            // Card Display
            CardDisplay cardDisplay = cardUI .GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.card = card;
                cardDisplay.DisplayCardInfo();
            }
            else
            {
                Debug.LogWarning("CardDisplay component not found on the instantiated object.");
            }
            // Calculate the total width of all cards in the hand
            float totalWidth = (hand.Count - 1) * 2.6f;

            // Calculate the x-position based on the index and total width
            
            float newXPosition = -totalWidth / 2f + position * 2.6f;

            // Set the local position of the card
            cardUI.transform.localPosition = new Vector3(newXPosition, 0f, 0f);
            // cardUI .transform.Rotate(new Vector3(-90f, 180f, 0f));
            Debug.Log($"Instantiating Card {card.cardName}");
    }

    private void UpdateHandUI()
    {
        // Destroy existing UI elements representing cards in the hand
        foreach (Transform child in cardParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        int position = -1;

        // Instantiate UI elements for each card in the hand
        foreach (Card card in hand)
        {
            position++;
            InstantiateCardUI(card, position);
        }
    }

    public void CombineCards(GameObject card1, GameObject card2)
    {
        // Example of retrieving the Card components
        Card card1 = card1GameObject.GetComponent<CardDisplay>().card;
        Card card2 = card2GameObject.GetComponent<CardDisplay>().card;

        // Implement your game's logic to check if these two cards can be combined
        // This might involve checking the card types, any special conditions, etc.

        if (CanCombine(card1, card2))
        {
            // Determine the result of the combination
            Card combinedCard = GetCombinationResult(card1, card2);

            // Add the new card to the player's hand
            AddCardToHand(combinedCard);

            // Remove the original cards from the hand
            RemoveCardFromHand(card1);
            RemoveCardFromHand(card2);

            // Update the UI to reflect the new hand
            UpdateHandUI();
        }
        else
        {
            // Handle the case where cards can't be combined
            // For example, you could provide feedback to the player
            Debug.Log("These cards can't be combined!");
        }
    }
}
