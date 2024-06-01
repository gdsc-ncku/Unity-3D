using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdventureSceneGemDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Value;
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(transform.position, Vector3.up, 50f * Time.deltaTime);
        Value.text = "x" + playerBattleInfo.Soul.ToString("0");
    }
}
