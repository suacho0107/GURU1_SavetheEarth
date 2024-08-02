using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FakeHeightObject : MonoBehaviour
{
    // ����ź ������ ��ũ��Ʈ - �̸� ����: 
    public UnityEvent onGroundHitEvent;

    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    public float gravity = -10;
    public Vector2 groundVelocity;
    public float verticalVelocity;
    private float lastIntialVerticalVelocity;

    public bool isGrounded;
    private bool isDestroyed = false; // ������Ʈ �ı� ����

    void Update()
    {
        if (isDestroyed) return; // ������Ʈ�� �ı��� ��� Update ���� ����

        UpdatePosition();
        CheckGroundHit();
    }

    // Player.cs - Shoot()���� ȣ��
    // ���� �̵� �ӵ��� �ʱ� ���� �ӵ� ����
    public void Initialize(Vector2 groundVelocity, float verticalVelocity)
    {
        isGrounded = false;
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
        lastIntialVerticalVelocity = verticalVelocity;
    }

    // ������Ʈ ��ġ ������Ʈ
    // - ���� �̵� �ӵ�, �ʱ� ���� �ӵ� �� ���
    void UpdatePosition()
    {
        if (trnsBody == null || trnsObject == null) return;

        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
            trnsBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        }

        trnsObject.position += (Vector3)groundVelocity * Time.deltaTime;
    }

    // ���� ��Ҵ��� Ȯ��
    void CheckGroundHit()
    {
        if (trnsBody == null || trnsObject == null) return; // ������ null���� Ȯ��

        if (trnsBody.position.y < trnsObject.position.y && !isGrounded)
        {
            trnsBody.position = trnsObject.position;
            isGrounded = true;
            GroundHit();
        }
    }

    void GroundHit()
    {
        if (onGroundHitEvent != null)
        {
            onGroundHitEvent.Invoke();  // �̺�Ʈ ȣ��
        }
    }

    public void Stick()
    {
        groundVelocity = Vector2.zero;
    }

    public void Bounce(float divisionFactor)
    {
        Initialize(groundVelocity, lastIntialVerticalVelocity / divisionFactor);
    }

    // ���� �ӵ� ����
    public void SlowDownGroundVelocity(float divisionFactor)
    {
        groundVelocity = groundVelocity / divisionFactor;
    }

    public void Destroy(float timeToDestruction)
    {
        isDestroyed = true;

        trnsObject = null;
        trnsBody = null;
        trnsShadow = null;

        Destroy(gameObject, timeToDestruction);
    }

}