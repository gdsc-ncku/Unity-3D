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
    #endregion

    #region Hero_E_Skill
    public float ESkillRange = 10f;
    #endregion

    // document falls around and hits the enemy
    public void UseingQ_Skill(Transform player, LayerMask whatIsEnemy, GameObject effectPrefab)
    {
        // Instantiate effect at player position
        Instantiate(effectPrefab, player.position, Quaternion.identity);

        // Find all enemies within range
        Collider[] enemiesInRange = Physics.OverlapSphere(player.position, QSkillRange, whatIsEnemy);

        // Log the names of all enemies within range
        foreach (Collider enemy in enemiesInRange)
        {
            Debug.Log("Enemy in range: " + enemy.gameObject.name);
        }
    }

    // Throw the gavel at the enemy
    public void UseingE_Skill(Transform cam, Transform attackPoint, LayerMask whatIsEnemy, GameObject effectPrefab)
    {
        // Cast a ray to detect enemy
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ESkillRange, whatIsEnemy))
        {
            // Instantiate effect at enemy position
            Instantiate(effectPrefab, hit.point, Quaternion.identity);
            Debug.Log("Skill activated at enemy position: " + hit.point);
        }
    }
}
