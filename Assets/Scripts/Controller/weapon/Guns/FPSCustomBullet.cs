using UnityEngine;
using UnityEngine.UIElements;

public class FPSCustomBullet : MonoBehaviour
{
    //Assignables
    public WeaponsDataFetch AttackWeapon;
    public Rigidbody rb;
    public GameObject explosion, target;
    private Vector3 OriginAttackPoint;

    //Lifetime
    public float maxLifetime;

    public AudioClip explosionSound;
    private void Start()
    {
        OriginAttackPoint = AttackWeapon.weaponAttackPoint.position;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
        if (collider.transform.root.gameObject != target || target == null) return;

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

        Ray ray = new(OriginAttackPoint, rb.velocity.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, (collider.transform.position - OriginAttackPoint).magnitude * 2);
        GameObject explosionObj;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.root.gameObject == collider.transform.root.gameObject)
            {
                if (explosion != null)
                {
                    explosionObj = Instantiate(explosion, hit.point, Quaternion.identity);
                    ExplosionDamageController explosionDamageController;
                    if(explosionObj.TryGetComponent(out explosionDamageController))
                    {
                        explosionDamageController.bullet = gameObject;
                        explosionDamageController.hit = hit;
                    }
                    Destroy(explosionObj, 1f);
                }

                if (GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().PlayOneShot(explosionSound);
                }
                break;
            }
        }

        Destroy(gameObject);
    }
}
