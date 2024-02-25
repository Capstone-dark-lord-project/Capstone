using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum EventName { Flood, Fire }

[CreateAssetMenu(fileName = "New Event Card", menuName = "Card/Event Card")]
public class EventCard : Card
{
    public EventName eventName;

    public EventCard(string cardName, string cardDescription, Sprite cardArtwork, EventName eventName)
        : base(cardName, cardDescription, cardArtwork)
    {
        this.eventName = eventName;
    }
}