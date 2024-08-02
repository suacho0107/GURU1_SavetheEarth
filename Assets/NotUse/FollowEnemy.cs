using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject projectile;

    private float speed;
    private float minimumDistance;
    private float timeBetweenShots;
    private float nextShotTime;

    void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

        // �÷��̾���� �Ÿ��� minimum �Ÿ����� �����ٸ� ����(retreat)
        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }
}
