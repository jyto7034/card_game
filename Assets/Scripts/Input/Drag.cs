using UnityEngine;
using Animation;
using Transform;


namespace Input {
    public class Drag : MonoBehaviour {
        private Card.Card card_component;
        private Vector3 mousePosition;
        private Vector3 lastPosition;
        private const float maxTilt = 10f; // 최대 기울기 값

        private void Start() {
            card_component = GetComponent<Card.Card>();
            lastPosition = transform.position;
        }

        private Vector3 GetMousePos(Vector3 pos) {
            return Camera.main!.WorldToScreenPoint(pos);
        }

        private void OnMouseDown() {
            var ray = Camera.main!.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var result = new RaycastHit[10];
            var size = Physics.RaycastNonAlloc(ray, result);

            for (var i = 0; i < size; i++) {
                var _object = result[i];

                if (!_object.collider.CompareTag("Card")) continue;
                
                var anim = new Animation.Animation();
                var scale = new TransformData(_object.transform.position, Constant.card_size_while_movement,
                    _object.transform.rotation);
                anim.MoveTo(_object.transform.GetComponent<Card.Card>(), scale, 0.1f);
                break;
            }

            var screenPoint = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y,
                GetMousePos(transform.position).z);
            var clickPosition = Camera.main!.ScreenToWorldPoint(screenPoint);

            var target_position = new Vector3(clickPosition.x, transform.position.y, clickPosition.z);
            mousePosition = UnityEngine.Input.mousePosition - GetMousePos(target_position);
        }

        private void OnMouseUp() {
            var ray = Camera.main!.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var result = new RaycastHit[10];
            var size = Physics.RaycastNonAlloc(ray, result);

            for (var i = 0; i < size; i++) {
                var _object = result[i];
                
                switch (_object.collider.tag) {
                    case "Untagged":
                        continue;
                    case "Card":
                        break;
                    default:
                        // field_slot 은 Field 스크립트가 부모 오브젝트에 있기 때문에 따로 처리 해줌.
                        var __object = _object.transform.CompareTag("Field_Slot") ? _object.transform.parent : _object.transform;
                        if (__object.TryGetComponent<Zone.Zone>(out var comp)) {
                            Debug.Log(int.Parse(_object.transform.name) - 1);
                            comp.add_card(card_component, int.Parse(_object.transform.name) - 1);
                        }
                        else {
                            // TODO Zone 을 상속 받은 Object 가 아닌 경우.
                        }
                        break;
                }
            }
        }

        private void OnMouseDrag() {
            var target_position = Camera.main!.ScreenToWorldPoint(UnityEngine.Input.mousePosition - mousePosition);
            
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