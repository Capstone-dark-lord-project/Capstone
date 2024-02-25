using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite cardArtwork;

    public Card(string cardName, string cardDescription, Sprite cardArtwork) //Constructor
    {
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardArtwork = cardArtwork;
    }

    public virtual void PlayCard()
    {

    }

    public void Print()
    {
        Debug.Log("Card Name: " + cardName + "\nDescription: " + cardDescription + " Artwork:" + cardArtwork);
    }
}