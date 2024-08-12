using System.Collections.Generic;
using System.Linq;
using Core;
using Transform;
using UnityEngine;

namespace Zone {
    public class Field : Zone {
        public static List<TransformData> field_slots;

        private void Start() {
            var result = GameObject.FindGameObjectsWithTag("Field_Slot").OrderBy(t => t.name).ToArray();
            field_slots = new List<TransformData>();
            
            foreach (var obj in result) {
                field_slots.Add(
                    // new TransformData(
                    //     new Vector3(obj.transform.position.x + 0.05f, obj.transform.position.y + 0.2f,
                    //         obj.transform.position.z),
                    //     8,
                    //     Quaternion.Euler(0, obj.transform.rotation.y, obj.transform.rotation.z)));
                    new TransformData(
                        new Vector3(obj.transform.position.x + 0.05f, obj.transform.position.y + 0.2f,
                            obj.transform.position.z),
                        // 초기 상태에서 Mul 하는 것.
                        obj.transform.WithScaleMulRet(x: 3, z: 3),
                        Quaternion.Euler(0, obj.transform.rotation.y, obj.transform.rotation.z)));
            }
        }

        public override void add_card(Card.Card comp, int slot_id = 0) {
            GameSystem.Instance.card_animation.place_to_field_slot(comp, slot_id);
        }

        public override void remove_card(GameObject card) {
            throw new System.NotImplementedException();
        }

        public override void pull_card(GameObject card, ZoneType zone_type) {
            throw new System.NotImplementedException();
        }
    }
}