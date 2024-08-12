using System;
using System.Collections.Generic;
using Transform;
using UnityEngine;

// Card 애니메이션 수행 시, 사용되는 위치값을 모아둔 class
public class Constant : MonoBehaviour {
    public static Vector3 card_size_while_movement = new Vector3(3.3f, 0.2f, 4.7f);
    public static Vector3 card_size_before_place_to_field = new Vector3(3.3f * 1.5f, 0.2f, 4.7f * 1.5f);
}