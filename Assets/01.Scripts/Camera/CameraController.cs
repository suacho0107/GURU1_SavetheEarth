using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float smoothing = 2f;

    void Start()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10);
        }
    }

    void Update()
    {
        //if (transform.position != target.position)
        //{
        //    Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 1f, -10);

        //    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        //}
        if (target == null)
        {
            ReloadScene();
        }
        else if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 1f, -10);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("GeneralShooting");
    }
}
