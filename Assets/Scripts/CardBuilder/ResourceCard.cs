using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ResourceType { Wood, Food, Scrap, Junk }

[CreateAssetMenu(fileName = "New Resource Card", menuName = "Card/Resource Card")]
public class ResourceCard : Card 
{
    public Sprite resourceIcon;
    public ResourceType resourceType;

    public ResourceCard(Card card, Sprite resourceIcon, ResourceType resourceType)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        this.resourceIcon = resourceIcon;
        this.resourceType = resourceType;
    }
}
