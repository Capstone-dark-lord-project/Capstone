using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Wood, Food, Scrap, Junk }

[CreateAssetMenu(fileName = "New Resource Card", menuName = "Card/Resource Card")]
public class ResourceCard : Card 
{
    public ResourceType resourceType;

    public ResourceCard(string cardName, string description, Sprite artwork, ResourceType resourceType)
        : base(cardName, description, artwork)
    {
        this.resourceType = resourceType;
    }
}
