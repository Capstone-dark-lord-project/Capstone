using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New heal Card", menuName = "Card/Action Card/Heal")]
public class ActionHeal : ActionCard, ICardPlayable
{
    public int healAmount;

    public ActionHeal(Card card, ActionName actionName, int healAmount)
        : base(card, actionName)
    {
        this.healAmount = healAmount;
    }

    public IEnumerator Heal(int healAmount)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        
        if (playerManager.health < 3)
        {
            for (int i = 0; i < healAmount; i++)
            {
                playerManager.health += 1;
            }
            Debug.LogWarning($"heald {healAmount}!!!");
        }
        else
        {
            Debug.LogWarning("You're healed !!");
        }
        yield return null;
    }

    public IEnumerator Play()
    {
        Debug.Log("PLAY IENUM");
        CoroutineStarter coroutineStarter = CoroutineStarter.Instance;
        yield return coroutineStarter.StartCoroutine(Heal(healAmount));
        Destroy(coroutineStarter.gameObject);
    }
}