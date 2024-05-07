using UnityEngine;

public enum ItemName { Map, Compass, ProcessedWood, FishingRod, Weapon, Toy, HealthPack, CannedFood, FoodWaste, MetalPlate, Trap, Bomb}

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