using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
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
        Debug.Log($"Added {card.cardName} to hand.");
    }

    // Update Hand Count UI
    void UpdateHandCountUI()
    {
        string handCountString = "Hand count: " + handCount;

        handCountText.text = handCountString;
    }
}
