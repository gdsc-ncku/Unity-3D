using UnityEngine;

public class RunTimeErrorBomb : MonoBehaviour
{
    // Define the detection radius
    public float detectionRadius = 1f;
    public GameObject explosionEffect; // Prefab for explosion effect
    public LayerMask enemyLayer; // Layer mask to filter only enemy objects
    private bool hasExploded = false; // Flag to track if the bomb has exploded

    private void OnCollisionEnter(Collision collision)
    {
        // Check if colliders are within the specified radius and are in the enemy layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        // Display names of objects within the radius
        foreach (var collider in colliders)
        {
            Debug.Log("Projectile collided with: " + collider.gameObject.name);
        }

        // Check if the bomb has already exploded
        if (!hasExploded)
        {
            // Find the closest enemy
            Collider closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider;
                }
            }

            // Play explosion effect at the closest enemy's position
            if (closestEnemy != null)
            {
                Instantiate(explosionEffect, closestEnemy.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }

            // Set the flag to true to indicate that the bomb has exploded
            hasExploded = true;
        }
    }

    void PlayExplosionEffect(Vector3 position)
    {
        // If there is an explosion effect prefab, play the explosion effect at the specified position
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, position, Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}
