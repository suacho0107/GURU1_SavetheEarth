using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    Camera cam;

    public Grenede grenede;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    private void Start()
    {
        cam = Camera.main;
        grenede.DeactivateRb();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    void OnDragStart()
    {
        grenede.DeactivateRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        // »Æ¿ŒøÎ
        Debug.DrawLine(startPoint, endPoint);

        trajectory.UpdateDots(grenede.pos, force);
    }

    void OnDragEnd()
    {
        // ≈ı√¥
        grenede.ActivateRb();
        grenede.Push(force);

        trajectory.HIde();
    }
}
