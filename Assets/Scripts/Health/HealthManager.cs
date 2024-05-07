using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public PlayerManager playerManager;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool Stunned = false;

    // Update is called once per frame
    void Update()
    {
        if (playerManager.health < 0)
        {
            playerManager.health = 0;
        }
        
        foreach(Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < playerManager.health; i++)
        {
            hearts[i].sprite = fullHeart;
        }

        if (playerManager.health == 0 && Stunned == false)
        {
            Debug.LogWarning("Stunned");
            Stunned = true;
        }
    }
}
