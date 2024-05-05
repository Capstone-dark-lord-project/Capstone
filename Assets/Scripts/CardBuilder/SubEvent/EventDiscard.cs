using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Discard Event Card", menuName = "Card/Event Card/Discard")]
public class EventDiscard : EventCard, ICardEventDrawn
{
    public int cardAmount;

    public EventDiscard(Card card, int cardAmount)
        : base(card)
    {
        this.cardAmount = cardAmount;
    }

    public void DiscardFromHand(int cardAmount)
    {
        CardContainer cardContainer = FindObjectOfType<CardContainer>();
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();     
        var handCount = cardContainer.cardOnHandUI.Count;

        if (handCount > 0)
        {
            for (int i = 0; i < cardAmount; i++)
            {
                var discardingIndex = Random.Range(0, cardContainer.cardOnHandUI.Count);
                Debug.LogWarning($"Discarding at {discardingIndex}.");
                
                playerManager.hand.RemoveAt(discardingIndex);
                Destroy(cardContainer.cardOnHandUI[discardingIndex].gameObject);
                cardContainer.cardOnHandUI.RemoveAt(discardingIndex);
                playerManager.UpdateHandCountUI();
            }
            Debug.LogWarning($"Discarded {cardAmount} cards from everyone!!!");
        }
        else
        {
            Debug.LogWarning("YOU ARE LUCKY >:(");
        }
    }

    public IEnumerator Drawn()
    {
        DiscardFromHand(cardAmount);
        yield return null;
    }
}