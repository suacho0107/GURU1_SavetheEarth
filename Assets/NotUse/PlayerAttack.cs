using UnityEngine;
using UnityEngine.UI;  // UI 관련 추가

public class PlayerAttack : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform grenadeSpawnPoint;
    public float maxGrenadeSpeed = 15f; // Max speed for the grenade
    public Image aimCircle; // 조준 UI를 위한 이미지
    public LineRenderer aimLine; // 조준선을 위한 LineRenderer

    private GameObject activeGrenade;
    private bool isAiming;
    private Vector3 aimStartPos;

    void Update()
    {
        HandleGrenadeActivation();
        HandleAimingAndThrowing();
    }

    void HandleGrenadeActivation()
    {
        if (Input.GetKeyDown(KeyCode.Q) && activeGrenade == null)
        {
            ActivateGrenade();
        }
    }

    void ActivateGrenade()
    {
        activeGrenade = Instantiate(grenadePrefab, grenadeSpawnPoint.position, Quaternion.identity);
    }

    void HandleAimingAndThrowing()
    {
        if (activeGrenade != null && Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            aimStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimStartPos.z = 0;

            // Initialize aim line positions
            aimLine.positionCount = 2;
            aimLine.SetPosition(0, grenadeSpawnPoint.position);
            aimLine.SetPosition(1, grenadeSpawnPoint.position);
            aimLine.enabled = true;

            // Initialize aim circle position
            aimCircle.transform.position = Camera.main.WorldToScreenPoint(grenadeSpawnPoint.position);
            aimCircle.enabled = true;
        }

        if (Input.GetMouseButton(0) && isAiming)
        {
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;
            Vector3 aimDirection = (currentMousePos - aimStartPos).normalized;
            float aimDistance = Vector3.Distance(currentMousePos, aimStartPos);
            float clampedDistance = Mathf.Clamp(aimDistance, 0, maxGrenadeSpeed);

            // Update aim line
            aimLine.SetPosition(0, grenadeSpawnPoint.position);
            aimLine.SetPosition(1, grenadeSpawnPoint.position + aimDirection * clampedDistance);

            // Update aim circle position
            aimCircle.transform.position = Camera.main.WorldToScreenPoint(grenadeSpawnPoint.position + aimDirection * clampedDistance);
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            ThrowGrenade();
            isAiming = false;
            aimLine.enabled = false;
            aimCircle.enabled = false;
        }
    }

    void ThrowGrenade()
    {
        if (activeGrenade != null)
        {
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;
            Vector3 aimDirection = (currentMousePos - aimStartPos).normalized;
            float aimDistance = Vector3.Distance(currentMousePos, aimStartPos);
            float clampedDistance = Mathf.Clamp(aimDistance, 0, maxGrenadeSpeed);

            Rigidbody2D rb = activeGrenade.GetComponent<Rigidbody2D>();
            rb.velocity = aimDirection * clampedDistance;
            activeGrenade = null;
        }
    }
}
