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

    //나가는 방향 각도와 동기화된 변수
    Vector3 _direction;
    //나가는 각도 방향과 동기화된 변수
    float _horizontalAngle;
    float _verticalAngle;

    public Vector3 Direction
    {
        get => _direction;
        set
        {
            //방향이기 때문에 크기는 1로 맞출게요!
            _direction = value.normalized;
            //_angle = _direction.ToAngle();
            _horizontalAngle = _direction.ToHorizontalAngle();
            _verticalAngle = _direction.ToVerticalAngle();
        }
    }
    public float HorizontalAngle
    {
        get => _horizontalAngle;
        set
        {
            _horizontalAngle = value;
            //_direction = value.ToDirection();
            _direction = new Vector2(_horizontalAngle, _verticalAngle).ToDirection();
        }
    }
    public float VerticalAngle
    {
        get => _verticalAngle;
        set
        {
            _verticalAngle = value;
            //_direction = value.ToDirection();
            _direction = new Vector2(_horizontalAngle, _verticalAngle).ToDirection();
        }
    }

    // 발사체의 특성 수치
    #region
    [Tooltip("발사체의 속도")]
    public float currentSpeed;
    [Tooltip("발사체의 가속도 - 시간에 따라 점진적으로 변화하는 속도량")]
    public float acceleration;
    [Tooltip("발사체의 각속도 - 시간이 지나면 회전")]
    public float angularSpeed;
    [Tooltip("이 물체가 살아남는 시간")]
    public float leftTime;
    [Tooltip("발사체가 쏜 본인한테도 맞는가?")]
    public bool contactSelf;
    [Tooltip("발사체가 목표물을 계속 따라가는가?")]
    public bool isTracking;
    #endregion
    void Start()
    {
        // 발사체의 행동들을 모아서 할당시켜두기
        actions = GetComponents<ProjectileAction>();
    }

    void Update()
    {
        if (leftTime <= 0) { Destroy(gameObject); }
        leftTime -= Time.deltaTime;

        // 시간이 지날수록 가속
        currentSpeed += acceleration * Time.deltaTime;
    }

    public void Initialize(MovableBase wantOnwer, MovableBase wantTarget, bool wantTracking)
    {
        owner = wantOnwer;
        focusTarget = wantTarget;
        isTracking = wantTracking;
    }

    public virtual void Activate(GameObject other)
    {
        // 충돌무시 오브젝트라면 return
        if (ignoreList.Contains(other)) { return; }

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            MonsterBase monster = other.GetComponent<MonsterBase>();
            if (monster != null)
            {
                foreach (ProjectileAction current in actions) { current?.Activate(this, monster, transform.position); }
            }
        }

    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //대상을 무시하도록 합니다!
    }
}
