using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("몬스터 정보")]
    public int currentHealth;
    public int monsterColor;

    private new Renderer renderer; // 렌더러 컴포넌트
    public Color originalColor; // 원래 머티리얼

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            originalColor = renderer.material.color; // 초기 원래 머티리얼 저장
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
