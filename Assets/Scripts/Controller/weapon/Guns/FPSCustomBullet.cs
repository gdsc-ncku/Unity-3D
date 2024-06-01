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

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        Ray ray = new(OriginAttackPoint, rb.velocity.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, (collider.transform.position - OriginAttackPoint).magnitude * 2);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.root.gameObject == collider.transform.root.gameObject)
            {
                if (explosion != null)
                {
                    Destroy(Instantiate(explosion, hit.point, Quaternion.identity), 1f);
                }

                if (GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().PlayOneShot(explosionSound);
                }
            }
        }

        Destroy(gameObject);
    }
}
