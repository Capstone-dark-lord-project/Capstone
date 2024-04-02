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
        hand.Add(card);
        UpdateHandCountUI();
        UpdateHandUI();
        Debug.Log($"Added {card.cardName} to hand.");
    }

    // Update Hand Count UI
    void UpdateHandCountUI()
    {
        string handCountString = "Hand count: " + hand.Count;

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

    public void InstantiateCardUI(Card card)
    {
            GameObject CardPrefab = GetCardTypePrefab(card);
            GameObject cardUI  = Instantiate(CardPrefab, cardParent.transform);

            // Card Display
            CardDisplay cardDisplay = cardUI.GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.card = card;
                cardDisplay.DisplayCardInfo();
            }
            else
            {
                Debug.LogWarning("CardDisplay component not found on the instantiated object.");
            }
            float yOffset = 200.0f;
            float xOffset = 150.0f;
            Vector3 newPosition = cardUI.transform.position;
            newPosition.y -= yOffset;
            newPosition.x += xOffset;
            cardUI.transform.position = newPosition;
            Debug.Log($"Instantiating Card {card.cardName}");
    }

    private void UpdateHandUI()
    {
        int existingCardCount = cardParent.transform.childCount;

        // Instantiate for only new cards
        for (int i = existingCardCount; i < hand.Count; i++)
        {
            InstantiateCardUI(hand[i]);
        }
    }
}
