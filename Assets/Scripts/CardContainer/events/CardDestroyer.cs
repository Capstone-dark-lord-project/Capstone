using UnityEngine;

namespace events {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        
        public void OnCardDestroyed(CardEvent evt) {
            Debug.Log("CardDestroyer.cs");
            var cardObj = evt.cardWrapper;
            var cardToRemoveIndex = container.cardOnHandUI.IndexOf(cardObj);
            
            Debug.Log($"Destroying at: {cardToRemoveIndex}");
            if (cardToRemoveIndex >= 0) {
                container.playerManager.hand.RemoveAt(cardToRemoveIndex);
                container.cardOnHandUI.Remove(cardObj);
            }
            
            if (cardObj != null) {
                Destroy(cardObj.gameObject);
            }
        }
    }
}