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

    public void DrawToHand(int drawAmount)
    {
        DeckManager deckManager = FindObjectOfType<DeckManager>();
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        for (int i = 0; i < drawAmount; i++)
        {
            StartCoroutine(deckManager.DrawCard(playerManager));
            Debug.Log("draw success!");
        }
        Debug.LogWarning($"Add {drawAmount} card");
    }

    public void Play()
    {
        DrawToHand(drawAmount);
    }
}