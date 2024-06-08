using System.Reflection;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[CreateAssetMenu(fileName = "EEStudent", menuName = "Character/EEStudent")]
public class EEStudent : CharacterBaseData
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
    public override void UseingQ_Skill()
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(Q_Skill, Camera.main.transform.position + Camera.main.transform.forward * 1f, Quaternion.identity);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(Camera.main.transform.forward * 30f, ForceMode.Impulse);
    }

    public override void UseingE_Skill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, E_SkillRange, LayerMask.GetMask("Enemy")))
        {
            GameObject effect = Instantiate(E_Skill, hit.collider.gameObject.transform.position, Quaternion.identity);
            effect.GetComponent<EEStudentSecondSkill>().records.Add(effect);
            effect.GetComponent<EEStudentSecondSkill>().enemyAI = hit.collider.gameObject.transform.root.gameObject.GetComponent<EnemyAI>();
            Destroy(effect, 2f);
        }
    }

    private void OnEnable()
    {
        HealthRate = PlayerPrefs.GetFloat("EEHealthRate");
        AttackRate = PlayerPrefs.GetFloat("EEAttackRate");
        WalkSpeedRate = PlayerPrefs.GetFloat("EEWalkSpeedRate");
        DefenseRate = PlayerPrefs.GetFloat("EEDefenseRate");
        Q_SkillDamageRate = PlayerPrefs.GetFloat("EEQ_SkillDamageRate");
        E_SkillDamageRate = PlayerPrefs.GetFloat("EEE_SkillDamageRate");
    }
}
