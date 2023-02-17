using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBase))]
public abstract class MovableBase : MonoBehaviour
{
    protected StatBase stat;
    protected Collider collider; // 콜라이더
    protected Animator anim;     // 애니메이터
    protected Enums.State state = Enums.State.Default; // 상태(FSM을 위한)

    // 추상 메서드들
    abstract public float Attack();      // 공격관련 메서드
    abstract public float GetDamage(float damage, MovableBase from);   // 피격관련 메서드
    abstract public float GetHeal();     // 체력회복 메서드
    abstract public void Move();        // 이동관련 메서드

    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }
}
