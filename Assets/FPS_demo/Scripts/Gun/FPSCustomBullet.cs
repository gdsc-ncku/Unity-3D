using UnityEngine;
using UnityEngine.UIElements;

public class FPSCustomBullet : MonoBehaviour
{
    //Assignables
    public WeaponsDataFetch AttackWeapon;
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    private Vector3 OriginAttackPoint;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public float explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    PhysicMaterial physics_mat;

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

        EnemyAI EnemyInfo = collider.transform.root.gameObject.GetComponent<EnemyAI>();
        Debug.Log(collider.gameObject.name);
        if (EnemyInfo != null)
        {
            EnemyInfo.ReduceHealth(AttackWeapon.ThisWeapon.damage);
        }

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
