using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet01 : MonoBehaviour
{
    public float speed = 5f;         // ÃÑ¾Ë ¼Óµµ
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (col.CompareTag("Player"))
        {
            HealthSystem _healthSystem = col.gameObject.GetComponent<HealthSystem>();
            _healthSystem.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
