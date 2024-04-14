using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum EventName { Flood, Fire }

[CreateAssetMenu(fileName = "New Event Card", menuName = "Card/Event Card")]
public class EventCard : Card
{
    public EventName eventName;

    public EventCard(Card card, EventName eventName)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        this.eventName = eventName;
    }
}