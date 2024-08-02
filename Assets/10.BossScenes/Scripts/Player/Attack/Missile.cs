using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 4.0f;
    Vector3 dir;
    GameObject enemy;

    public int damage = 30;

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        dir = (enemy.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) {
            GameObject explosion = Instantiate(Explosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 0.6f);

            enemy.GetComponent<Boss>().Damaged(damage);
            Destroy(this.gameObject);
        }
    }
}
