using System;
using UnityEngine;

namespace Input {
    public class DragWithTilt : MonoBehaviour {
        private Vector3 mousePosition;
        private Vector3 lastPosition;
        private Vector3 lastVelocity;
        private float maxTilt = 10f; // 최대 기울기 값

        private void Start() {
            lastPosition = transform.position;
            lastVelocity = Vector3.zero;
        }

        private Vector3 GetMousePos(Vector3 pos) {
            return Camera.main!.WorldToScreenPoint(pos);
        }

        private void OnMouseDown() {
            var screenPoint = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y,
                GetMousePos(transform.position).z);
            var clickPosition = Camera.main!.ScreenToWorldPoint(screenPoint);

            var target_position = new Vector3(clickPosition.x, transform.position.y, clickPosition.z);
            mousePosition = UnityEngine.Input.mousePosition - GetMousePos(target_position);
        }

        private void OnMouseDrag() {
            var target_position = Camera.main!.ScreenToWorldPoint(UnityEngine.Input.mousePosition - mousePosition);
            
            Vector3 direction = new Vector3(target_position.x - lastPosition.x, 0, target_position.z - lastPosition.z).normalized;
            
            float tiltX = Mathf.Clamp(direction.x * maxTilt, -maxTilt, maxTilt);
            float tiltZ = Mathf.Clamp(direction.z * maxTilt, -maxTilt, maxTilt);
            
            transform.rotation = Quaternion.Euler(tiltX, 0, tiltZ);

            lastPosition = transform.position;

            transform.position = Vector3.Lerp(transform.position, target_position, Time.deltaTime * 15f);
        }
    }
}