using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;

    protected Card(string cardName, string description, Sprite artwork) //Constructor
    {
        this.cardName = cardName;
        this.description = description;
        this.artwork = artwork;
    }

    public virtual void PlayCard()
    {

    }
}