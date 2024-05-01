using UnityEngine;

public enum ActionName {Weapon, Bomb, Discard, Draw, Heal}

[CreateAssetMenu(fileName = "New Action Card", menuName = "Card/Action Card")]
public class ActionCard : Card
{
    public ActionName actionName;

    public ActionCard(Card card, ActionName actionName)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        this.actionName = actionName;
    }
}