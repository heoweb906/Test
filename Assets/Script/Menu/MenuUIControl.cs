using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUIControl : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerInformation playerInformation;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerInformation = FindObjectOfType<PlayerInformation>();
        playerInformation.IsMenu = true;
    }

    public void Play()
    {
        SceneManager.LoadScene("Play1"); // "YourSceneName"은 이동하고자 하는 씬의 이름으로 바꿔주세요.
        gameManager.soundManager.Play();
        playerInformation.IsMenu = false;
    }

    public void SceneTurnTiming()
    {
        SceneManager.LoadScene("Timing");
        gameManager.soundTime.Play();
    }




    public void QuitGame() // 게임 종료 버튼 클릭(게임 종료)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
