using UnityEngine;

[CreateAssetMenu(fileName = "LawStudent", menuName = "Character/LawStudent")]
public class LawStudent : CharacterBaseData
{
    // You can create specific strengthening below.
    #region Hero_Q_Skill
    public float rangeQ = 10f;
    #endregion

    #region Hero_E_Skill
    public float rangeE = 10f;
    #endregion

    // Hero skills effect need to write in the functions below.
    public new void UseingQ_Skill(Transform player, LayerMask whatIsEnemy ,GameObject effectPrefab)
    {
        /*Instantiate(effectPrefab, player, Quaternion.identity);
        if (Physics.Raycast(ray, out hit, rangeQ, whatIsEnemy))
        {
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }*/
    }

    // This method combines skill activation.
    public void UseingE_Skill(Transform cam, Transform attackPoint, LayerMask whatIsEnemy, GameObject effectPrefab)
    {
        // Cast a ray to detect enemy
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rangeE, whatIsEnemy))
        {
            // Instantiate effect at enemy position
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }
    }
}
