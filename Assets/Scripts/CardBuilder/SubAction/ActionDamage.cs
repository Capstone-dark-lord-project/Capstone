using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New Damage Card", menuName = "Card/Action Card/Damage")]
public class ActionDamage : ActionCard, ICardPlayable
{
    public int damageAmount;

    public ActionDamage(Card card, ActionName actionName, int damageAmount)
        : base(card, actionName)
    {
        this.damageAmount = damageAmount;
    }

    public IEnumerator Damage(int damageAmount)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        
        playerManager.TaskVariableUpdate(ref playerManager.dealDamage);
        if (playerManager.health > 0)
        {
            for (int i = 0; i < damageAmount; i++)
            {
                Debug.LogWarning($"damage 1 !!!");
                playerManager.health -= 1;
            }
            Debug.LogWarning($"Damaged {damageAmount}!!!");
        }
        else
        {
            Debug.LogWarning("You're stunned");
        }
        yield return null;
    }

    public IEnumerator Play()
    {
        Debug.Log("PLAY IENUM");
        CoroutineStarter coroutineStarter = CoroutineStarter.Instance;
        yield return coroutineStarter.StartCoroutine(Damage(damageAmount));
        Destroy(coroutineStarter.gameObject);
    }
}