using UnityEngine.SceneManagement;
using UnityEngine;

public class Potal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±׸� ���� ������Ʈ�� �浹���� ��
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            // ���� �� �̸��� ���� �ٸ� ������ �̵�
            if (currentSceneName == "Play1")
            {
                SceneManager.LoadScene("Play2"); // �� 1���� �� 2�� �̵�
            }
            else if (currentSceneName == "Play2")
            {
                SceneManager.LoadScene("Play1"); // �� 2���� �� 1�� �̵�
            }
        }
    }
}