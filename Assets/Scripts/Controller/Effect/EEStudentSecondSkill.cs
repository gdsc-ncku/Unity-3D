using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.VFX.Utility;

public class EEStudentSecondSkill : MonoBehaviour
{
    public HashSet<GameObject> records = new();
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    public EnemyAI enemyAI;
    public int connect = 0, maxConnect;

    private void Start()
    {
        StartCoroutine(detect());
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyAI != null)
        {
            transform.position = enemyAI.gameObject.transform.position;
            transform.GetChild(4).transform.position = enemyAI.gameObject.transform.position + new Vector3(0, 1f, 0);
            transform.GetChild(6).transform.position = enemyAI.gameObject.transform.position + new Vector3(0, 1f, 0);
            enemyAI.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate / 2 * Time.deltaTime);
        }
        transform.GetChild(3).transform.position = playerBattleInfo.nowWeaponData.weaponAttackPoint.position;
        transform.GetChild(5).transform.position = playerBattleInfo.nowWeaponData.weaponAttackPoint.position;
    }

    IEnumerator detect()
    {
        while(true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 8f, LayerMask.GetMask("Enemy"));
            foreach (Collider collider in colliders)
            {
                if (collider == null)
                {
                    continue;
                }

                if (!gameObject.transform.root.gameObject.GetComponent<EEStudentSecondSkill>().records.Contains(collider.gameObject) && gameObject.transform.root.gameObject.GetComponent<EEStudentSecondSkill>().connect < maxConnect)
                {
                    gameObject.transform.root.gameObject.GetComponent<EEStudentSecondSkill>().records.Add(collider.gameObject);
                    GameObject laser = Instantiate(gameObject, collider.gameObject.transform.position, Quaternion.identity);
                    laser.transform.parent = gameObject.transform.root.transform;
                    laser.GetComponent<EEStudentSecondSkill>().enemyAI = collider.gameObject.transform.root.gameObject.GetComponent<EnemyAI>();
                    gameObject.transform.root.gameObject.GetComponent<EEStudentSecondSkill>().connect++;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
