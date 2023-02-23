using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBase : MonoBehaviour
{
    public string name; // �̸�

    #region ���ȵ�
    [SerializeField] protected float _currentHp;         // ����ü��
    [SerializeField] protected float _maxHp;             // �ִ�ü��
    [SerializeField] protected float _moveSpeed;         // �̼�
    [SerializeField] protected float _attackSpeed;       // ����
    [SerializeField] protected float _resistance;        // ���׷�
    [SerializeField] protected int _attackPower;         // ���ݷ�
    [SerializeField] protected int _defensePower;        // ����
    [SerializeField] protected int _selfRecovery;        // ü����

    public float moveSpeedMultiflier = 2f;

    public float CurrentHp 
    {
        get => _currentHp;
        set => _currentHp = Mathf.Clamp(value, 0, _maxHp); 
    } // ���� ü�� ����
    public float MaxHP { get => _maxHp; }
    public float HPRate { get => _currentHp / _maxHp; } // ü�º���
    public float MoveSpeed { get => _moveSpeed; }
    public float AttackSpeed { get => _attackSpeed; }
    public float Resistance { get => _resistance; }
    public int AttackPower { get => _attackPower; }
    public int DefensePower { get => _defensePower; }
    #endregion
    
}
