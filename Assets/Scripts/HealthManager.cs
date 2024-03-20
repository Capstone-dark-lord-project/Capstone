using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool Stunned = false;

    // Update is called once per frame
    void Update()
    {
        if(Stunned == false)
        {
            foreach(Image img in hearts)
            {
                img.sprite = emptyHeart;
            }
            for (int i = 0; i < health; i++)
            {
                hearts[i].sprite = fullHeart;
            }
        }

        if(health == 0 && Stunned == false)
        {
            Debug.Log("Stunned");
            Stunned = true;
        }
    }
}
