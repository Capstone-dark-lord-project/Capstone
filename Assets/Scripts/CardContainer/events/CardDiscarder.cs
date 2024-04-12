using UnityEngine;

namespace events {
    public class CardDiscarder : MonoBehaviour {
        public CardContainer container;
        public void OnCardDiscarded(CardEvent evt) {
            Debug.Log("CardDiscarder.cs");
            var cardObj = evt.cardWrapper;
            var cardToDiscardIndex = container.cardOnHandUI.IndexOf(cardObj);
            
            Debug.Log($"Discarding at: {cardToDiscardIndex}");
            if (cardToDiscardIndex >= 0) {
                container.playerManager.hand.RemoveAt(cardToDiscardIndex);
                container.cardOnHandUI.Remove(cardObj);
            }
            
            if (cardObj != null) {
                Destroy(cardObj.gameObject);
            }
        }
    }
}