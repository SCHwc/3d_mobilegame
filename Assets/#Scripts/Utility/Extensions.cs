using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   

    public static Vector3 ToDirection(this float value)
    {
        //                                        이걸 곱하시면
        //                                         360도가 2PI가 돼요!
        return new Vector3(Mathf.Cos(value * Mathf.Deg2Rad), Mathf.Sin(value * Mathf.Deg2Rad));
    }

    /// <summary> 수평, 수직 각도를 전달을 해준다라고 한다면 거기에 맞게끔 3차원 방향을 알려줘요! </summary>
    /// <param name="angles"> 내용을 넣을 벡터입니다! x에 수평각도, y에 수직각도를 넣어주세요! </param>
    public static Vector3 ToDirection(this Vector2 angles)
    {
        Vector3 result;

        
        angles.x *= Mathf.Deg2Rad;
        angles.y *= Mathf.Deg2Rad;

        result.x = Mathf.Cos(angles.x) * Mathf.Abs(Mathf.Cos(angles.y));
        result.z = Mathf.Sin(angles.x) * Mathf.Abs(Mathf.Cos(angles.y));
        result.y = Mathf.Sin(angles.y);

        return result;
    }

    public static Vector3 ToDirection(float horAngle, float verAngle)
    {
        return new Vector2(horAngle, verAngle);
    }

    public static float ToAngle(this Vector3 value)
    {
        //                                          2PI  360
        //각도를 계산해주고 갑니다!     이건 반대로 각도로 만들어줘요!
        return Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
    }

    public static float ToHorizontalAngle(this Vector3 value)
    {
        value.y = 0;
        value.Normalize();

        return Mathf.Atan2(value.z, value.x) * Mathf.Rad2Deg;
    }

    public static float ToVerticalAngle(this Vector3 value)
    {
        return 90 - Mathf.Acos(value.y) * Mathf.Rad2Deg;
    }
}
