using System.Collections.Generic;
using UnityEngine;
using Zone;

namespace Card {
    public class CardSorting : MonoBehaviour {
        public Hand hand_field;
        private const int BASE_QUEUE = 3000; // Transparent 큐 시작점

        void Update()
        {
            // 카드 위치에 따라 renderQueue 동적 업데이트
            UpdateRenderQueue();
        }

        private void UpdateRenderQueue()
        {
            for (int i = 0; i < hand_field.cards.Count; i++)
            {
                MeshRenderer renderer = hand_field.cards[i].GetComponent<MeshRenderer>();
                renderer.material.renderQueue = BASE_QUEUE + i;
            }
        }
    }
}