using UnityEngine;

[CreateAssetMenu(fileName = "MEStudent", menuName = "Character/MEStudent")]
public class MEStudent : CharacterBaseData
{
    #region Hero_Q_Skill
    public float QskillRange = 10f;
    #endregion

    #region Hero_E_Skill

    #endregion

    // The turret falls and hits the enemy
    public void UseingQ_Skill(Transform cam, Transform attackPoint, LayerMask whatIsEnemy, GameObject effectPrefab)
    {
        // Cast a ray to detect enemy
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, QskillRange, whatIsEnemy))
        {
            // Instantiate effect at enemy position
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }
    }

    // This method combines skill activation.
    public void UseingE_Skill()
    {
        
    }
}
