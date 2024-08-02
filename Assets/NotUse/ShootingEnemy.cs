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

        // 플레이어와의 거리가 minimum 거리보다 가깝다면 후퇴(retreat)
        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }
}
