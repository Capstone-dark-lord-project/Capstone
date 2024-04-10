using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemName { Map, Compass }

[CreateAssetMenu(fileName = "New Item Card", menuName = "Card/Item Card")]
public class ItemCard : Card
{
    public ItemName itemName;

    public ItemCard(string cardName, string cardDescription, Sprite cardArtwork, ItemName itemName)
        : base(cardName, cardDescription, cardArtwork)
    {
        this.itemName = itemName;
    }
}