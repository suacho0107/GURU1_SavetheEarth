using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _speed;
    [SerializeField] private float _weaponRange = 10f;

    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private Animator _muzzleFlashAnimator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LookAtMouse();
        Move();
        Shoot_();
    }

    void Shoot_()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate(총알, firePos에서);
            Instantiate(_bullet, _firePos);
            _muzzleFlashAnimator.SetTrigger("Shoot");

            // 발사 로직
            var hit = Physics2D.Raycast(
                _firePos.position,
                transform.up,
                _weaponRange
                );

            // 이펙트
            var trail = Instantiate(
                _bulletTrail,
                _firePos.position,
                transform.rotation
                );

            // trail 객체에서 BulletTrail 가져옴
            // Raycast가 충돌한 collider를 반환 시, target trail위치를 hit point로 설정
            var trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);
                var hittable = hit.collider.GetComponent<IHittable>();
                hittable?.ReceiveHit(hit);
            }
            else
            {
                var endPosition = _firePos.position + transform.up * _weaponRange;
                trailScript.SetTargetPosition(endPosition);
            }
        }
    }

    void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void Move()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _rb.velocity = input.normalized * _speed;
    }
}
