using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7.0f;
    Vector3 dir = Vector3.right;

    int damage;

    public GameObject Explosion;

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            GameObject explosion = Instantiate(Explosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 0.3f);

            damage = Random.Range(1, 5);
            collision.gameObject.GetComponent<Boss>().Damaged(damage);
            Destroy(this.gameObject);
        }
    }
}
