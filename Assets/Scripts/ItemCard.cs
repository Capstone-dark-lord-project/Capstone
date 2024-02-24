using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName { Map, Compass }

[CreateAssetMenu(fileName = "New Item Card", menuName = "Card/Item Card")]
public class ItemCard : Card
{
    public ItemName itemName;

    public ItemCard(string cardName, string description, Sprite artwork, ItemName itemName)
        : base(cardName, description, artwork)
    {
        this.itemName = itemName;
    }
}