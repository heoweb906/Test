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
        if (!(playerInformation.IsMenu))  // 메뉴화면이 아닌 경우에만
        {
            UnlockCursor(); // 커서 락 해제

            SceneManager.LoadScene("Menu"); // "YourSceneName"은 이동하고자 하는 씬의 이름으로 바꿔주세요.
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