using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Food Card", menuName = "Card/Event Card/AddFood")]
public class EventAddFood : EventCard, ICardEventDrawn
{
    public int cardAmount;
    public Card foodResourceCard;

    public EventAddFood(Card card, int cardAmount, Card foodResourceCard)
        : base(card)
    {
        this.cardAmount = cardAmount;
        this.foodResourceCard = foodResourceCard;
    }

    public void AddFoodToHand(int cardAmount)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        for (int i = 0; i < cardAmount; i++)
        {
            playerManager.TaskVariableUpdate(ref playerManager.Food);
            playerManager.AddCardToHand(foodResourceCard);
        }
        Debug.LogWarning($"Add {cardAmount} Food for Everyone!!!");
    }

    public IEnumerator Drawn()
    {
        AddFoodToHand(cardAmount);
        yield return null;
    }
}
