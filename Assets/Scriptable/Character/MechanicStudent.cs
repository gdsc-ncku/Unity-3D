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
    #endregion

    #region Hero_E_Skill
    #endregion

    //Hero skills effect need to write in the functions below.
    public GameObject objectTo;
    public GameObject[] countObjects;
    public int fortnum=0;
    public int fortready=1;


    public new void UseingQ_Skill()
    {
        countObjects = GameObject.FindGameObjectsWithTag("fort");
        fortnum=countObjects.Length;
        // Debug.Log(fortnum);
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        GameObject myObject = objectTo;
        Vector3 objectPos = cameraPos + cameraForward * 10;

        objectPos.y = cameraPos.y;

        if(fortnum>20)
        {
            fortready=0;
        }
        else if(fortnum<=20 && fortready==1){
            fortready=1;
            GameObject instantiatedObject=Instantiate(myObject, objectPos, Quaternion.identity);
            Destroy(instantiatedObject,10f);
        }
    }

    public new void UseingE_Skill()
    {

    }

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
