using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("���� ����")]
    public int currentHealth;
    public int monsterColor;

    private new Renderer renderer; // ������ ������Ʈ
    public Color originalColor; // ���� ��Ƽ����

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            originalColor = renderer.material.color; // �ʱ� ���� ��Ƽ���� ����
        }
        else
        {
            Debug.LogError("Renderer component not found on this GameObject.");
        }
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        renderer.material.color = Color.black;

        if (currentHealth <= 0)
        {
            Die();
        }

        Invoke("ColorBack", 0.1f);
    }

    private void ColorBack()
    {
        renderer.material.color = originalColor;
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
