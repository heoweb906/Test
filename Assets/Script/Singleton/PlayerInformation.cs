using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static PlayerInformation instance;

    [Header("�÷��̾� ����")]
    [SerializeField]private bool isGame;
    [SerializeField]private bool isMenu;
    [SerializeField]private int weponColor;
    [SerializeField] private int jugde;


    // ������Ƽ�� ���� �� �׼���
    public bool IsGame { get { return isGame; } set { isGame = value; } }
    public bool IsMenu { get { return isMenu; } set { isMenu = value; } }
    public int WeponColor { get { return weponColor; } set { weponColor = value; } }
    public int Jugde { get { return jugde; } set { jugde = value; }}

    // �̱��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ
    public static PlayerInformation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerInformation>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("PlayerInformation");
                    instance = singleton.AddComponent<PlayerInformation>();
                }
            }
            return instance;
        }
    }

    // �ʿ��� �ʱ�ȭ�� �ٸ� ����� �߰��� �� �ֽ��ϴ�.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
