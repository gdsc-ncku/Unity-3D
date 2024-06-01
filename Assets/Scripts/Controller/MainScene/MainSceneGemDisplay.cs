using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class MainSceneGemDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Value;
    [SerializeField] PlayerBasicInformationScriptable playerBasicInfo;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(transform.position, Vector3.up, 50f * Time.deltaTime);
        Value.text = "x" + playerBasicInfo.Soul.ToString("0");
    }
}
