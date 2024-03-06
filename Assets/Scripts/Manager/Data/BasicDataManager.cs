using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicDataManager : MonoBehaviour
{
    public PlayerBasicInformationScriptable playerBasicInformation;
    public UnityEvent<bool> Catched = new();
    public DataManager dataManager;
}
