using UnityEngine;

[CreateAssetMenu(fileName = "EEStudent", menuName = "Character/EEStudent")]
public class EEStudent : CharacterBaseData
{
    private float healthRate;
    private float attackRate;
    private float walkSpeedRate;
    private float defenseRate;
    private float q_SkillDamageRate;
    private float e_SkillDamageRate;
    public override float HealthRate
    {
        get
        {
            return healthRate;
        }
        set
        {
            healthRate = value;
            PlayerPrefs.SetFloat("EEHealthRate", value);
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
            PlayerPrefs.SetFloat("EEAttackRate", value);
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
            PlayerPrefs.SetFloat("EEWalkSpeedRate", value);
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
            PlayerPrefs.SetFloat("EEDefenseRate", value);
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
            PlayerPrefs.SetFloat("EEQ_SkillDamageRate", value);
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
            PlayerPrefs.SetFloat("EEE_SkillDamageRate", value);
        }
    }
    // You can create specific strengthening below.
    #region Hero_Q_Skill
    public float throwForce;
    public float throwUpwardForce;
    #endregion

    #region Hero_E_Skill

    #endregion

    // throwing run time error bomb
    public void UseQSkill(Transform cam, Transform attackPoint, GameObject objectToThrow)
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + cam.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    public void UseingE_Skill()
    {
        
    }
}
