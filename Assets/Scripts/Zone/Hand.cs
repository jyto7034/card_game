using Card;
using config;
using UnityEngine;
using Utils;

namespace Zone {
    public class Hand : Zone {
        public float vertical_displacement;
        public bool force_fit_to_container = true;
        public int card_container_width = 10;
        public int card_rotation = 15;
        private Vector3 origin;
        
        [SerializeField]
        public ZoomConfig zoom_config;

        [Header("Events")] 
        [SerializeField]
        private EventsConfig events_config;
        
        private void Start() {
            origin = transform.position - new Vector3(3, 0, 0);
            InitializeCards();
            SetCardPosition();
            SetCardsRotation();
        }

        private void InitializeCards() {
            var gs = CardManager.player_cards;
            foreach (var item in gs) {
                add_card(item.GetComponent<Card.Card>());
            }
        }

        private float GetCardVerticalDisplacement(int index) {
            if (cards.Count < 3) return 0;
            return vertical_displacement *
                   (1 - Mathf.Pow(index - (cards.Count - 1) / 2f, 2) / Mathf.Pow((cards.Count - 1) / 2f, 2));
        }
        
        private float GetCardRotation(int index) {
            if (cards.Count < 3) return 0;
            return card_rotation * (index - (cards.Count - 1) / 2f) / ((cards.Count - 1) / 2f);
        }
        
        private void SetCardsRotation() {
            for (var i = 0; i < cards.Count; i++) {
                cards[i].transform.WithRotation(y: GetCardRotation(i));
                cards[i].transform.AddPosition(z: GetCardVerticalDisplacement(i));
            }
        }
        
        private void SetCardPosition() {
            var cards_total_width = cards[0].transform.lossyScale.x * cards.Count;
            DebugExtensions.log_with_separator(", ", cards_total_width, card_container_width);
            // force_fit_to_container 가 true 고 cards_total_width 가 card_container_width 보다 클 때,
            if (force_fit_to_container && cards_total_width > card_container_width) {
                distribute_cards_to_fit_Container(cards_total_width);
            }
            else {
                
            }
        }
        
        private void distribute_cards_to_fit_Container(float cards_total_width) {
            var currentX = origin.x;
            var card_width = cards[0].transform.lossyScale.x;
            var distanceBetweenChildren = (card_container_width - cards_total_width) / (cards.Count - 1);
            print(card_width);
            foreach (var item in cards) {
                item.transform.WithPosition(x: currentX);
                currentX += card_width + distanceBetweenChildren;
                item.SetActive(true);
            }
        }
        private void distribute_cards(float cards_total_width) {
            
        }
        
        private void SetDisplacement() {
            
        }

        public override void add_card(Card.Card comp, int slot_id = 0) {
            comp.GetComponent<Card.Card>().zoom_config = zoom_config;
            comp.GetComponent<Card.Card>().event_config = events_config;
            cards.Add(comp.gameObject);
            comp.gameObject.SetActive(true);
        }

        public override void remove_card(GameObject card) {
            // 카드 제거 로직 구현
        }

        public override void pull_card(GameObject card, ZoneType zone_type) {
            // 카드 당기기 로직 구현
        }

    }
}
