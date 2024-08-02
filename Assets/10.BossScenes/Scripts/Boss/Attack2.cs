using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour
{
    float speed;
    Vector3 dir = -Vector3.up;

    public int damage = 40;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(7f, 15f);    
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
