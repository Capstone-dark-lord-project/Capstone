using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ResourceType { Wood, Food, Scrap, Junk }

[CreateAssetMenu(fileName = "New Resource Card", menuName = "Card/Resource Card")]
public class ResourceCard : Card 
{
    public ResourceType resourceType;
    public Sprite resourceIcon;

    public ResourceCard(string cardName, string cardDescription, Sprite cardArtwork, ResourceType resourceType, Sprite resourceIcon)
        : base(cardName, cardDescription, cardArtwork)
    {
        this.resourceType = resourceType;
        this.resourceIcon = resourceIcon;
    }
}
