using System;
using events;
using UnityEngine;
using UnityEngine.Events;

namespace config {
    [Serializable]
    public class EventsConfig {
        [SerializeField]
        public UnityEvent<CardEvent> OnCardPlayed;
        
        [SerializeField]
        public UnityEvent<CardEvent> OnCardHover;
        
        [SerializeField]
        public UnityEvent<CardEvent> OnCardUnhover;
        
        [SerializeField]
        public UnityEvent<CardEvent> OnCardDestroy;

        [SerializeField]
        public UnityEvent<CardEvent> OnCardDiscard;
        
        [SerializeField]
        public UnityEvent<Card, CardEvent> OnCraftSlotInput;
    }
}
