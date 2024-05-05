using System;
using UnityEngine;

namespace events {
    public class CardCraftInitiate : MonoBehaviour {
        public CraftingManager craftingManager;
        public Card AA;
        public Card AB;
        public Card AC;
        public Card AD;
        public Card BB;
        public Card BC;
        public Card BD;
        public Card CC;
        public Card CD;
        public Card DD;
        public GameObject itemCardPrefab;
        public GameObject actionCardPrefab;

        public void OnBothSlotFull(ResourceCard slot1, ResourceCard slot2) {
            Debug.Log("CardCraftInitiate.cs");
            craftingManager.resultSlot = GetResult(slot1, slot2);
            InstantiateCardUI(craftingManager.resultSlot);
        }

        public Card GetResult(ResourceCard slot1, ResourceCard slot2) {
            ResourceType slot1Type = slot1.resourceType;
            ResourceType slot2Type = slot2.resourceType;

            Card result = GetCombination(slot1Type, slot2Type);
            if (result != null)
            {
                return result;
            }
            else
            {
                return GetCombination(slot2Type, slot1Type);
            }
        }

        private Card GetCombination(ResourceType slot1Type, ResourceType slot2Type) 
        {
            switch (slot1Type) 
            {
                case ResourceType.Wood:
                    if (slot2Type == ResourceType.Wood) return AA;
                    else if (slot2Type == ResourceType.Food) return AB;
                    else if (slot2Type == ResourceType.Scrap) return AC;
                    else if (slot2Type == ResourceType.Junk) return AD;
                    break;
                case ResourceType.Food:
                    if (slot2Type == ResourceType.Food) return BB;
                    else if (slot2Type == ResourceType.Scrap) return BC;
                    else if (slot2Type == ResourceType.Junk) return BD;
                    break;
                case ResourceType.Scrap:
                    if (slot2Type == ResourceType.Scrap) return CC;
                    else if (slot2Type == ResourceType.Junk) return CD;
                    break;
                case ResourceType.Junk:
                    if (slot2Type == ResourceType.Junk) return DD;
                    break;
            }
            return null;
        }

        public void InstantiateCardUI(Card card)
        {
            
            GameObject CardPrefab = GetCardTypePrefab(card);
            GameObject cardUI  = Instantiate(CardPrefab);

            cardUI.transform.SetParent(craftingManager.resultArea, false);

            CardDisplay cardDisplay = cardUI.GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.card = card;
                cardDisplay.DisplayCardInfo();
            }
            else
            {
                Debug.LogWarning("CardDisplay component not found on the instantiated object.");
            }
            Debug.Log($"Instantiating Card {card.cardName}");
        }

        GameObject GetCardTypePrefab(Card card)
        {
            if (card is ItemCard)
            {
                return itemCardPrefab;
            }
            else if (card is ActionCard)
            {
                return actionCardPrefab;
            }
            else
            {
                return itemCardPrefab;
            }
        }
    }
}
