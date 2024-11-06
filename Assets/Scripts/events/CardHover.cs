using UnityEngine;

namespace events {
    public class CardHover : CardEvent {
        public CardHover(Card.Card card) : base(card) {
            Debug.Log("hoverd");
        }
    }
}
