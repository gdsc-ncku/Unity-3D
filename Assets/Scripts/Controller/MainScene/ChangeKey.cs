using TMPro;
using UnityEngine;

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
        KeyIndex key;
        if (bindingIndex == 1)
        {
            key = KeyIndex.Forward;
            PlayerPrefs.SetString(KeyIndex.Forward.ToString(), text.text);
        }
        else if (bindingIndex == 2)
        {
            key = KeyIndex.Backward;
        }
        else if (bindingIndex == 3)
        {
            key = KeyIndex.Left;
        }
        else
        {
            key = KeyIndex.Right;
        }

        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Move, bindingIndex, text, key);
    }

    public void changeSpaceKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Jump, bindingIndex, text, KeyIndex.Jump);
    }

    public void changeFireKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Fire, bindingIndex, text, KeyIndex.Fire);
    }

    public void changeWeapon1Key()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon1, bindingIndex, text, KeyIndex.Weapon1);
    }

    public void changeWeapon2Key()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon2, bindingIndex, text, KeyIndex.Weapon2);
    }

    public void changeQ_SkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Q_Skill, bindingIndex, text, KeyIndex.Q_Skill);
    }

    public void changeE_SkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.E_Skill, bindingIndex, text, KeyIndex.E_Skill);
    }

    public void changeBackpackKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Backpack, bindingIndex, text, KeyIndex.Backpack);
    }

    public void changeReloadKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Reload, bindingIndex, text, KeyIndex.Reload);
    }

    public void changeWeapomSkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.WeaponSkill, bindingIndex, text, KeyIndex.WeaponSkill);
    }

    public void changePickUpKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.PickUp, bindingIndex, text, KeyIndex.PickUp);
    }
}
