using UnityEngine;

[CreateAssetMenu(fileName = "LawStudent", menuName = "Character/LawStudent")]
public class LawStudent : CharacterBaseData
{
    [SerializeField] private float healthRate;
    [SerializeField] private float attackRate;
    [SerializeField] private float walkSpeedRate;
    [SerializeField] private float defenseRate;
    [SerializeField] private float q_SkillDamageRate;
    [SerializeField] private float e_SkillDamageRate;
    public new float HealthRate
    {
        get
        {
            return healthRate;
        }
        set
        {
            healthRate = value;
            PlayerPrefs.SetFloat("LawHealthRate", value);
        }
    }
    public new float AttackRate
    {
        get
        {
            return attackRate;
        }
        set
        {
            attackRate = value;
            PlayerPrefs.SetFloat("LawAttackRate", value);
        }
    }
    public new float WalkSpeedRate
    {
        get
        {
            return walkSpeedRate;
        }
        set
        {
            walkSpeedRate = value;
            PlayerPrefs.SetFloat("LawWalkSpeedRate", value);
        }
    }
    public new float DefenseRate
    {
        get
        {
            return defenseRate;
        }
        set
        {
            defenseRate = value;
            PlayerPrefs.SetFloat("LawDefenseRate", value);
        }
    }
    public new float Q_SkillDamageRate
    {
        get
        {
            return q_SkillDamageRate;
        }
        set
        {
            q_SkillDamageRate = value;
            PlayerPrefs.SetFloat("LawQ_SkillDamageRate", value);
        }
    }
    public new float E_SkillDamageRate
    {
        get
        {
            return e_SkillDamageRate;
        }
        set
        {
            e_SkillDamageRate = value;
            PlayerPrefs.SetFloat("LawE_SkillDamageRate", value);
        }
    }
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
