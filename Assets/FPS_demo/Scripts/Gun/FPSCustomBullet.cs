using UnityEngine;

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

    int collisions;
    PhysicMaterial physics_mat;

    public AudioClip explosionSound;
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(explosionSound);
        }


        //Add a little delay, just to make sure everything works fine
        //Invoke("Delay", 0.05f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Don't count collisions with other bullets
        if (collider.CompareTag("Bullet") || collider.gameObject == AttackWeapon.gameObject) return;

        EnemyAI EnemyInfo = collider.gameObject.GetComponent<EnemyAI>();
        if (EnemyInfo != null)
        {
            EnemyInfo.ReduceHealth(AttackWeapon.ThisWeapon.damage);
        }
        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        //collision.collider.CompareTag("Enemy") && 
        if (explodeOnTouch) Explode();
    }

    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        if (GetComponent<SphereCollider>() != null)
            GetComponent<SphereCollider>().material = physics_mat;
        else if (GetComponent<BoxCollider>() != null)
            GetComponent<BoxCollider>().material = physics_mat;
        else if (GetComponent<CapsuleCollider>() != null)
            GetComponent<CapsuleCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }


}
