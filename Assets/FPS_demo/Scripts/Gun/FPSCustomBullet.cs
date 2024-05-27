using UnityEngine;
using UnityEngine.UIElements;

public class FPSCustomBullet : MonoBehaviour
{
    //Assignables
    public WeaponsDataFetch AttackWeapon;
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

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

    private void Explode(Vector3 position)
    {
        //Instantiate explosion
        if (explosion != null) Destroy(Instantiate(explosion, position, Quaternion.identity), 1f);

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(explosionSound);
        }     
    }

    private void OnTriggerEnter(Collider collider)
    {
        Ray ray = new(AttackWeapon.weaponAttackPoint.position, rb.velocity.normalized);
        RaycastHit hit;

        if (collider.CompareTag("bullet") || collider.gameObject == AttackWeapon.gameObject) return;
        
        EnemyAI EnemyInfo = collider.gameObject.GetComponent<EnemyAI>();
        if (EnemyInfo != null)
        {
            EnemyInfo.ReduceHealth(AttackWeapon.ThisWeapon.damage);
        }

        if (Physics.Raycast(ray, out hit, (collider.transform.position - AttackWeapon.weaponAttackPoint.position).magnitude) && !hit.collider.gameObject.CompareTag("bullet") && AttackWeapon.gameObject != hit.collider.gameObject)
        {
            Explode(hit.point);
        }
        Destroy(gameObject);
    }
}
