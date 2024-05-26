using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetting : MonoBehaviour
{
    [SerializeField] private TMP_InputField sliderText = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private float maxSliderAmount = 5.0f;
    [SerializeField] private PlayerBasicInformationScriptable playerBasic;

    private void Start()
    {
        float sensitivity = playerBasic.edpi / playerBasic.MouseDPI;
        sliderText.text = sensitivity.ToString("0.0##");
        slider.value = sensitivity / maxSliderAmount;

        sliderText.onEndEdit.AddListener(SliderChange);
        slider.onValueChanged.AddListener(SliderChange);
    }

    public void SliderChange(float value)
    {
        float localValue = value * maxSliderAmount;
        if(localValue > maxSliderAmount)
        {
            localValue = maxSliderAmount;
        }
        sliderText.text = localValue.ToString("0.0##");
        playerBasic.edpi = playerBasic.MouseDPI * localValue;
        PlayerPrefs.SetFloat("MouseSensitivity", playerBasic.edpi);
    }

    public void SliderChange(string str)
    {
        float localValue = float.Parse(str);
        slider.value = localValue / maxSliderAmount;
        if (localValue > maxSliderAmount)
        {
            localValue = maxSliderAmount;
        }
        sliderText.text = localValue.ToString("0.0##");
        playerBasic.edpi = playerBasic.MouseDPI * localValue;
        PlayerPrefs.SetFloat("MouseSensitivity", playerBasic.edpi);
    }
}
