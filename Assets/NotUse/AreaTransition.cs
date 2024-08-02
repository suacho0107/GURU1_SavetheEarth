using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    private BossCameraController cam;

    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Vector3 movePlayer;

    void Start()
    {
        cam = Camera.main.GetComponent<BossCameraController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 카메라 이동 관련 위치 제한
            //cam.minPosition = newMinPos;
            //cam.maxPosition = newMaxPos;

            col.transform.position += movePlayer;
        }
    }
}
