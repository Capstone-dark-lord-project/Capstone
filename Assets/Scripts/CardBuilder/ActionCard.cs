using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ActionName { Skip, Draw2 }

[CreateAssetMenu(fileName = "New Action Card", menuName = "Card/Action Card")]
public class ActionCard : Card
{
    public ActionName actionName;

    public ActionCard(Card card, ActionName actionName)
        : base(card.cardName, card.cardDescription, card.cardArtwork)
    {
        this.actionName = actionName;
    }

    public override void PlayCard()
    {
        // Implement action-specific functionality
    }
}