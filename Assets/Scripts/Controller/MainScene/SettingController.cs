using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingController : MonoBehaviour
{
    public SettingManager settingManager;
    // Start is called before the first frame update
    void Start()
    {
        
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
                        if (binding.overridePath == callback.action.bindings[bindingIndex].overridePath && binding.id != action.bindings[bindingIndex].id)
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
                        if (key.value.text == key.display)
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
