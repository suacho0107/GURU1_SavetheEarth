using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    Vector3 _startPosition;
    Vector3 _targetPosition;
    float _progress;

    [SerializeField] float _speed = 40f;

    void Start()
    {
        _startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition.WithAxis(Axis.Z, -1);
    }
}
