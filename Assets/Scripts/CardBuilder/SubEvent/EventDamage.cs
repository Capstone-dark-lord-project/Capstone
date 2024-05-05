using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Event Card", menuName = "Card/Event Card/Damage")]
public class EventDamage : EventCard, ICardEventDrawn
{
    public int damageAmount;

    public EventDamage(Card card, int damageAmount)
        : base(card)
    {
        this.damageAmount = damageAmount;
    }

    public void Damage(int damageAmount)
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        if (playerManager.health > 0)
        {
            for (int i = 0; i < damageAmount; i++)
            {
                playerManager.health -= 1;
            }
            Debug.LogWarning($"Damaged {damageAmount}!!!");
        }
        else
        {
            Debug.LogWarning("You're stunned");
        }
    }

    public IEnumerator Drawn()
    {
        Damage(damageAmount);
        yield return null;
    }
}