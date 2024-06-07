using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MechanicStudent", menuName = "Character/MechanicStudent")]
public class MechanicStudent : CharacterBaseData
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
            PlayerPrefs.SetFloat("MechanicHealthRate", value);
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
            PlayerPrefs.SetFloat("MechanicAttackRate", value);
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
            PlayerPrefs.SetFloat("MechanicWalkSpeedRate", value);
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
            PlayerPrefs.SetFloat("MechanicDefenseRate", value);
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
            PlayerPrefs.SetFloat("MechanicQ_SkillDamageRate", value);
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
            PlayerPrefs.SetFloat("MechanicE_SkillDamageRate", value);
        }
    }

    //You can Create specific strengthening below.
    #region Hero_Q_Skill
    public override void UseingQ_Skill()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 objectPos = cameraPos + cameraForward * 2f;
        RaycastHit hit;
        if (Physics.Raycast(objectPos, -Vector3.up, out hit, Mathf.Infinity))
        {
            objectPos.y = hit.point.y + 0.8f;
        }
         
        GameObject instantiatedObject = Instantiate(Q_Skill, objectPos, Quaternion.identity);
        Destroy(instantiatedObject, 60f);
    }
    #endregion

    #region Hero_E_Skill
    public override void UseingE_Skill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, E_SkillRange))
        {
            Vector3 pos = new Vector3(hit.point.x, 8, hit.point.z);

            // Instantiate effect at player position
            GameObject effect = Instantiate(E_Skill, pos, Quaternion.identity);
            effect.transform.forward = -Vector3.up;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                effect.GetComponent<MEStudentSecondSkill>().target = hit.collider.gameObject;
            }
        }
    }
    #endregion

    private void OnEnable()
    {
        HealthRate = PlayerPrefs.GetFloat("MechanicHealthRate");
        AttackRate = PlayerPrefs.GetFloat("MechanicAttackRate");
        WalkSpeedRate = PlayerPrefs.GetFloat("MechanicWalkSpeedRate");
        DefenseRate = PlayerPrefs.GetFloat("MechanicDefenseRate");
        Q_SkillDamageRate = PlayerPrefs.GetFloat("MechanicQ_SkillDamageRate");
        E_SkillDamageRate = PlayerPrefs.GetFloat("MechanicE_SkillDamageRate");
    }
}
