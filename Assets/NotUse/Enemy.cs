using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 2f;

    // �÷��̾� ����: enemy ������Ʈ�� �浹
    // Attack �ִϸ��̼� ��� �� enemy ��ġ �̵�&�÷��̾�� �浹 �� �÷��̾��� damage() �Լ� ȣ��
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Health System�� ������ ������Ʈ�� ����(�÷��̾)
        if (col.gameObject.GetComponent<HealthSystem>() != null)
        {
            HealthSystem _healthSystem = col.gameObject.GetComponent<HealthSystem>();
            _healthSystem.TakeDamage(damage);
        }
    }
}
