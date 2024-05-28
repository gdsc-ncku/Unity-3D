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

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
        Ray ray = new(OriginAttackPoint, rb.velocity.normalized);
        RaycastHit[] hits = Physics.RaycastAll(ray, (collider.transform.position - OriginAttackPoint).magnitude);

        if (collider.CompareTag("bullet") || collider.gameObject == AttackWeapon.gameObject) return;
        
        EnemyAI EnemyInfo = collider.gameObject.GetComponent<EnemyAI>();
        if (EnemyInfo != null)
        {
            EnemyInfo.ReduceHealth(AttackWeapon.ThisWeapon.damage);
        }

        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.gameObject.CompareTag("bullet") && AttackWeapon.gameObject != hit.collider.gameObject && hit.collider.gameObject == collider.gameObject)
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
