using UnityEngine;

namespace events {
    public class CardCraftSlot : MonoBehaviour {
        public CraftingManager craftingManager;

        public void OnCraftSlotInput(Card cardManagerSlot, CardEvent evt) {
            Debug.Log("CardCraftSlot.cs");
            var card = evt.cardWrapper.card;

            if (cardManagerSlot == craftingManager.cardSlot1) {
                craftingManager.cardSlot1 = card;
            } else if (cardManagerSlot == craftingManager.cardSlot2) {
                craftingManager.cardSlot2 = card;
            }

            Debug.Log($"Adding: {card.name} to slot");
        }
    }
}
