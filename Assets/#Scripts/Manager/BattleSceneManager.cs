using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private static BattleSceneManager _instance;
    public static BattleSceneManager Instance { get => _instance; }

    // 실제 게임 창에서 생성된 동료 배열 
    public PartnerBase[] partners = new PartnerBase[3];

    // 전투 시작시 생성될 위치 배열 ( 0~2:동료, 3:플레이어 )
    public Transform[] spawnPosition;
    // QTE 를 위한 동료 스킬버튼
    public AllySkillBtn skillBtn;
    // 스킬 버튼 이미지 배열
    Sprite[] skillIcons = new Sprite[3];

    #region QTE관련 필드
    // qte 동작 시간
    public float qteTime;
    // qte 성공 여부
    public bool isQTESuccess = false;

    // qte 진행 여부
    bool isQTEStarted = false;
    // qte 타이머
    float qteTimer;
    // qte 실행할 인덱스
    int qteIdx;
    #endregion

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        Initialize();
    }

    private void Initialize()
    {
        GameManager.Instance.player.gameObject.transform.position = spawnPosition[3].position;

        // 동료를 번호에 따라 지정된 스폰위치에 생성
        for(int i = 0; i < partners.Length; i++)
        {
            PartnerInfo currentInfo = GameManager.Instance.party[i];
            partners[i] = Instantiate(currentInfo.prefab).GetComponent<PartnerBase>();
            partners[i].gameObject.transform.position = spawnPosition[i].position;
            skillIcons[i] = currentInfo.icon;
        }
    }

    private void Update()
    {
        if(isQTEStarted)
        {            
            qteTimer += Time.deltaTime; // qte 타이머 갱신

            if(qteTimer > qteTime)
            {
                isQTEStarted = false; // qte종료
                skillBtn.gameObject.SetActive(false); // 버튼 비활성화
            }

            // QTE에 성공했다면
            if(isQTESuccess)
            {
                // 마지막QTE를 끝낸다면
                if(qteIdx >= partners.Length -1)
                {
                    isQTEStarted = false; // qte종료
                    skillBtn.gameObject.SetActive(false); // 버튼 비활성화
                    return; // 빠져나가기
                }
                else
                {
                    // 다시 체크하기 위해 초기화
                    isQTESuccess = false;

                    qteIdx++;
                    // 스킬버튼 갱신후 활성화
                    while (partners[qteIdx] == null)
                    {
                        qteIdx++;
                        if (qteIdx >= partners.Length)
                            return;
                    }

                    skillBtn.gameObject.SetActive(false);
                    skillBtn._icon.sprite = skillIcons[qteIdx];
                    skillBtn.SetTarget(partners[qteIdx]);
                    skillBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    // QTE발동 메서드
    public void StartQTE()
    {
        qteIdx = 0;

        while (partners[qteIdx] == null)
        {
            qteIdx++;
            if (qteIdx >= partners.Length)
                return;
        }

        isQTEStarted = true;
        qteTimer = 0f;
        isQTESuccess = false;

        // 스킬버튼 갱신후 활성화
        skillBtn._icon.sprite = skillIcons[qteIdx];
        skillBtn.SetTarget(partners[qteIdx]);
        skillBtn.gameObject.SetActive(true);
    }

}
