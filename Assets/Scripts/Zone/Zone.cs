using System.Collections.Generic;
using UnityEngine;

namespace Zone {
    public abstract class Zone : MonoBehaviour{
        [HideInInspector] public List<GameObject> cards;

        public abstract void add_card(Card.Card comp, int slot_id = 0);

        public abstract void remove_card(GameObject card);

        public abstract void pull_card(GameObject card, ZoneType zone_type);
    }
}