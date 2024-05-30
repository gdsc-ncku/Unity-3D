using UnityEngine;

[CreateAssetMenu(fileName = "MEStudent", menuName = "Character/MEStudent")]
public class MEStudent : CharacterBaseData
{
    // You can create specific strengthening below.
    #region Hero_Q_Skill
    public float range = 10f;
    #endregion

    #region Hero_E_Skill
    
    #endregion

    // Hero skills effect need to write in the functions below.
    public new void UseingQ_Skill()
    {
        /*// Cast a ray to detect enemy
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, whatIsEnemy))
        {
            // Instantiate effect at enemy position
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }*/
    }

    // This method combines skill activation.
    public void UseingE_Skill()
    {
        
    }
}
