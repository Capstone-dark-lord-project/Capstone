using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemName { Map, Compass, Plank, FishingRod, Weapon, Toy, HealthPack, CannedFood, Poison, MetalPlate, Trap, Bomb}

[CreateAssetMenu(fileName = "New Item Card", menuName = "Card/Item Card")]
public class ItemCard : Card
{
    public ItemName itemName;

    public ItemCard(Card card, ItemName itemName)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        this.itemName = itemName;
    }
}