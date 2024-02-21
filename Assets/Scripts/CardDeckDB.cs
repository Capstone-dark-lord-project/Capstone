using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckDB : MonoBehaviour
{
    public static List<Card> cardDeck = new List<Card>();
    
    void Awake()
    {
        cardDeck.Add(CreateResourceCard("Log", "Yes, a log. Nothing more.", ResourceType.Wood));
        cardDeck.Add(CreateItemCard("Island Map", "You can now navigate easily around the island, draw 2 cards instead of 1 every turn", ItemName.Map));
        cardDeck.Add(CreateEventCard("A (not so) little refreshment", "All players randomly lose 1 card from their hand", EventName.Flood));
        cardDeck.Add(CreateActionCard("Today is a long day!", "You feel energized, you decided to search for more stuff.", ActionName.Draw2));
    }

    private ResourceCard CreateResourceCard(string cardName, string description, ResourceType resourceType)
    {
        ResourceCard card = ScriptableObject.CreateInstance<ResourceCard>();
        card.cardName = cardName;
        card.description = description;
        card.resourceType = resourceType;
        return card;
    }

    private ItemCard CreateItemCard(string cardName, string description, ItemName itemName)
    {
        ItemCard card = ScriptableObject.CreateInstance<ItemCard>();
        card.cardName = cardName;
        card.description = description;
        card.itemName = itemName;
        return card;
    }

    private EventCard CreateEventCard(string cardName, string description, EventName eventName)
    {
        EventCard card = ScriptableObject.CreateInstance<EventCard>();
        card.cardName = cardName;
        card.description = description;
        card.eventName = eventName;
        return card;
    }

    private ActionCard CreateActionCard(string cardName, string description, ActionName actionName)
    {
        ActionCard card = ScriptableObject.CreateInstance<ActionCard>();
        card.cardName = cardName;
        card.description = description;
        card.actionName = actionName;
        return card;
    }
}