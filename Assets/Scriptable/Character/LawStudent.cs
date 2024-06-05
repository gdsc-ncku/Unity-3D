using UnityEngine;

[CreateAssetMenu(fileName = "LawStudent", menuName = "Character/LawStudent")]
public class LawStudent : CharacterBaseData
{
    public override float HealthRate
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
    public override float AttackRate
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
    public override float WalkSpeedRate
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
    public override float DefenseRate
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
    public override float Q_SkillDamageRate
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
    public override float E_SkillDamageRate
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
    public float QSkillRange = 10f;

    // document falls around and hits the enemy
    public override void UseingQ_Skill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, Q_SkillRange))
        {
            Vector3 pos = new Vector3(hit.point.x, 4, hit.point.z);

            // Instantiate effect at player position
            Instantiate(Q_Skill, pos, Quaternion.identity);
        }
    }
    #endregion

    #region Hero_E_Skill
    public float ESkillRange = 10f;

    // Throw the gavel at the enemy
    public override void UseingE_Skill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, E_SkillRange))
        {
            Vector3 pos = new Vector3(hit.point.x, 4, hit.point.z);

            // Instantiate effect at player position
            Instantiate(E_Skill, pos, Quaternion.identity);
        }
    }
    #endregion

    private void OnEnable()
    {
        HealthRate = PlayerPrefs.GetFloat("LawHealthRate");
        AttackRate = PlayerPrefs.GetFloat("LawAttackRate");
        WalkSpeedRate = PlayerPrefs.GetFloat("LawWalkSpeedRate");
        DefenseRate = PlayerPrefs.GetFloat("LawDefenseRate");
        Q_SkillDamageRate = PlayerPrefs.GetFloat("LawQ_SkillDamageRate");
        E_SkillDamageRate = PlayerPrefs.GetFloat("LawE_SkillDamageRate");
    }
}
