using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATController : MonoBehaviour
{
    public GameObject areaTransitions;

    public bool playerInRange;

    void Update()
    {
        if (playerInRange)
        {
            if (areaTransitions.activeInHierarchy)
            {
                areaTransitions.SetActive(false);
            }
            else
            {
                areaTransitions.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInRange = false;
            areaTransitions.SetActive(false);
        }
    }
}
