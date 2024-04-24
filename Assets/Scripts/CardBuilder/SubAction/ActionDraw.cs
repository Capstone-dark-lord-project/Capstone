using UnityEngine;

[CreateAssetMenu(fileName = "New Draw Card", menuName = "Card/Action Card/Draw")]
public class ActionDraw : ActionCard
{
    public int drawAmount;

    public ActionDraw(Card card, int drawAmount)
        : base(card)
    {
        this.drawAmount = drawAmount;
    }

}
