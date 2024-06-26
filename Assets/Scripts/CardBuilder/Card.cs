using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite cardArtwork;
    public int count = 1;

    public Card(string cardName, string cardDescription, Sprite cardArtwork) //Constructor
    {
        this.cardName = cardName;
        this.cardDescription = cardDescription;
        this.cardArtwork = cardArtwork;
    }
}