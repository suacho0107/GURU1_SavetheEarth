using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public float damage = 1f;

    private Rigidbody2D rb;
    public ParticleSystem enemyHitEffect;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Health System�� ������ ������Ʈ�� ����(�÷��̾)
        if (col.gameObject.GetComponent<HealthSystem>() != null)
        {
            ParticleSystem _ps = Instantiate(enemyHitEffect, gameObject.transform.position, transform.rotation);
            Destroy(_ps.gameObject, 1);

            HealthSystem _healthSystem = col.gameObject.GetComponent<HealthSystem>();
            _healthSystem.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
