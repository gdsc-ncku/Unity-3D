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
        Destroy(gameObject, maxLifetime);
        Setup();
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


        //Add a little delay, just to make sure everything works fine
        //Invoke("Delay", 0.05f);
        Destroy(gameObject);
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
