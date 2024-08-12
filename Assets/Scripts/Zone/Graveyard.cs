using UnityEngine;

namespace Zone {
    public class Graveyard : Zone{
        public override void add_card(Card.Card comp, int slot_id = 0) {
            throw new System.NotImplementedException();
        }

        public override void remove_card(GameObject card) {
            throw new System.NotImplementedException();
        }

        public override void pull_card(GameObject card, ZoneType zone_type) {
            throw new System.NotImplementedException();
        }
    }
}