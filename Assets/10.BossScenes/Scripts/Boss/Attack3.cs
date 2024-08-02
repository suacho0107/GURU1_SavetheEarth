using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3 : MonoBehaviour
{
    public int damage = 40;

    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<PlayerMove>().Damaged(damage);

            speed = -3f;
        }
    }
    public void Attack() {
        speed = 5f;
    }
    public void AttackFail() {
        speed = -3f;
    }
}
