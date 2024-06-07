using System.Collections;
using UnityEngine;

public class ShotGunBullet : MonoBehaviour
{
    [SerializeField] int splitAmount = 5;
    public bool GenOtherBullet = true;
    public float spreadRange = 0.2f;
    private Rigidbody rb;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Calculateforce());
    }

    IEnumerator Calculateforce()
    {
        yield return null;
        if (GenOtherBullet)
        {
            rb = GetComponent<Rigidbody>();
            velocity = rb.velocity;
            rb.velocity = Vector3.zero;
            for (int i = 0; i < splitAmount - 1; i++)
            {
                Vector3 offsetShotgunBullet = new Vector3(Random.Range(-spreadRange, spreadRange), Random.Range(-spreadRange, spreadRange), Random.Range(-spreadRange, spreadRange));
                while (gameObject.transform.position + offsetShotgunBullet == gameObject.transform.position)
                {
                    offsetShotgunBullet = new Vector3(Random.Range(-spreadRange, spreadRange), Random.Range(-spreadRange, spreadRange), Random.Range(-spreadRange, spreadRange));
                }
                GameObject copyBullet = Instantiate(gameObject, gameObject.transform.position + offsetShotgunBullet, gameObject.transform.rotation);
                copyBullet.GetComponent<ShotGunBullet>().GenOtherBullet = false;
                Rigidbody copyBulletRb = copyBullet.GetComponent<Rigidbody>();
                copyBulletRb.velocity = velocity;
            }
            GenOtherBullet = false;
            rb.velocity = velocity;
        }
        yield break;
    }
}
