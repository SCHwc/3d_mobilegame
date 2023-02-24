using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // 발사체의 주인
    public MovableBase owner { get; protected set; }

    // 발사체의 목표물
    public MovableBase focusTarget;

    // 발사체가 할 일들
    protected ProjectileAction[] actions;

    // 충돌을 무시할 리스트
    protected List<GameObject> ignoreList = new List<GameObject>();

    //나가는 방향
    protected Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set
        {
            _direction = value.normalized;
        }
    }


    // 발사체의 특성 수치
    #region
    [Tooltip("발사체의 속도")]
    public float currentSpeed;
    //[Tooltip("발사체의 가속도 - 시간에 따라 점진적으로 변화하는 속도량")]
    //public float acceleration;
    //[Tooltip("발사체의 각속도 - 시간이 지나면 회전")]
    //public float angularSpeed;
    [Tooltip("이 물체가 살아남는 시간")]
    public float leftTime;
    [Tooltip("발사체가 쏜 본인한테도 맞는가?")]
    public bool contactSelf;
    [Tooltip("발사체가 목표물을 계속 따라가는가?")]
    public bool isTracking;
    #endregion
    void Start()
    {
        foreach (Collider current in GetComponentsInChildren<Collider>())
        {
            // 콜라이더가 있는 오브젝트에게 충돌감지 기능을 할당시킨다.
            current.gameObject.AddComponent<ProjectileCollider>();
        }

        // 발사체의 행동들을 모아서 할당시켜두기
        actions = GetComponents<ProjectileAction>();
    }

    void Update()
    {
        if (leftTime <= 0) { Destroy(gameObject); }
        leftTime -= Time.deltaTime; // 발사체의 생존시간

        // 시간이 지날수록 가속
        // currentSpeed += acceleration * Time.deltaTime;

        if (isTracking)
        {  // 목표물을 추적하도록 설정되있다면 목표물을 바라보게 만들고 계속 전진
            Vector3 targetPosition = focusTarget.transform.position;
            targetPosition.y = 1;
            transform.LookAt(targetPosition);
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Direction * currentSpeed * Time.deltaTime;
        }
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {   // 발사체 초기화 할당
        owner = wantOnwer;
        focusTarget = wantTarget;
        isTracking = wantTracking;

        Direction = focusTarget.transform.position;
    }

    public virtual void Activate(GameObject other)
    {
        // 충돌무시 오브젝트라면 return
        if (ignoreList.Contains(other)) { return; }

        if (owner.focusTarget.gameObject == other && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Activate(other.gameObject);
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //대상을 무시하도록 합니다!
    }
}
