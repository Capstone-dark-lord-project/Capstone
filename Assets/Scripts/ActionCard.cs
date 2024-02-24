using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionName { Skip, Draw2 }

[CreateAssetMenu(fileName = "New Action Card", menuName = "Card/Action Card")]
public class ActionCard : Card
{
    public ActionName actionName;

    public ActionCard(string cardName, string description, Sprite artwork, ActionName actionName)
        : base(cardName, description, artwork)
    {
        this.actionName = actionName;
    }

    public override void PlayCard()
    {
        // Implement action-specific functionality
    }
}