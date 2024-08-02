using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon weapon;

    float moveSpeed = 8.0f;

    Vector2 moveDir;    // moveDir(x, y)
    Vector2 mousePos;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �� �����Ӹ��� ȣ��Ǵ� �Լ��� ��ǻ�� ���ɿ� ���� �޶��� �� ����
        // moveDir: ���� ������ ���� ������ Ű�Է¿� ���� ����ǹǷ�,
        // �����Ӹ��� ȣ��Ǵ� update���� ���� ĳġ ���
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // ���� �ð����� ȣ��, ���ɰ� ����
        Move();
        RotateToMousePos();
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    }

    void RotateToMousePos()
    {
        Vector2 dirToMousePos = mousePos - rb.position;

        transform.up = new Vector2(dirToMousePos.x, dirToMousePos.y);
    }

    public void Shoot()
    {
        weapon.Shoot();
    }
}