using System.Collections;
using UnityEngine;

public class LawStudentESkill : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;
    public KeyCode Key;
    public GameObject effectPrefab;

    public float cooldown = 5f;
    public float range = 10f;

    private bool canUseSkill = true;

    private void Update()
    {
        if (Input.GetKeyDown(Key) && canUseSkill)
        {
            StartCoroutine(UseSkill());
        }
    }

    private IEnumerator UseSkill()
    {
        // Disable skill usage during cooldown
        canUseSkill = false;

        // Cast a ray to detect enemy
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, whatIsEnemy))
        {
            // Instantiate effect at enemy position
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }

        // Wait for cooldown duration
        yield return new WaitForSeconds(cooldown);

        // Enable skill usage after cooldown
        canUseSkill = true;
    }
}
