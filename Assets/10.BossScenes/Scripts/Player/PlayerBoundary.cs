using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public float minX = 0f;
    public float maxX = 1920f;
    public float minY = 0f;
    public float maxY = 1080f;

    float currentTime = 0f;
    public float aidTime = 8f;

    public GameObject aidKit;
    private void Update()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

        currentTime += Time.deltaTime;
        if (currentTime > aidTime) {
            float RandomX = Random.Range(minX, maxX);
            float RandomY = Random.Range(minY, maxY);

            GameObject aid = Instantiate(aidKit);
            aid.transform.position = new Vector3(RandomX, RandomY, 0);
            Destroy(aid,3f);

            currentTime = 0f;
        }
    }
}
