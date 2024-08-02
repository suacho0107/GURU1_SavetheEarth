using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    private Vector3 startPos;

    private float floatAmplitude = 0.06f;    // 진폭
    private float floatFrequency = 4f;      // 주기

    private bool isHeld = false;

    void Start()
    {
        offset = new Vector3(0, 1.5f, 0);
        startPos = transform.position;
    }

    private void Update()
    {
        Floating();
        if (isHeld)
        {
            // 플레이어의 머리 위에 아이템 위치 고정
            transform.position = player.position + offset;
        }
    }

    void Floating()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.transform;
            isHeld = true;

            transform.SetParent(player);
            transform.localPosition = offset;
        }
    }
}
