using UnityEngine;

public class Gerenade1 : MonoBehaviour
{
    public float explosionDelay = 2f;
    public float explosionRadius = 5f;
    public float explosionForce = 10f;
    public GameObject explosionEffect;

    private void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        // Instantiate explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Detect all objects within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Destroy grenade
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
