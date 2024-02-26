using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescriptionText;
    public Image artWorkImage;
    public Image resourceIconImage;

    void Start()
    {
        DisplayCardInfo();
    }

    void DisplayCardInfo()
    {
        if (card != null)
        {
            cardNameText.text = card.cardName;
            cardDescriptionText.text = card.cardDescription;
            artWorkImage.sprite = card.cardArtwork;
            
            if (card is ResourceCard resourceCard && resourceCard.resourceIcon != null)
            {
                resourceIconImage.sprite = resourceCard.resourceIcon;
                resourceIconImage.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("No Card assigned to CardDisplay!");
        }
    }
}
