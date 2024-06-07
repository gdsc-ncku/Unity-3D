using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fortBullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;

    //Lifetime
    public float maxLifetime;

    public AudioClip explosionSound;
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        Destroy(gameObject, maxLifetime);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.normalized != Vector3.zero)
        {
            transform.forward = rb.velocity.normalized;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.gameObject == gameObject.transform.root.gameObject) return;
        MeshRenderer meshRenderer;
        TrailRenderer trailRenderer;
        if (TryGetComponent(out meshRenderer))
        {
            meshRenderer.enabled = false;
        }

        if (TryGetComponent(out trailRenderer))
        {
            trailRenderer.enabled = false;
        }

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(explosionSound);
        }

        Destroy(gameObject);
    }
}
