using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomedSize = 3f;
    public float zoomDuration = 0.5f;

    private float originalSize;

    Transform player;

    private void Start()
    {
        originalSize = mainCamera.orthographicSize;

        player = GameObject.Find("Player").transform;
    }
    public void StartZoom()
    {
        Vector3 targetPosition = new Vector3(player.position.x + 2, player.position.y, -10);
        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = zoomedSize;
    }
    public void EndZoom()
    {
        mainCamera.transform.position = new Vector3(0, 0, -10);
        mainCamera.orthographicSize = 5f;
    }
}
