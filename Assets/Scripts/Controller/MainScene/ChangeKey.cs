using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeKey : MonoBehaviour
{
    [SerializeField] SettingController settingController;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int bindingIndex;
    private void Awake()
    {
        
    }

    void Update()
    {

    }

    public void changeMoveKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Move, bindingIndex, text);
    }
}
