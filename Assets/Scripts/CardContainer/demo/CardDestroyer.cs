using events;
using UnityEngine;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        public void OnCardDestroyed(CardDestroy evt) {
            Debug.Log("CardDestroyer.cs");
            var cardObj = evt.card;
            var cardToRemoveIndex = container.cardOnHandUI.IndexOf(cardObj);
            Debug.Log($"Discarding at: {cardToRemoveIndex}");
            container.handList.RemoveAt(cardToRemoveIndex);
            container.cardOnHandUI.Remove(cardObj);
            Destroy(cardObj.gameObject);
            container.playerManager.hand = container.handList;
        }
    }
}