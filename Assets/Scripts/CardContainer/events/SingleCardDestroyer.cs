using System;
using System.ComponentModel;
using UnityEngine;

namespace events {
    public class SingleCardDestroyer : MonoBehaviour {
        public CraftingManager craftingManager;
        public PlayerManager playerManager;
        public SingleCardContainer ContainerCraftArea1;
        public SingleCardContainer ContainerCraftArea2;
        public SingleCardContainer ContainerResultArea;
        
        public void OnCardRemoveFromSlot(int slot, SingleCardWrapper cardObj) {
            Debug.Log("OnCardRemoveFromSlot");
            var card = cardObj.card;
            switch (slot) {
                case 1:
                    ContainerCraftArea1.cardInSlot.Remove(cardObj);
                    if (cardObj != null) {
                        Destroy(cardObj.gameObject);
                    }
                    craftingManager.cardSlot1 = null;
                    if (craftingManager.resultSlot != null) {   
                        craftingManager.resultSlot = null;
                        ContainerResultArea.cardInSlot.Clear();
                        Destroy(craftingManager.resultArea.GetChild(0).gameObject);
                    }
                    playerManager.AddCardToHand(card);
                    break;
                case 2:
                    ContainerCraftArea2.cardInSlot.Remove(cardObj);
                    if (cardObj != null) {
                        Destroy(cardObj.gameObject);
                    }
                    craftingManager.cardSlot2 = null;
                    if (craftingManager.resultSlot != null) {
                        craftingManager.resultSlot = null;
                        ContainerResultArea.cardInSlot.Clear();
                        Destroy(craftingManager.resultArea.GetChild(0).gameObject);
                    }
                    playerManager.AddCardToHand(card);
                    break;
                case 3:
                    ContainerResultArea.cardInSlot.Remove(cardObj);
                    ContainerCraftArea1.cardInSlot.Clear();
                    ContainerCraftArea2.cardInSlot.Clear();
                    Destroy(craftingManager.craftArea1.GetChild(0).gameObject);
                    Destroy(craftingManager.craftArea2.GetChild(0).gameObject);
                    if (cardObj != null) {
                        Destroy(cardObj.gameObject);
                    }
                    craftingManager.cardSlot1 = null;
                    craftingManager.cardSlot2 = null;
                    craftingManager.resultSlot = null;
                    playerManager.AddCardToHand(card);
                    break;
            }
        }

        public void OnCraftingCancel() {
            Debug.Log("OnCraftingCancel");
            
            if (craftingManager.cardSlot1 != null) {
                var card1 = ContainerCraftArea1.cardInSlot[0].card;
                ContainerCraftArea1.cardInSlot.Clear();
                craftingManager.cardSlot1 = null;
                Destroy(craftingManager.craftArea1.GetChild(0).gameObject);
                playerManager.AddCardToHand(card1);
            }

            if (craftingManager.cardSlot2 != null) {
                var card2 = ContainerCraftArea2.cardInSlot[0].card;
                ContainerCraftArea2.cardInSlot.Clear();
                craftingManager.cardSlot2 = null;
                Destroy(craftingManager.craftArea2.GetChild(0).gameObject);
                playerManager.AddCardToHand(card2);
            }
            
            if (craftingManager.resultSlot != null) {
                ContainerResultArea.cardInSlot.Clear();
                craftingManager.resultSlot = null;
                Destroy(craftingManager.resultArea.GetChild(0).gameObject);
            }
        }
    }
}