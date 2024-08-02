using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // moving projectile towards to player
    Vector3 targetPosition;

    private float speed = 1f;

    void Start()
    {
        targetPosition = FindObjectOfType<PlayerController>().transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}
