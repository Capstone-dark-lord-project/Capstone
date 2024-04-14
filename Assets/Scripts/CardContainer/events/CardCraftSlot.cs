using UnityEngine;

namespace events {
    public class CardCraftSlot : MonoBehaviour {
        public CraftingManager craftingManager;
        public GameObject resourceCardPrefab;

        public void OnCraftSlotInput(int slot, ResourceCard resourceCard) {
            Debug.Log("CardCraftSlot.cs");

            if (slot == 1) {
                craftingManager.cardSlot1 = resourceCard;
                InstantiateCardUI(1, resourceCard);
            } else if (slot == 2) {
                craftingManager.cardSlot2 = resourceCard;
                InstantiateCardUI(2, resourceCard);
            }

            Debug.Log($"Adding: {resourceCard.name} to slot");
        }

        public void InstantiateCardUI(int slot, Card card)
        {
            GameObject cardUI = Instantiate(resourceCardPrefab);

            if (slot == 1) {
                cardUI.transform.SetParent(craftingManager.craftArea1, false);
            } else if (slot == 2) {
                cardUI.transform.SetParent(craftingManager.craftArea2, false);
            }

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

    }
}
