using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    public float damage = 0.5f;
    public float damageRadius = 0.5f;    // 데미지를 가하는 범위

    private void Start()
    {
        // 오브젝트가 생성되자마자 범위 내의 적에게 데미지를 가함
        DealDamageToEnemiesInRange();

        // 오브젝트가 범위 내의 적에게 데미지를 가한 후 파괴
        Destroy(gameObject, 3f);
    }

    private void DealDamageToEnemiesInRange()
    {
        // 범위 내의 모든 콜라이더를 가져옴
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, damageRadius);

        foreach (var hit in hits)
        {
            // Enemy 태그를 가진 오브젝트를 찾음
            if (hit.CompareTag("Enemy"))
            {
                HealthSystem healthSystem = hit.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    // HealthSystem 컴포넌트를 가지고 있는 경우 데미지를 가함
                    healthSystem.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 편집기에서 범위를 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

