using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 2f;

    // 플레이어 공격: enemy 오브젝트와 충돌
    // Attack 애니메이션 출력 후 enemy 위치 이동&플레이어와 충돌 시 플레이어의 damage() 함수 호출
    private void OnCollisionEnter2D(Collision2D col)
    {
        // Health System이 부착된 오브젝트만 적용(플레이어만)
        if (col.gameObject.GetComponent<HealthSystem>() != null)
        {
            HealthSystem _healthSystem = col.gameObject.GetComponent<HealthSystem>();
            _healthSystem.TakeDamage(damage);
        }
    }
}
