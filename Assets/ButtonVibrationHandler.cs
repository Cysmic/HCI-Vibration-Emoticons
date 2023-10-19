using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CandyCoded.HapticFeedback;

// install: https://github.com/CandyCoded/HapticFeedback.git#v1.0.3 via Unity pm

public class ButtonVibrationHandler : MonoBehaviour
{
    [SerializeField]
    private Button defaultVibrationButton;
    [SerializeField]
    private Button lightVibrationButton;
    [SerializeField]
    private Button mediumVibrationButton;
    [SerializeField]
    private Button heavyVibrationButton;

    private void OnEnable()
    {
        defaultVibrationButton.onClick.AddListener(DefaultVibration);
        lightVibrationButton.onClick.AddListener(LightVibration);
        mediumVibrationButton.onClick.AddListener(MediumVibration);
        heavyVibrationButton.onClick.AddListener(HeavyVibration);
    }

    private void OnDisable()
    {
        defaultVibrationButton.onClick.RemoveListener(DefaultVibration);
        lightVibrationButton.onClick.RemoveListener(LightVibration);
        mediumVibrationButton.onClick.RemoveListener(MediumVibration);
        heavyVibrationButton.onClick.RemoveListener(HeavyVibration);
    }

    private void DefaultVibration()
    {
        Debug.Log("Default Vibration performed!");
        Handheld.Vibrate();
    }

    private void LightVibration()
    {
        Debug.Log("Light Vibration performed!");
        HapticFeedback.LightFeedback();
    }

    private void MediumVibration()
    {
        Debug.Log("Medium Vibration performed!");
        HapticFeedback.MediumFeedback();
    }

    private void HeavyVibration()
    {
        Debug.Log("Heavy Vibration performed!");
        HapticFeedback.HeavyFeedback();
    }
}
