using UnityEngine;
using UnityEngine.UIElements;

public class FPSCustomBullet : MonoBehaviour
{
    //Assignables
    public WeaponsDataFetch AttackWeapon;
    public Rigidbody rb;
    public GameObject explosion;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.gameObject.CompareTag("bullet") || collider.transform.root.gameObject.CompareTag("Weapon") || collider.transform.root.gameObject == AttackWeapon.transform.root.gameObject) return;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        Ray ray = new(OriginAttackPoint, rb.velocity.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, (collider.transform.position - OriginAttackPoint).magnitude);

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
