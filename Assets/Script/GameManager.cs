using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    private static GameManager instance;

    [Header("���� �� ����")]
    public bool rhythmCorrect; // �̰� true�� ���ȿ��� Ű�Է��� ������
    [Space(10f)]


    [Header("������Ʈ")]
    public Canvas canvas;
    public PlayerInformation playerInformation;
    [Space(10f)]


    [Header("���� ����")]
    public AudioSource soundManager; // ���� �������
    public AudioSource soundTime; // Ÿ�̹� ���ߴ� ��Ʈ�γ�
    public Image aim_Around;
    public RectTransform rhythmPosition_1;
    public RectTransform rhythmPosition_2;
    public RectTransform rhythmPosition_sub;
    public Image rhythmSpriteRenderer_left;
    public Image rhythmSpriteRenderer_right;
    public float moveDistance = 245f; // �̵� �Ÿ�
    public float iconDestroydeay = 1.2f; // �ı� �ð�
    public float iconSpeed = 1.1f; 
    public float iconFadeDuration = 1f; // ���̵� ��(������ ��Ÿ����) �ð�
    [Space(10f)]


    [Header("�׽�Ʈ ��")]
    private float timeSinceLastCreation = 0.0f;
    //private float creationInterval = 1f / (130f / 60f); // 1��
    private float creationInterval = 0.5f; // 1��


    private void Awake()
    {
        playerInformation = FindObjectOfType<PlayerInformation>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (!(playerInformation.IsGame))
        {
            Invoke("SetStartGame", playerInformation.Jugde * 0.01f);  // �̰ɷ� ������ ���� ����
        }
    }

    private void SetStartGame()
    {
        playerInformation.IsGame = true;
    }


    private void FixedUpdate()
    {   
        if(playerInformation.IsGame && !(playerInformation.IsMenu))
        {
            timeSinceLastCreation += Time.fixedUnscaledDeltaTime;

            if (timeSinceLastCreation >= creationInterval)
            {
                CreateRhythmIcon();
                timeSinceLastCreation = 0.0f; // �ʱ�ȭ�ؼ� ���� ȣ����� ��ٸ��ϴ�.
            }
        }

    }


    private void CreateRhythmIcon()
    {
        float startTime = Time.time;

        Image RhythmImage_1 = Instantiate(rhythmSpriteRenderer_left, rhythmPosition_1.position, Quaternion.identity);
        RhythmImage_1.transform.SetParent(rhythmPosition_1.transform); // ���� ��ġ�� �ڽ����� ����
        // �̹��� ���� �� �ִϸ��̼� �� ���̵� �� ����
        RhythmImage_1.rectTransform.anchoredPosition = Vector2.zero;
        RhythmImage_1.color = new Color(1f, 0f, 0f, 1f); // �ʱ� ���İ� 0, ���������� ����
        Tweener rhythmTween = RhythmImage_1.rectTransform.DOAnchorPosX(moveDistance, iconSpeed).SetEase(Ease.Linear);
        RhythmImage_1.DOFade(1f, iconFadeDuration); // ���İ� ������ 1�� ����
        StartCoroutine(DestroyAfterDelay(RhythmImage_1.gameObject, iconDestroydeay, startTime)); // ���� �ð� �Ŀ� �̹��� �ı�

        Image RhythmImage_sub = Instantiate(rhythmSpriteRenderer_left, rhythmPosition_sub.position, Quaternion.identity);
        RhythmImage_sub.transform.SetParent(rhythmPosition_sub.transform);
        RhythmImage_sub.rectTransform.anchoredPosition = Vector2.zero;
        RhythmImage_sub.color = new Color(0f, 0f, 0f, 0f); // �ʱ� ���İ� 0, ���������� ����
        Tweener rhythmTween_sub = RhythmImage_sub.rectTransform.DOAnchorPosX(moveDistance, iconSpeed - 0.1f).SetEase(Ease.Linear);

        rhythmTween_sub.OnComplete(() =>
        {
            RhythmAnimationCompleted(aim_Around);
        });

        Image RhythmImage_2 = Instantiate(rhythmSpriteRenderer_right, rhythmPosition_2.position, Quaternion.identity);
        RhythmImage_2.transform.SetParent(rhythmPosition_2.transform); // ���� ��ġ�� �ڽ����� ����
        // �̹��� ���� �� �ִϸ��̼� �� ���̵� �� ����
        RhythmImage_2.rectTransform.anchoredPosition = Vector2.zero;
        RhythmImage_2.color = new Color(1f, 0f, 0f, 1f); // �ʱ� ���İ� 0, ���������� ����
        RhythmImage_2.rectTransform.DOAnchorPosX(-moveDistance, iconSpeed).SetEase(Ease.Linear);
        RhythmImage_2.DOFade(1f, iconFadeDuration); // ���İ� ������ 1�� ����

        StartCoroutine(DestroyAfterDelay(RhythmImage_2.gameObject, iconDestroydeay, startTime)); // ���� �ð� �Ŀ� �̹��� �ı�
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay, float startTime)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null)
        {
            float endTime = Time.time;
            float elapsedTime = endTime - startTime;

            //Debug.Log("�̹����� ������ �� " + elapsedTime + "�� �ڿ� �����");

            Destroy(obj);
        }
    }

    public void RhythmAnimationCompleted(Image rhythmImage)
    {
        StartCoroutine(SetRhythmCorrectWithDelay(0.2f));  // @@@@@@@@@@@@@ 0.2�� ���ȸ� ������ �Ű� ��
        //Debug.Log("RhythmImage_1 �ִϸ��̼� �Ϸ� �� �Լ� ȣ��");
        // rhythmImage�� �ִϸ��̼��� �Ϸ�� �̹����Դϴ�.
    }

    private IEnumerator SetRhythmCorrectWithDelay(float delay)
    {
        rhythmCorrect = true; // rhythmCorrect�� true�� ����
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        rhythmCorrect = false; // ������ �ð� �Ŀ� rhythmCorrect�� �ٽ� false�� ����
    }

}