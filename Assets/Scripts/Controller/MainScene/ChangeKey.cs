using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    [SerializeField] SettingController settingController;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int bindingIndex;
    [SerializeField] KeyIndex key;

    private void OnEnable()
    {
        text.text = PlayerPrefs.GetString(key.ToString());
    }

    public void changeMoveKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        KeyIndex key;
        switch (bindingIndex)
        {
            case 1:
                key = KeyIndex.Forward;
                break;
            case 2:
                key = KeyIndex.Backward;
                break;
            case 3:
                key = KeyIndex.Left;
                break;
            case 4:
                key = KeyIndex.Right;
                break;
            default:
                return;
        }

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Move, bindingIndex, text, key);
    }

    public void changeSpaceKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Jump, bindingIndex, text, KeyIndex.Jump);
    }

    public void changeFireKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Fire, bindingIndex, text, KeyIndex.Fire);
    }

    public void changeWeapon1Key()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon1, bindingIndex, text, KeyIndex.Weapon1);
    }

    public void changeWeapon2Key()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon2, bindingIndex, text, KeyIndex.Weapon2);
    }

    public void changeQ_SkillKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Q_Skill, bindingIndex, text, KeyIndex.Q_Skill);
    }

    public void changeE_SkillKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.E_Skill, bindingIndex, text, KeyIndex.E_Skill);
    }

    public void changeBackpackKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Backpack, bindingIndex, text, KeyIndex.Backpack);
    }

    public void changeReloadKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Reload, bindingIndex, text, KeyIndex.Reload);
    }

    public void changeWeapomSkillKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.WeaponSkill, bindingIndex, text, KeyIndex.WeaponSkill);
    }

    public void changePickUpKey()
    {
        gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.PickUp, bindingIndex, text, KeyIndex.PickUp);
    }
}
