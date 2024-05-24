using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : MonoBehaviour
{
    [SerializeField] int splitAmount = 5;
    public bool GenOtherBullet = true;
    public float spreadRange = 0.2f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Calculateforce());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator Calculateforce()
    {
        yield return new WaitForSeconds(0.01f);

        rb = GetComponent<Rigidbody>();

        yield return null;
        if (GenOtherBullet)
        {
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
                copyBulletRb.velocity = rb.velocity;
            }
            GenOtherBullet = false;
        }
    }
}
