using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers _instance = null;
    public static Managers Instance { get => _instance; }

    #region 하위 매니저 정적 필드 및 프로퍼티
    private static InputManager _input = new InputManager();

    // Managers초기화 전 사용할 경우 대비하여 get시 초기화 실행 후 반환
    public static InputManager Input { get { Init(); return _input; } }
    #endregion



    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        // 싱글톤
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject("@Managers");

            // 게임오브젝트에 Managers가 있다면 가져오고 없다면 붙여서 반환
            _instance = Utils.GetOrAddComponent<Managers>(go);

            // 씬 전환시에도 파괴되지 않도록 처리
            DontDestroyOnLoad(go);

            #region 하위매니저들 초기화
            _input.Init();
            #endregion

            // 성능 향상을 위한 프레임 고정
            Application.targetFrameRate = 60;
        }
    }
}