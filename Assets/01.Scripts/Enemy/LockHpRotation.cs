using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHpRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // �ʱ� ȸ�� �� ����
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // �� �����Ӹ��� �ʱ� ȸ�� ������ ����
        transform.rotation = initialRotation;
    }
}
