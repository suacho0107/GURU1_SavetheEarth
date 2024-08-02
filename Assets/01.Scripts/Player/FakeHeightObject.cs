using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FakeHeightObject : MonoBehaviour
{
    // 수류탄 프리팹 스크립트 - 이름 변경: 
    public UnityEvent onGroundHitEvent;

    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    public float gravity = -10;
    public Vector2 groundVelocity;
    public float verticalVelocity;
    private float lastIntialVerticalVelocity;

    public bool isGrounded;
    private bool isDestroyed = false; // 오브젝트 파괴 여부

    void Update()
    {
        if (isDestroyed) return; // 오브젝트가 파괴된 경우 Update 실행 중지

        UpdatePosition();
        CheckGroundHit();
    }

    // Player.cs - Shoot()에서 호출
    // 수평 이동 속도와 초기 수직 속도 설정
    public void Initialize(Vector2 groundVelocity, float verticalVelocity)
    {
        isGrounded = false;
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
        lastIntialVerticalVelocity = verticalVelocity;
    }

    // 오브젝트 위치 업데이트
    // - 지면 이동 속도, 초기 수직 속도 값 사용
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

    // 땅에 닿았는지 확인
    void CheckGroundHit()
    {
        if (trnsBody == null || trnsObject == null) return; // 참조가 null인지 확인

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
            onGroundHitEvent.Invoke();  // 이벤트 호출
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

    // 수평 속도 감소
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