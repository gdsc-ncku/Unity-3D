using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SettingController : MonoBehaviour
{
    public SettingManager settingManager;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (Key key in System.Enum.GetValues(typeof(Key)))
        {
            if (key != Key.None)
            {
                KeyControl keyControl = Keyboard.current[key];
                if (keyControl != null)
                {
                    Debug.Log(keyControl.path);
                    //settingManager.keyboards.Add(keyControl.path);
                }
            }
        }
    }
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
