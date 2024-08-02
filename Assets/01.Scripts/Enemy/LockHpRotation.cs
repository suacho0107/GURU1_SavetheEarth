using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHpRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // 초기 회전 값 저장
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 매 프레임마다 초기 회전 값으로 복원
        transform.rotation = initialRotation;
    }
}
