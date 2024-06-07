using System.Collections;
using UnityEngine;
public class fort_arc: MonoBehaviour
{
    public float speed = 5f;
    public float searchRadius = 0.1f; // 尋找敵人的範圍
    [SerializeField] GameObject fort, attackPointLeft, attackPointRight, bullet, muzzleflash;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(attack());
        StartCoroutine(search());
    }

    IEnumerator search()
    {
        while (true)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Enemy"));
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                Collider closestEnemy = null;
                float closestDistance = Mathf.Infinity;
                // 找到最近的敵人
                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(fort.transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
                if (closestEnemy != null)
                {
                    Vector3 direction = closestEnemy.transform.position - transform.position;
                    Quaternion toRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(fort.transform.rotation, toRotation, speed * Time.deltaTime);
                }
            }
            yield return null;
        }
    }
      
    IEnumerator attack()
    {
        while (true)
        {
            Ray leftRay = new Ray(attackPointLeft.transform.position, attackPointLeft.transform.forward), rightRay = new Ray(attackPointRight.transform.position, attackPointRight.transform.forward);
            RaycastHit leftHit, rightHit;

            if (Physics.Raycast(leftRay, out leftHit, 10f))
            {
                Destroy(Instantiate(muzzleflash, attackPointLeft.transform.position, attackPointLeft.transform.rotation), 1f);
                audioSource.Play();
                GameObject Bullet = Instantiate(bullet, attackPointLeft.transform.position, Quaternion.LookRotation(attackPointLeft.transform.forward));
                Bullet.GetComponent<Rigidbody>().AddForce(attackPointLeft.transform.forward * 1000);

                yield return new WaitForSeconds(0.1f);
            }

            if (Physics.Raycast(rightRay, out rightHit, 10f))
            {
                Destroy(Instantiate(muzzleflash, attackPointRight.transform.position, attackPointRight.transform.rotation), 1f);
                audioSource.Play();
                GameObject Bullet = Instantiate(bullet, attackPointRight.transform.position, Quaternion.LookRotation(attackPointRight.transform.forward));
                Bullet.GetComponent<Rigidbody>().AddForce(attackPointRight.transform.forward * 1000);

                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }
}