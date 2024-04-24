using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Draw Card", menuName = "Card/Action Card/Draw")]
public class ActionDraw : ActionCard, ICardPlayable
{
    public int drawAmount;

    public ActionDraw(Card card, int drawAmount)
        : base(card)
    {
        this.drawAmount = drawAmount;
    }

    public void DrawToHand()
    {
        Debug.Log("DrawnCard!!!");
    }

    public void Play()
    {
        DrawToHand();
    }
}