using events;
using UnityEngine;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        public void OnCardDestroyed(CardDestroy evt) {
            Debug.Log("CardDestroyer.cs");
            var cardObj = evt.card; // Access the card object from the event
            var cardToRemoveIndex = container.cardOnHandUI.IndexOf(cardObj);
            Debug.Log($"Removing at: {cardToRemoveIndex}");

            // Remove card from hand list
            container.handList.RemoveAt(cardToRemoveIndex);

            // Remove card from UI list
            container.cardOnHandUI.Remove(cardObj);

            // Destroy the GameObject of the card
            Destroy(cardObj.gameObject);

            // Update the player's hand with the modified hand list
            container.playerManager.hand = container.handList;
        }
    }
}