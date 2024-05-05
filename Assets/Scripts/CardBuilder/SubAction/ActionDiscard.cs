using UnityEngine;

[CreateAssetMenu(fileName = "New Discard Card", menuName = "Card/Action Card/Discard")]
public class ActionDiscard : ActionCard
{
    public int discardAmount;

    public ActionDiscard(Card card, ActionName actionName, int discardAmount)
        : base(card, actionName)
    {
        this.discardAmount = discardAmount;
    }

    public void DiscardFromHand()
    {
        Debug.Log("DiscardedCard!!!");
    }
}