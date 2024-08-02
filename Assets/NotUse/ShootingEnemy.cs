using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject projectile;

    private float speed = 1.5f;
    private float minimumDistance = 1.5f;
    private float timeBetweenShots = 2f;
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
