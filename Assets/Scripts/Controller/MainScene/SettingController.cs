using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SettingController : MonoBehaviour
{
    public SettingManager settingManager;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => settingManager != null);
        foreach (var device in InputSystem.devices)
        {
            foreach (var control in device.allControls)
            {
                string key = control.path.ToString();
                string NewKey = key.ToString().Split('/').Last();
                if (settingManager.keyboards.Any(k => k.key == NewKey))
                {
                    continue;
                }

                keyboard newkey = new();
                newkey.key = NewKey;

                int index = settingManager.keyboardDisplayKey.FindIndex(x => x == NewKey);
                if (index == -1)
                {
                    newkey.display = NewKey.ToUpper();
                }
                else
                {
                    newkey.display = settingManager.keyboardDisplayValue[index];
                }

                newkey.value = null;
                settingManager.keyboards.Add(newkey);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeKey(InputActionMap mp, InputAction action, int bindingIndex, TextMeshProUGUI text)
    {
        ResettingPressKey(mp, action, bindingIndex, text);
    }

    void ResettingPressKey(InputActionMap mp, InputAction action, int bindingIndex, TextMeshProUGUI text)
    {
        mp.Disable();
        text.text = "";
        action.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                string newKey = callback.action.bindings[bindingIndex].overridePath.ToString().Split('/').Last();
                //Input System
                foreach (InputAction ac in settingManager.playerBasicInformationScriptable.playerControl)
                {
                    ac.actionMap.Disable();
                    for (int i = 0; i < ac.bindings.Count; i++)
                    {
                        InputBinding binding = ac.bindings[i];
                        if ((binding.path == callback.action.bindings[bindingIndex].overridePath || (binding.overridePath != null && binding.overridePath == callback.action.bindings[bindingIndex].overridePath)) && binding.id != action.bindings[bindingIndex].id)
                        {
                            ac.ApplyBindingOverride(i, new InputBinding { overridePath = "" });
                            break;
                        }
                    }
                    ac.actionMap.Enable();
                }

                //UI
                foreach (keyboard key in settingManager.keyboards)
                {
                    if (key.key == newKey)
                    {
                        if (key.value != null && key.value.text == key.display)
                        {
                            key.value.text = "";
                        }
                        key.value = text;
                        text.text = key.display;
                    }
                }
                callback.Dispose();
                mp.Enable();
            })
            .Start();
    }
}
