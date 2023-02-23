using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   

    public static Vector3 ToDirection(this float value)
    {
        //                                        �̰� ���Ͻø�
        //                                         360���� 2PI�� �ſ�!
        return new Vector3(Mathf.Cos(value * Mathf.Deg2Rad), Mathf.Sin(value * Mathf.Deg2Rad));
    }

    /// <summary> ����, ���� ������ ������ ���شٶ�� �Ѵٸ� �ű⿡ �°Բ� 3���� ������ �˷����! </summary>
    /// <param name="angles"> ������ ���� �����Դϴ�! x�� ���򰢵�, y�� ���������� �־��ּ���! </param>
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
        //������ ������ְ� ���ϴ�!     �̰� �ݴ�� ������ ��������!
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
