using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    [SerializeField] float currentHealth;

    [SerializeField] ParticleSystem deathParticle;

    public Image mask;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float _healPoint)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += _healPoint;
            UpdateHealthBar();
        }
        else
        {
            Debug.Log("플레이어 체력 max");
            return;
        }
    }

    void UpdateHealthBar()
    {
        // HP 값 적용
        // Mask - Image(Fill Amount): 0 ~ 1
        float fill = currentHealth / maxHealth;
        mask.fillAmount = fill;
        if (currentHealth < 0)
        {
            mask.fillAmount = 0;
        }
    }

    void Die()
    {
        ParticleSystem _ps = Instantiate(deathParticle, gameObject.transform.position, transform.rotation);
        Destroy(_ps.gameObject, 1);

        Destroy(gameObject);
    }
}
