using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    public float damage = 1f;

    public ParticleSystem enemyHitEffect;

    Vector3 startPos;   // bullet 생성 지점

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // rb.velocity = transform.up * speed;
        rb.velocity = transform.right * speed;
        startPos = GetComponent<Transform>().position;
        Destroy(gameObject, 3);
    }

    // 트리거로 변경
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Health System이 부착된 오브젝트만 적용(플레이어만)
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
