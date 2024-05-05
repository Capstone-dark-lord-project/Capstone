using UnityEngine;

[CreateAssetMenu(fileName = "New Event Card", menuName = "Card/Event Card")]
public class EventCard : Card
{
    public EventCard(Card card)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {

    }
}