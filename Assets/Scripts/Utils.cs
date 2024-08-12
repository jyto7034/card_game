using System;
using UnityEngine;

public static class TransformExtensions
{
    public static void With(
        this UnityEngine.Transform transform,
        Vector3? position = null,
        Vector3? rotation = null,
        Vector3? scale = null)
    {
        if (position.HasValue)
            transform.position = position.Value;

        if (rotation.HasValue)
            transform.eulerAngles = rotation.Value;

        if (scale.HasValue)
            transform.localScale = scale.Value;
    }

    // General Funcs
    public static void WithPosition(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        transform.position = transform.position.With(x, y, z);
    }

    public static void WithRotation(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = currentRotation.With(x, y, z);
    }

    public static void WithScale(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        transform.localScale = transform.localScale.With(x, y, z);
    }
    
    
    // Ret Funcs
    public static Vector3 WithPositionRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        return transform.position.With(x, y, z);
    }

    public static Vector3 WithRotationRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        Vector3 currentRotation = transform.eulerAngles;
        return currentRotation.With(x, y, z);
    }

    public static Vector3 WithScaleRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        return transform.localScale.With(x, y, z);
    }
    
    
    // Mul Funcs
    public static void WithPositionMul(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        transform.position = transform.position.WithMul(x, y, z);
    }

    public static void WithRotationMul(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = currentRotation.WithMul(x, y, z);
    }

    public static void WithScaleMul(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        transform.localScale = transform.localScale.WithMul(x, y, z);
    }
    
    // Mul Ret Funcs
    public static Vector3 WithPositionMulRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        return transform.position.WithMul(x, y, z);
    }

    public static Vector3 WithRotationMulRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        Vector3 currentRotation = transform.eulerAngles;
        return currentRotation.WithMul(x, y, z);
    }

    public static Vector3 WithScaleMulRet(this UnityEngine.Transform transform, float? x = null, float? y = null, float? z = null)
    {
        return transform.localScale.WithMul(x, y, z);
    }
}

public static class Vector3Extensions
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(
            x ?? original.x,
            y ?? original.y,
            z ?? original.z
        );
    }
    public static Vector3 WithMul(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(
            original.x * x ?? original.x,
            original.y * y ?? original.y,
            original.z * z ?? original.z
        );
    }
}

public static class Utility {
    public static int get_port() {
        return 0;
    }
    
    public static GameConfig get_game_config() {
        return GameConfig.New();
    }
    
    // public T GetComponent<T>() {
    //     var type = typeof(T);
    //
    //     return type switch {
    //         not null when type == typeof(Card) => (T)(object)card_component,
    //         _ => throw new GameError("GetCompponent() Unknown Type")
    //     };
    // }
}

public class Option<T> {
    private readonly T _value;
    private readonly bool _hasValue;

    private Option(T value, bool hasValue) {
        _value = value;
        _hasValue = hasValue;
    }

    public static Option<T> Some(T value) {
        return new Option<T>(value, true);
    }

    public static Option<T> None() {
        return new Option<T>(default(T), false);
    }

    public bool HasValue => _hasValue;

    public T Value {
        get {
            if (!_hasValue) {
                throw new InvalidOperationException("No value present");
            }
            return _value;
        }
    }
}