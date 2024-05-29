using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDataManager : MonoBehaviour
{
    public CharacterBaseData studentData;

    public void Die(GameObject playerDie, GameObject PlayerDieUI)
    {
        StartCoroutine(DieUIDisplay(playerDie, PlayerDieUI));
    }

    IEnumerator DieUIDisplay(GameObject playerDie, GameObject PlayerDieUI)
    {
        yield return new WaitForSeconds(playerDie.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length + 3f);
        PlayerDieUI.SetActive(true);
        GameObject originDie = PlayerDieUI.transform.GetChild(0).gameObject;
        Destroy(playerDie.transform.GetChild(0).gameObject);
        playerDie.transform.parent = PlayerDieUI.transform;
        playerDie.transform.position = new Vector3(457.8341f, 11.50649f, -346.357f);
        playerDie.transform.rotation = Quaternion.Euler(368.637f, 88.404f, 1.604f);
        originDie.SetActive(false);
    }
}
