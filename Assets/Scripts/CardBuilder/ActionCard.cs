using UnityEngine;

[CreateAssetMenu(fileName = "New Action Card", menuName = "Card/Action Card")]
public class ActionCard : Card
{
    public ActionCard(Card card)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        
    }
}