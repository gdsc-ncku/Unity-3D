using UnityEngine;

public class BombCollision : MonoBehaviour
{
    // Define the detection radius
    public float detectionRadius = 1f;
    public GameObject explosionEffect; // Prefab for explosion effect
    private bool hasExploded = false; // Flag to track if the bomb has exploded

    private void OnCollisionEnter(Collision collision)
    {
        // Check if colliders are within the specified radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Display names of objects within the radius
        foreach (var collider in colliders)
        {
            Debug.Log("Projectile collided with: " + collider.gameObject.name);
        }
        
        // Check if the bomb has already exploded
        if (!hasExploded)
        {
            // Play explosion effect
            PlayExplosionEffect();

            // Set the flag to true to indicate that the bomb has exploded
            hasExploded = true;
        }
    }

    void PlayExplosionEffect()
    {
        // If there is an explosion effect prefab, play the explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}
