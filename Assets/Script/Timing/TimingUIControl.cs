using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class TimingUIControl : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInformation playerInformation;

    public TMP_Text jugdeNumber;
    public Image centerIcon;
    public Image timingIcon;
    public RectTransform position_timingIcon;
    public float moveDistance = 1400f; // �̵� �Ÿ�
    public float iconDestroydeay = 1.1f; // �ı� �ð�
    public float iconSpeed = 1.1f; 
    public float iconFadeDuration = 1f; // ���̵� ��(������ ��Ÿ����) �ð�


    private float timeSinceLastCreation = 0f;
    private float creationInterval = 2f; // 1��

    private bool hasExecuted = false;


    private float jugdeNum;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInformation = FindObjectOfType<PlayerInformation>();
        playerInformation.IsMenu = true;
    }
    private void Update()
    {
        ShowJugdeNumber();
    }
    private void FixedUpdate()
    {
        timeSinceLastCreation += Time.fixedUnscaledDeltaTime;

        if (timeSinceLastCreation >= creationInterval)
        {
            CreateTimingIcon();
            timeSinceLastCreation = 0.0f; // �ʱ�ȭ�ؼ� ���� ȣ����� ��ٸ��ϴ�.
        }
    }

    private void CreateTimingIcon()
    {
        float startTime = Time.time;

        Image timingIcon_ = Instantiate(timingIcon, position_timingIcon.position, Quaternion.identity);
        timingIcon_.transform.SetParent(position_timingIcon.transform);
        timingIcon_.rectTransform.anchoredPosition = Vector2.zero;
        timingIcon_.color = new Color(0f, 0f, 0f, 1f); // �ʱ� ���İ� 0, ���������� ����
        Tweener timingIcon_main = timingIcon_.rectTransform.DOAnchorPosX(moveDistance, iconSpeed).SetEase(Ease.Linear);

        timingIcon_main.OnUpdate(() =>
        {
            // centerIcon�� timingIcon_�� ������ ����մϴ�.
            Vector3 centerPosition = centerIcon.rectTransform.position;
            Vector3 timingIconPosition = timingIcon_.rectTransform.position;

            // �� �������� ���� �Ÿ� �ȿ� ������ �� ������ �����ϵ��� ����
            float distanceThreshold = 10f; // ������ ���� �Ÿ� �Ӱ谪
            if (!hasExecuted && Vector3.Distance(centerPosition, timingIconPosition) <= distanceThreshold)
            {
                // centerIcon�� timingIcon_�� ������ �� ������ �ڵ带 ���⿡ �ۼ�
                RhythmAnimationCompleted(centerIcon);
                hasExecuted = true; // �� ���� ����ǵ��� �÷��׸� ����
            }
        });

        StartCoroutine(DestroyAfterDelay(timingIcon_.gameObject, iconDestroydeay, startTime));

    }

    public void RhythmAnimationCompleted(Image rhythmImage)
    {
        // �̹����� ������ �� �Ͼ ��
        Debug.Log("�̹����� ���ƽ��ϴ�");
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay, float startTime)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null)
        {
            hasExecuted = false;

            Destroy(obj);
        }
    }








    public void SceneTurnMenu()
    {
        SceneManager.LoadScene("Menu");
        gameManager.soundTime.Stop();
    }

    public void jugdeMinus()
    {
        if(playerInformation.Jugde > -20)
        {
            playerInformation.Jugde -= 1;
        }
    }

    public void jugdePlus()
    {
        if (playerInformation.Jugde < 20)
        {
            playerInformation.Jugde += 1;
        }
    }

    private void ShowJugdeNumber()
    {
        if(playerInformation.Jugde < 0)
        {
            jugdeNum = playerInformation.Jugde * (0.1f);
            jugdeNumber.text = jugdeNum.ToString();
        }
        else if(playerInformation.Jugde == 0)
        {
            jugdeNumber.text = "0";
        }
        else if(playerInformation.Jugde > 0)
        {
            jugdeNum = playerInformation.Jugde * (0.1f);
            jugdeNumber.text = "+" + jugdeNum.ToString();
        }
       
    }

}
