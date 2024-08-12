using System.Collections.Generic;
using System.Linq.Expressions;
using Card;
using Transform;
using UnityEngine;

namespace Zone {
    public class Hand : Zone {
        private void Start() {
            var gs = CardManager.player_cards;
            cards.AddRange(gs);

            var (pos_weight, angles_weight) = get_cards_position();
            var origin_pos = GameObject.FindWithTag("Hand").transform.position;
            
            for (var i = 0; i < pos_weight.Count; i++) {
                cards[i].transform.position = new Vector3(origin_pos.x + pos_weight[i].Item1, origin_pos.y, 
                    origin_pos.z + pos_weight[i].Item2);
                cards[i].SetActive(true);
            }

            var euler = transform.rotation.eulerAngles;
            euler.x = 0;
            euler.y = 0;
            euler.z = 0;
            
            var flag = cards.Count % 2 == 0;
            for (var i = 0; i < angles_weight.Count; i+=2) {
                euler.y = angles_weight[i].Item1;
                cards[i].transform.rotation = Quaternion.Euler(euler);
                if (flag) {
                    euler.y = angles_weight[i].Item2;
                    cards[i + 1].transform.rotation = Quaternion.Euler(euler);
                }
                else {
                    i -= 1;
                }
                flag = true;
            }
        }

        public override void add_card(Card.Card comp, int slot_id = 0) {
            
        }

        public override void remove_card(GameObject card) {
            throw new System.NotImplementedException();
        }

        public override void pull_card(GameObject card, ZoneType zone_type) {
            throw new System.NotImplementedException();
        }
        
        private float Deg2Rad(float degrees) {
            return degrees * Mathf.Deg2Rad;
        }

        private (float, float) get_point_xy(float deg, float r) {
            deg = Deg2Rad(deg);
            var x = Mathf.Cos(deg) * r;
            var y = Mathf.Sin(deg) * r;

            if (Mathf.Abs(x) < 1e-6) x = 0;
            if (Mathf.Abs(y) < 1e-6) y = 0;

            return (x, y);
        }
        
        void shift_left(List<(int, int)> list) {
            if (list == null || list.Count == 0) return;

            var firstElement = list[0];
            for (var i = 0; i < list.Count - 1; i++) {
                list[i] = list[i + 1];
            }
            list[^1] = firstElement;
        }
        
        private (List<(float, float)>, List<(int, int)>) get_cards_position() {
            const float radius = 5;
            var card_count = cards.Count;
            var positions = new List<(float, float)>();

            const int RAD_WEIGHT = 6;
            var rads = card_count % 2 == 0 
                ? new List<int> { 90, 90 + RAD_WEIGHT }
                : new List<int> { 90 - RAD_WEIGHT, 90 }; 
            
            // 원의 좌표 공식을 통해 각 위치를 구함.
            // 위에서 아래로. 한 단계씩 내려가면서 위치값을 구함. 트리 구조 생각하면 됨
            for (var i = 1; i <= card_count; i++) {
                positions.Add(get_point_xy(rads[i % 2], radius));
                rads[0] -= RAD_WEIGHT;
                rads[1] += RAD_WEIGHT;
            }
            
            const float POS_WEIGHT = 0.9f;
            var weight = POS_WEIGHT;
            
            // 카드간 높낮이를 수정함.
            // 위에서 아래로
            for (var i = 0; i < card_count; i += 2) {
                var current = positions[i];
                var new_pos = (current.Item1, current.Item2 + weight);
                var flag = true;
                
                positions[i] = new_pos;
                if (card_count % 2 == 1 && i == 0) {
                    i -= 1;
                    flag = false;
                }

                if (flag) {
                    current = positions[i + 1];
                    new_pos = (current.Item1, current.Item2 + weight);
                    positions[i + 1] = new_pos;
                }

                weight += POS_WEIGHT * ((i + 1) / 20f);
            }

            const int ANGLE_WEIGHT = 3;
            var angles = new List<(int, int)>();
            for (var i = 1; i <= card_count; i++) {
                angles.Add((i * ANGLE_WEIGHT, i * -ANGLE_WEIGHT));
            }

            if (card_count % 2 != 0) {
                angles[0] = (0, 0);
            }

            return (positions, angles);
        }
    }
}