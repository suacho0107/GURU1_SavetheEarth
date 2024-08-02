using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public float speed = 20f;
    Vector3 dir;
    Vector3 playerPosition;

    public int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform.position;

        dir = (playerPosition - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<PlayerMove>().Damaged(damage);
            Destroy(this.gameObject);
        }
    }
}
