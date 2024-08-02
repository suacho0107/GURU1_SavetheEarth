using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    public float damage = 1f;

    public ParticleSystem enemyHitEffect;

    Vector3 startPos;   // bullet ���� ����

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // rb.velocity = transform.up * speed;
        rb.velocity = transform.right * speed;
        startPos = GetComponent<Transform>().position;
        Destroy(gameObject, 3);
    }

    // Ʈ���ŷ� ����
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Health System�� ������ ������Ʈ�� ����(�÷��̾)
        if (col.gameObject.GetComponent<HealthSystem>() != null)
        {
            Vector2 _lookDir = startPos - col.transform.position;
            
            ParticleSystem _ps = Instantiate(enemyHitEffect, gameObject.transform.position, Quaternion.LookRotation(_lookDir));
            Destroy(_ps.gameObject, 1);
            
            HealthSystem _healthSystem = col.gameObject.GetComponent<HealthSystem>();
            _healthSystem.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
