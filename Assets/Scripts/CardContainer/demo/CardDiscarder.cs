using events;
using UnityEngine;

namespace demo {
    public class CardDiscarder : MonoBehaviour {
        public CardContainer container;
        public void OnCardDiscarded(CardDiscard evt) {
            Debug.Log("CardDiscarder.cs");
            var cardObj = evt.card;
            var cardToDiscardIndex = container.cardOnHandUI.IndexOf(cardObj);
            Debug.Log($"Discarding at: {cardToDiscardIndex}");
            container.handList.RemoveAt(cardToDiscardIndex);
            container.cardOnHandUI.Remove(cardObj);
            Destroy(cardObj.gameObject);
            container.playerManager.hand = container.handList;
        }
    }
}