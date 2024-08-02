using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float healPoint = 1f;

    private float floatAmplitude = 0.06f;    // ÁøÆø
    private float floatFrequency = 4f;      // ÁÖ±â
    
    private AudioClip healSound;
    private AudioSource audioSource;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Floating();
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
            HealthSystem healthSystem = col.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Heal(healPoint);

                AudioSource healSound = GetComponent<AudioSource>();
                healSound.Play();
            }
        }

        Destroy(gameObject, 0.1f);
    }
}
