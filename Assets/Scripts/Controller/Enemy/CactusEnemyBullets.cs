using System.Collections;
using UnityEngine;

public class CactusEnemyBullets : MonoBehaviour
{
    public GameObject Spawner;
    private Rigidbody rb;
    private float parts, totalTime = 0;
    private bool attacking = false;
    private float zoom;
    private Vector3 Enemy;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] CactusEnemy cactusEnemy;

    // Start is called before the first frame update
    void Start()
    {
        parts = 1f / 60f;
        rb = GetComponent<Rigidbody>();
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        Enemy = Spawner.transform.position;
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
        transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.GetChild(0).transform.position = Enemy + new Vector3(0, cactusEnemy.BulletZoomInEverySecond - 2f, 0);
        zoom = cactusEnemy.BulletFinalSize * parts / cactusEnemy.BulletChargingTime;
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        Enemy = Spawner == null ? Enemy : Spawner.transform.position;
        if (attacking && transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else if(!attacking)
        {
            transform.GetChild(0).transform.position = Enemy + new Vector3(0, cactusEnemy.BulletZoomInEverySecond - 2f, 0);
            gameObject.transform.position = Enemy + new Vector3(0, cactusEnemy.BulletZoomInEverySecond, 0);
        }
        gameObject.transform.RotateAround(transform.position, Vector3.up, 1f);
    }

    IEnumerator attack()
    {
        while (totalTime < cactusEnemy.BulletChargingTime)
        {
            gameObject.transform.localScale += Vector3.one * zoom;
            totalTime += parts;
            yield return new WaitForSeconds(parts);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cactusEnemy.AttackRange, bulletAim);
        if (hitColliders.Length > 0)
        {
            rb.AddForce(rb.mass * Random.Range(10f, 15f) * (hitColliders[0].transform.position - transform.position).normalized, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(rb.mass * Random.Range(50f, 80f) * Vector3.up, ForceMode.Impulse);
            Destroy(gameObject, 2f);
        }
        attacking = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            gameObject.transform.localScale -= (Vector3.one * zoom * 5f);
        }

        if(!attacking || (Spawner == null || other.gameObject == Spawner) || ((1 << other.gameObject.layer) & gameObject.layer) != 0)
        {
            return;
        }

        Vector3 sizeA = GetObjectSize(gameObject);
        Vector3 sizeB = GetObjectSize(other.gameObject);

        float volumeA = sizeA.x * sizeA.y * sizeA.z;
        float volumeB = sizeB.x * sizeB.y * sizeB.z;

        float percentage = (volumeB / volumeA) * 100;

        if (volumeA <= 0.01f)
        {
            Destroy(gameObject);
        }

        if (((1 << other.gameObject.transform.root.gameObject.layer) & bulletAim) != 0)
        {
            PlayerInfo.ReduceHealth(cactusEnemy.AttackDamage / cactusEnemy.BulletFinalSize * gameObject.transform.localScale.x);
            other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * volumeA, ForceMode.Impulse);
        }
        else if(percentage < 75f)
        {
            return;
        }

        Destroy(gameObject);
    }

    private Vector3 GetObjectSize(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
