using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    public float damage = 0.5f;
    public float damageRadius = 0.5f;    // �������� ���ϴ� ����

    private void Start()
    {
        // ������Ʈ�� �������ڸ��� ���� ���� ������ �������� ����
        DealDamageToEnemiesInRange();

        // ������Ʈ�� ���� ���� ������ �������� ���� �� �ı�
        Destroy(gameObject, 3f);
    }

    private void DealDamageToEnemiesInRange()
    {
        // ���� ���� ��� �ݶ��̴��� ������
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, damageRadius);

        foreach (var hit in hits)
        {
            // Enemy �±׸� ���� ������Ʈ�� ã��
            if (hit.CompareTag("Enemy"))
            {
                HealthSystem healthSystem = hit.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    // HealthSystem ������Ʈ�� ������ �ִ� ��� �������� ����
                    healthSystem.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // �����⿡�� ������ �ð������� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

