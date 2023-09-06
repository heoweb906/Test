using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static PlayerInformation instance;

    [Header("플레이어 정보")]
    [SerializeField]private bool isGame;
    [SerializeField]private bool isMenu;
    [SerializeField]private int weponColor;
    [SerializeField] private int jugde;


    // 프로퍼티를 통한 값 액세스
    public bool IsGame { get { return isGame; } set { isGame = value; } }
    public bool IsMenu { get { return isMenu; } set { isMenu = value; } }
    public int WeponColor { get { return weponColor; } set { weponColor = value; } }
    public int Jugde { get { return jugde; } set { jugde = value; }}

    // 싱글톤 인스턴스에 접근할 수 있는 프로퍼티
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

    // 필요한 초기화나 다른 기능을 추가할 수 있습니다.
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
