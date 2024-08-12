using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace Card {
    [System.Serializable]
    public class CardJson {
        public int player_type; 
        public string name;
        public string region;
        public int attack;
        public int health;
        public int cost;
        public string text;
        public string tag;
        public int count;
    }
    
    [Serializable]
    class Cards {
        public List<CardJson> cards;
    }
    
    public class CardGenerator : MonoBehaviour {
        // player 와 opponent card json 따로 구분 해야함.
        private const string json_path = "Assets/Resource/card_json.json";
        private static GameObject cardPrefab;
        
        // card.json 을 읽어들여서 모든 카드 정보에 대해서 Card 객체를 생성함.
        void Awake() {
            cardPrefab = Resources.Load<GameObject>("Prefebs/card");
        }

        private static Cards read_card_json() {
            var json = File.ReadAllText(json_path);
            // json 블럭들을 CardJson 객체로 생성.
            return JsonConvert.DeserializeObject<Cards>(json);
        }
        
        // 카드의 PlayerType 을 설정해야함.
        public static (List<GameObject>, List<GameObject>) create_card() {
            var p1 = new List<GameObject>();
            var p2 = new List<GameObject>();
            
            foreach (var card_data in read_card_json().cards) {
                var uuid = Guid.NewGuid();
                while (card_data.count-- > 0) {
                    var targetList = card_data.player_type == 1 ? p1 : p2;
                    
                    var trans = GameObject.FindWithTag("Hand").transform;
                    var card = Instantiate(cardPrefab, trans.position, Quaternion.Euler(90, 0, 0));
                    card.SetActive(false);
                    
                    card.transform.Find("Desc").GetComponent<TMP_Text>().text = card_data.text;
                    card.transform.Find("Atk").GetComponent<TMP_Text>().text = card_data.attack.ToString();
                    card.transform.Find("Def").GetComponent<TMP_Text>().text = card_data.health.ToString();
                    card.transform.Find("Name").GetComponent<TMP_Text>().text = card_data.name;
                    card.transform.Find("Cost").GetComponent<TMP_Text>().text = card_data.cost.ToString();
                    card.GetComponent<Card>().uuid = uuid;
                    card.transform.tag = "Card";
                    
                    targetList.Add(card);
                } 
            }

            return (p1, p2);
        }
    }
}