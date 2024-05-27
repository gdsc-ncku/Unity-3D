using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public PlayerBasicInformationScriptable playerBasicInformationScriptable;
    public List<keyboard> keyboards = new List<keyboard>();
    public List<string> keyboardDisplayKey = new();
    public List<string> keyboardDisplayValue = new();
    // Start is called before the first frame update
    void Start()
    {

    }
}
