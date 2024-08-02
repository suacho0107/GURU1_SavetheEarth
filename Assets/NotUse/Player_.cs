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
            // Instantiate(�Ѿ�, firePos����);
            Instantiate(_bullet, _firePos);
            _muzzleFlashAnimator.SetTrigger("Shoot");

            // �߻� ����
            var hit = Physics2D.Raycast(
                _firePos.position,
                transform.up,
                _weaponRange
                );

            // ����Ʈ
            var trail = Instantiate(
                _bulletTrail,
                _firePos.position,
                transform.rotation
                );

            // trail ��ü���� BulletTrail ������
            // Raycast�� �浹�� collider�� ��ȯ ��, target trail��ġ�� hit point�� ����
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
