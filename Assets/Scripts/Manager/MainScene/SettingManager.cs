using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    PlayerBasicInformationScriptable playerBasicInformation;
    public List<KeyCodeToImage> keyboardPair; 
    // Start is called before the first frame update
    void Start()
    {
        keyboardPair = playerBasicInformation.keyboard;
    }
}
