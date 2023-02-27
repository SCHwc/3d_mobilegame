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
    [Tooltip("광역기인가?")]
    public bool isRangeAttack;
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
            if (focusTarget)
            {
                Vector3 targetPosition = focusTarget.transform.position;
                targetPosition.y = 1;
                transform.LookAt(targetPosition);
                transform.position += transform.forward * currentSpeed * Time.deltaTime;
            }
        }
        else
        {   // 아니라면 초기화 메서드에서 할당받은 방향으로 전진
            Vector3 movePosition = Direction * currentSpeed * Time.deltaTime;

            Vector3 lookPosition = new Vector3(Direction.x, 0, Direction.z).normalized;
            //transform.LookAt(transform.position + lookPosition); 
            transform.rotation=Quaternion.LookRotation(lookPosition);
            transform.position += movePosition;
        }



    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {   // 발사체 초기화 할당
        owner = wantOnwer;
        focusTarget = wantTarget;
        isTracking = wantTracking;
        if (!contactSelf && owner != null) { SetIgnore(owner.gameObject); }
        Direction = focusTarget.transform.position - owner.transform.position;
    }

    public virtual void Activate(GameObject other)
    {
        // 충돌무시 오브젝트라면 return
        if (ignoreList.Contains(other)) { return; }
        // 충돌한 물체가 같은 진영이라면 return
        if (owner.isAlly == other.GetComponent<MovableBase>()?.isAlly) { return; }

        // 몬스터를 공격하기 위한 로직
        if ((owner.focusTarget.gameObject == other && other.gameObject.layer == LayerMask.NameToLayer("Monster"))
            || (isRangeAttack && other.gameObject.layer == LayerMask.NameToLayer("Monster")))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {    // 이 스킬에 달려있는 이벤트들을 모두 실행시킨다.
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

        // 플레이어 진영을 공격하기 위한 로직
        if (owner.focusTarget.gameObject == other && (other.gameObject.layer == LayerMask.NameToLayer("Player")
            || other.gameObject.layer == LayerMask.NameToLayer("Partner")))
        {
            MovableBase target = other.GetComponent<MovableBase>();
            if (target != null)
            {   // 이 스킬에 달려있는 이벤트들을 모두 실행시킨다.
                foreach (ProjectileAction current in actions) { current?.Activate(this, target, transform.position); }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // 콜라이더에서 충돌을 감지하면 충돌한 물체의 정보를 여기서 받아옴 > Activate 함수로 정보 전달후에 충돌 이벤트 실행
        Activate(other.gameObject);
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //대상을 충돌무시한다.(Layer를 활용해서 충돌을 나누면 이 함수 불필요)
    }
}
