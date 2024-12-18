using System;
using events;
using UnityEngine;
using UnityEngine.Events;

namespace config {
    [Serializable]
    public class EventsConfig {
        [SerializeField]
        public UnityEvent<CardPlayed> OnCardPlayed;
        
        public UnityEvent<CardHover> OnCardHover;
        
        [SerializeField]
        public UnityEvent<CardUnhover> OnCardUnhover;
        
        [SerializeField]
        public UnityEvent<CardDestroy> OnCardDestroy;
    }
}
