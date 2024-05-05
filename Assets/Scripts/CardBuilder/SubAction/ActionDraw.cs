using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New Draw Card", menuName = "Card/Action Card/Draw")]
public class ActionDraw : ActionCard, ICardPlayable
{
    public int drawAmount;

    public ActionDraw(Card card, ActionName actionName, int drawAmount)
        : base(card, actionName)
    {
        this.drawAmount = drawAmount;
    }

    public IEnumerator DrawToHand(int drawAmount)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        DeckManager deckManager = FindObjectOfType<DeckManager>();
        for (int i = 0; i < drawAmount; i++)
        {
            Debug.LogWarning("Draw Start!");
            yield return deckManager.DrawCard(playerManager);
            Debug.LogWarning("Draw success!");
        }
        Debug.LogWarning($"Add {drawAmount} card");
    }

    public IEnumerator Play()
    {
        Debug.Log("PLAY IENUM");
        CoroutineStarter coroutineStarter = CoroutineStarter.Instance;
        yield return coroutineStarter.StartCoroutine(DrawToHand(drawAmount));
        Destroy(coroutineStarter.gameObject);
    }
}