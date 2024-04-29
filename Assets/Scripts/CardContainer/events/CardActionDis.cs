using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace events {
    public class CardActionDis : MonoBehaviour {
        private GameObject actionUI;
        public Canvas UIcanvas;
        public float scaleSpeed;
        public Vector3 finalScale;
        public GameObject actionCardPrefab;

        public IEnumerator InstantiateActionCard(Card card)
        {
            GameObject CardPrefab = actionCardPrefab;
            Vector3 center = UIcanvas.transform.position;
            actionUI = Instantiate(CardPrefab, center, Quaternion.identity, UIcanvas.transform);

            actionUI.gameObject.AddComponent<Canvas>();
            Canvas actionCanvas = actionUI.GetComponent<Canvas>();
            actionCanvas.overrideSorting = true;
            actionCanvas.sortingOrder = 30;

            // Card Display
            CardDisplay cardDisplay = actionUI.GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                cardDisplay.card = card;
                cardDisplay.DisplayCardInfo();
            }
            else
            {
                Debug.LogWarning("CardDisplay component not found on the instantiated object.");
            }
            
            Debug.LogWarning($"Instantiating action Card {card.cardName}");

            yield return StartCoroutine(ScaleObject());
        }

        private IEnumerator ScaleObject()
        {
            while (actionUI.transform.localScale != finalScale)
            {
                actionUI.transform.localScale = Vector3.MoveTowards(actionUI.transform.localScale, finalScale, scaleSpeed * Time.deltaTime);
                yield return null;
            }

            yield return StartCoroutine(DestroyAfterSeconds(2));
        }

        private IEnumerator DestroyAfterSeconds(int seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);

            Destroy(actionUI);
        }
    }
}
