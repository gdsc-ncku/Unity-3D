using TMPro;
using UnityEngine;

public class ChangeKey : MonoBehaviour
{
    [SerializeField] SettingController settingController;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int bindingIndex;

    public void changeMoveKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Move, bindingIndex, text);

        if (bindingIndex == 1)
        {
            PlayerPrefs.SetString(KeyIndex.Forward.ToString(), text.text);
        }
        else if (bindingIndex == 2)
        {
            PlayerPrefs.SetString(KeyIndex.Backward.ToString(), text.text);
        }
        else if (bindingIndex == 3)
        {
            PlayerPrefs.SetString(KeyIndex.Left.ToString(), text.text);
        }
        else if ((bindingIndex == 4))
        {
            PlayerPrefs.SetString(KeyIndex.Right.ToString(), text.text);
        }
    }

    public void changeSpaceKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Jump, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Jump.ToString(), text.text);
    }

    public void changeFireKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Fire, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Fire.ToString(), text.text);
    }

    public void changeWeapon1Key()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon1, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Weapon1.ToString(), text.text);
    }

    public void changeWeapon2Key()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Weapon2, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Weapon2.ToString(), text.text);
    }

    public void changeQ_SkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Q_Skill, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Q_Skill.ToString(), text.text);
    }

    public void changeE_SkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.E_Skill, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.E_Skill.ToString(), text.text);
    }

    public void changeBackpackKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Backpack, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Backpack.ToString(), text.text);
    }

    public void changeReloadKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.Reload, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.Reload.ToString(), text.text);
    }

    public void changeWeapomSkillKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.WeaponSkill, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.WeaponSkill.ToString(), text.text);
    }

    public void changePickUpKey()
    {
        settingController.ChangeKey(settingController.settingManager.playerBasicInformationScriptable.playerControl.Player, settingController.settingManager.playerBasicInformationScriptable.playerControl.Player.PickUp, bindingIndex, text);
        PlayerPrefs.SetString(KeyIndex.PickUp.ToString(), text.text);
    }
}
