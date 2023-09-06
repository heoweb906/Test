using UnityEngine.SceneManagement;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInformation playerInformation;

    public void Awake()
    {
        playerInformation = FindObjectOfType<PlayerInformation>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GoMenu();
        }
    }

    public void GoMenu()
    {
        if (!(playerInformation.IsMenu))  // �޴�ȭ���� �ƴ� ��쿡��
        {
            UnlockCursor(); // Ŀ�� �� ����

            SceneManager.LoadScene("Menu"); // "YourSceneName"�� �̵��ϰ��� �ϴ� ���� �̸����� �ٲ��ּ���.
            gameManager.soundManager.Stop();
            playerInformation.IsMenu = true;
        }
    }
    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}