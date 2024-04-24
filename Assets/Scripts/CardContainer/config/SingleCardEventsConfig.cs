using System;
using events;
using UnityEngine;
using UnityEngine.Events;

namespace config {
    [Serializable]
    public class SingleCardEventsConfig {
        [SerializeField]
        public UnityEvent<int, SingleCardWrapper> OnCardRemoveFromSlot;
    }
}
