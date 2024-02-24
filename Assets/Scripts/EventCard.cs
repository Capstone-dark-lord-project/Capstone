using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName { Flood, Fire }

[CreateAssetMenu(fileName = "New Event Card", menuName = "Card/Event Card")]
public class EventCard : Card
{
    public EventName eventName;

    public EventCard(string cardName, string description, Sprite artwork, EventName eventName)
        : base(cardName, description, artwork)
    {
        this.eventName = eventName;
    }
}