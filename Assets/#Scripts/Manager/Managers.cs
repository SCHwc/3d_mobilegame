using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers _instance = null;
    public static Managers Instance { get => _instance; }

    #region ���� �Ŵ��� ���� �ʵ� �� ������Ƽ
    private static InputManager _input = new InputManager();
    private static SceneManager _scene = new SceneManager();

    // Managers�ʱ�ȭ �� ����� ��� ����Ͽ� get�� �ʱ�ȭ ���� �� ��ȯ
    public static InputManager Input { get { Init(); return _input; } }
    public static SceneManager Scene { get { Init(); return _scene; } }
    #endregion

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        // �̱���
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject("@Managers");

            // ���ӿ�����Ʈ�� Managers�� �ִٸ� �������� ���ٸ� �ٿ��� ��ȯ
            _instance = Utils.GetOrAddComponent<Managers>(go);

            // �� ��ȯ�ÿ��� �ı����� �ʵ��� ó��
            DontDestroyOnLoad(go);

            #region �����Ŵ����� �ʱ�ȭ
            _input.Init();
            _scene.Init();
            #endregion

            // ���� ����� ���� ������ ����
            Application.targetFrameRate = 60;
        }
    }
}
