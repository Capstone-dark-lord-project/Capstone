using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;

    public virtual void PlayCard()
    {
        // Implement functionality for playing this card.
        // This method can be overridden by subclasses.
    }
}

public class ResourceCard : Card
{
    public ResourceType resourceType;
    // Additional properties specific to resource cards
}

public class ActionCard : Card
{
    public override void PlayCard()
    {
        // Implement action-specific functionality
    }
}

public class ItemCard : Card
{
    // Item-specific properties and methods
}

public class EventCard : Card
{
    // Event-specific properties and methods
}

public enum ResourceType { Wood, Food, Scrap, Junk }