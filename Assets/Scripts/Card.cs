using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Temp
public enum ResourceType { Wood, Food, Scrap, Junk }
public enum ItemName { Map, Compass }
public enum EventName { Flood, Fire }
public enum ActionName { Skip, Draw2 }

// Main Card class
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;

    public Card(string cardName, string description, Sprite artwork) //Constructor
    {
        this.cardName = cardName;
        this.description = description;
        this.artwork = artwork;
    }

    public virtual void PlayCard()
    {

    }
}

// Subclasses
public class ResourceCard : Card 
{
    public ResourceType resourceType;

    public ResourceCard(string cardName, string description, Sprite artwork, ResourceType resourceType)
        : base(cardName, description, artwork) //Constructor base on the main constructor in the "Card" class
    {
        this.resourceType = resourceType;
    }
}

public class ActionCard : Card
{
    public ActionName actionName;

    public ActionCard(string cardName, string description, Sprite artwork, ActionName actionName)
        : base(cardName, description, artwork)
    {
        this.actionName = actionName;
    }

    public override void PlayCard()
    {
        // Implement action-specific functionality
    }
}

public class ItemCard : Card
{
    public ItemName itemName;

    public ItemCard(string cardName, string description, Sprite artwork, ItemName itemName)
        : base(cardName, description, artwork)
    {
        this.itemName = itemName;
    }
}

public class EventCard : Card
{
    public EventName eventName;

    public EventCard(string cardName, string description, Sprite artwork, EventName eventName)
        : base(cardName, description, artwork)
    {
        this.eventName = eventName;
    }
}