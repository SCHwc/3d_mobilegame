using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBase : MonoBehaviour
{
    #region 스탯들
    [SerializeField] protected float _currentHp;         // 현재체력
    [SerializeField] protected float _maxHp;             // 최대체력
    [SerializeField] protected float _moveSpeed;         // 이속
    [SerializeField] protected float _attackSpeed;       // 공속
    [SerializeField] protected float _attackDelay;       // 딜레이
    [SerializeField] protected float _resistance;        // 저항력
    [SerializeField] protected int _attackPower;         // 공격력
    [SerializeField] protected int _defensePower;        // 방어력
    [SerializeField] protected int _selfRecovery;        // 체력젠

    [SerializeField] protected float moveSpeedMultiflier = 2f;

    public float CurrentHp
    {
        get => _currentHp;
        set => _currentHp = Mathf.Clamp(value, 0, _maxHp);
    } // 현재 체력 제한
    public float MaxHP { get => _maxHp; }
    public float HPRate { get => _currentHp / _maxHp; } // 체력비율
    public float MoveSpeed { get => _moveSpeed; }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
        }
    }
    public float AttackDelay
    {
        get => _attackDelay;
        set
        {
            _attackDelay = value;
        }
    }
    public float Resistance { get => _resistance; }
    public int AttackPower { get => _attackPower; }
    public int DefensePower { get => _defensePower; }
    #endregion

}
