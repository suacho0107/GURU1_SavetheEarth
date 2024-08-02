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
        // 매 프레임마다 호출되는 함수라 컴퓨터 성능에 따라 달라질 수 있음
        // moveDir: 방향 설정은 게임 내에서 키입력에 따라 변경되므로,
        // 프레임마다 호출되는 update에서 방향 캐치 담당
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
        // 일정 시간마다 호출, 성능과 무관
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