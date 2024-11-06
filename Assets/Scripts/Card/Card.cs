using System;
using config;
using DG.Tweening;
using events;
using Transform;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card { 
    public class Card : MonoBehaviour{
        private ZoneType currendt_zone;
        
        [HideInInspector] 
        public Guid uuid;
        
        private bool is_hover = false;
        private bool is_dragged = false;
        
        public EventsConfig event_config;
        public ZoomConfig zoom_config;
        
        private Vector3 mousePosition;
        private Vector3 lastPosition;
        private const float maxTilt = 10f; // 최대 기울기 값
        
        private void Start() {
            lastPosition = transform.position;
        }
        
        public void OnMouseEnter() {
            if (is_dragged) {
                return;
            }
            is_hover = true;
            event_config?.OnCardHover?.Invoke(new CardHover(this));
        }

        public void OnMouseExit() {
            if (is_dragged) {
                return;
            }
            is_hover = false;
            event_config?.OnCardUnhover?.Invoke(new CardUnhover(this));
        }
        
        private Vector3 GetMousePos(Vector3 pos) {
            return Camera.main!.WorldToScreenPoint(pos);
        }

        private void OnMouseDown() {
            print("drag enter");
            event_config?.OnCardUnhover?.Invoke(new CardUnhover(this));
            is_dragged = true;
            var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
            var result = new RaycastHit[10];
            var size = Physics.RaycastNonAlloc(ray, result);

            for (var i = 0; i < size; i++) {
                var _object = result[i];

                if (!_object.collider.CompareTag("Card")) continue;
                
                var anim = new Animation.Animation();
                var scale = new TransformData(_object.transform.position, Constant.card_size_while_movement,
                    _object.transform.rotation);
                anim.MoveTo(_object.transform.GetComponent<Card>(), scale, 0.1f);
                break;
            }

            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                GetMousePos(transform.position).z);
            var clickPosition = Camera.main!.ScreenToWorldPoint(screenPoint);

            var target_position = new Vector3(clickPosition.x, transform.position.y, clickPosition.z);
            mousePosition = Input.mousePosition - GetMousePos(target_position);
        }

        private void OnMouseUp() {
            if (!is_dragged) return;
            is_dragged = false;
            event_config?.OnCardPlayed.Invoke(new CardPlayed(this));
        }

        private void OnMouseDrag() {
            var target_position = Camera.main!.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            
            var movement = target_position - lastPosition;
            var movementMagnitude = movement.magnitude;

            var tiltFactor = Mathf.Clamp01(movementMagnitude / 0.1f);

            var tiltX = Mathf.Clamp(-movement.z * maxTilt * tiltFactor, -maxTilt, maxTilt);
            var tiltZ = Mathf.Clamp(movement.x * maxTilt * tiltFactor, -maxTilt, maxTilt);

            transform.rotation = Quaternion.Euler(tiltX, 0, tiltZ);
            
            lastPosition = transform.position;
            
            transform.position = Vector3.Lerp(transform.position, target_position, Time.deltaTime * 15f);
        }
    }
}